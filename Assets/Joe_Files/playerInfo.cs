using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Image = UnityEngine.UI.Image;
public class playerInfo : MonoBehaviour
{
    public TMPro.TextMeshProUGUI playerHealthText;
    public Image playerIcon;
    public int currJoeMat = 0;
    private GameObject[] sp = new GameObject[4];
    public GameObject[] UIs = new GameObject[4],  JoeBodyAccessories = new GameObject[5];
    public Material[] bodyMats = new Material[4];
    public Sprite[] icons;
    public MeshRenderer JoeBodyMesh, ringMesh;
    private GameObject[] JoeBodyAccessories_Dummy = new GameObject[5];
   
    public Material[] accesMats = new Material[4];
    private MeshRenderer joeBodyMat;
    private gameManager gm;
    
    public int launchCapability = 0, playerNum, playerLives = 3;
    public float explosionPower = 1;
    private bool game = false, canSelect = false, confirmed= false, doInc=false, doDec=false, canChange = true;
    public GameObject playerMesh;
    int nameNumber = 0;


IEnumerator waitForTime()
{
    yield return new WaitForSeconds(.1f);
    canSelect = true;
}

  public void OnConfirm(InputAction.CallbackContext context)
    {
        if(Selected() == false && canSelect == true) 
        {
            confirmed = true;
            JoeBodyMesh.material  = bodyMats[currJoeMat];
            foreach(GameObject part in JoeBodyAccessories)
            {
                part.GetComponent<MeshRenderer>().material = accesMats[currJoeMat];
            }      
        }
        else
        {
            //maybe put text to say its taken then have it disapear
            Debug.Log("already taken");

        }
    }

    public void OnInc(InputAction.CallbackContext context)
    {
        if(canSelect == true) 
            doInc =context.action.triggered;
     
      // Debug.Log(currJoeMat);
     
    }
    public void OnDec(InputAction.CallbackContext context)
    {
       if(canSelect == true) 
        doDec =context.action.triggered;
     
      // Debug.Log(currJoeMat);
       
    }


    void Awake()
    {
        gm = GameObject.FindWithTag("GameManager").GetComponent<gameManager>();

        nameNumber = gm.numOfPlayers;
        gameObject.transform.name = "Player"+(nameNumber+1);
        sp = gm.players;
        switch(nameNumber+1)
        {
            case 1:
            {
                sp[0] = gameObject;
                break;
            }

            case 2:
            {
                sp[1] = gameObject;
                break;
            }

            case 3:
            {
                sp[2] = gameObject;
                break;
            }
            case 4:
            {
                sp[3] = gameObject;
                break;
            }
        }
    }

    void changeMaterial()
    {
        if(doInc && canChange && !confirmed)
        {
            canChange = false;
            if(currJoeMat < 3)
            { 
                currJoeMat += 1;
            } else
            {
                currJoeMat = 0;
            }
            StartCoroutine(holdOn());
        } 
        if(doDec && canChange && !confirmed)
        { 
            canChange = false;
            if(currJoeMat > 0)
            { 
                currJoeMat += -1;
            } else
            {
                currJoeMat =  3;
            }
            StartCoroutine(holdOn());
            
        }
    }

    bool Selected()
    {
        if(!gm.taken[currJoeMat])
            {
                
                gm.taken[currJoeMat] = true;
                return false;
            }
            else{return true;}
    }
    IEnumerator holdOn()
    {
         yield return new WaitForSeconds(.2f);
         canChange = true;
    }

    public void meshEnabled(bool mesh)
    {
         if(mesh)
        {
            playerMesh.SetActive(false);
        }
        else 
        {
            playerMesh.SetActive(true);
        }
    }

    void Start()
    {
        StartCoroutine(waitForTime());
        UIs = GameObject.FindGameObjectWithTag("uiselect").GetComponent<UIManager>().UI;
        gm.taken[0] = false;

    }
    void Update()
    {
        game = gm.gameStart;
        if(game == true)
        {
            //Debug.Log(currJoeMat);
            //turns pn health text connection
            try{
                ringMesh.material = accesMats[currJoeMat];
                playerIcon = GameObject.FindGameObjectWithTag("Icon"+nameNumber).GetComponent<Image>();
                playerIcon.sprite = icons[currJoeMat];
                playerHealthText = GameObject.FindGameObjectWithTag("Health"+nameNumber).GetComponent<TMPro.TextMeshProUGUI>();
            } catch
            {
                // /throw;
            }

          
        }
        else
        { 
            changeMaterial();
            //Debug.Log("UISelect"+nameNumber);
            //enable ui element want.
            //Debug.Log(UIs.Length);
            UIs[nameNumber].SetActive(true);
            joeBodyMat = GameObject.FindGameObjectWithTag("body"+nameNumber).GetComponent<MeshRenderer>();
            JoeBodyAccessories_Dummy = GameObject.FindGameObjectsWithTag("accessories"+nameNumber);
            foreach(GameObject part in JoeBodyAccessories_Dummy)
            {

                part.GetComponent<MeshRenderer>().material = accesMats[currJoeMat];
                
            }
            joeBodyMat.material = bodyMats[currJoeMat] ;
            
            //show first color joe, and allow user to change it with east and west button with a to confirm
        }
        
        if(playerHealthText != null)
        {
            playerHealthText = GameObject.FindGameObjectWithTag("Health"+nameNumber).GetComponent<TMPro.TextMeshProUGUI>();
            playerHealthText.text = playerLives + "\n" + launchCapability*10+"%";
        }      
    }
}
