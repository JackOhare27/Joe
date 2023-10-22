using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static GameObject sampleInstance;
    public GameObject[] manager;
    public float countDownTimer = 10;
    public int numOfPlayers = 0, prevPlayerCount =0;
    public bool gameStart = false, redTaken = false, greenTaken = false, blueTaken =false, tanTaken = false;
    public bool[] taken = new bool[4];
    public GameObject[] players = new GameObject[4];
    public Image[] iconList = new Image[4];
    public GameObject onGame, preGame;

     public TMPro.TextMeshProUGUI countDownText;

    public void OnStart(InputAction.CallbackContext context)
    {
        
        gameStart = context.action.triggered;

    }

    bool checkifCanStart()
    {
        //e is for num of players locked in, o is the number of players total
        int e =0, o = 0;
        for(int i = 0; i < taken.Length;i++)
        {
            if(taken[i] == true)
            {

                e++;
            }
            if(players[i] != null)
            {
                o++;
            }
        }
        if(e == o)
        {
            return true;
        }

        return false;
    }
    private void Awake()
    {
        if (sampleInstance != null)
            Destroy(sampleInstance);

        sampleInstance = gameObject;
        DontDestroyOnLoad(this);
    }

    void Update()
    {

        
        numOfPlayers = GameObject.FindGameObjectsWithTag("Player").Length;

        
        if(numOfPlayers >= 2 && numOfPlayers == prevPlayerCount && gameStart == false && checkifCanStart() == true)
        {
            countDownTimer -= Time.deltaTime;
            countDownText.text = "Starting in:\n"+((int)countDownTimer).ToString();
        }
        else if(checkifCanStart() == false)
        {
            countDownTimer = 10f;
            countDownText.text = "";
        }
        if(countDownTimer <= 0)
        {
            gameStart = true;
        }

        if(gameStart ==true && numOfPlayers >= 2)
        {
            preGame.SetActive(false);
            onGame.SetActive(true);
            

        }

        prevPlayerCount = numOfPlayers;
    }
}
