using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public TMPro.TextMeshProUGUI countdownText;
    public Transform[] playing2, playing3, playing4;
    public GameObject[] playersAndUI, playersSpawn = new GameObject[4];
    private GameObject getSpawnPoints, gm;
    private Vector2 xRange, zRange;
    private AudioSource whistle;
    // Start is called before the first frame update
    void Start()
    {
        whistle = GetComponent<AudioSource>();
        getSpawnPoints = GameObject.FindGameObjectWithTag("spawnController");
         xRange = getSpawnPoints.GetComponent<outOfBounds>().xRespawn;
         zRange = getSpawnPoints.GetComponent<outOfBounds>().zRespawn;
        int playerCount = 8;
        gm = GameObject.FindGameObjectWithTag("GameManager");
        playersSpawn = gm.GetComponent<gameManager>().players;
        if(gm != null)
           playerCount = 2* (gm.GetComponent<gameManager>().numOfPlayers);
        switch(playerCount)
        {
            case 4:
                {
                    activateCharacters(playersAndUI, playerCount);
                    setUI(playersAndUI,playing2, playerCount);
                    break;
                }

            case 6:
                {
                    activateCharacters(playersAndUI, playerCount);
                    setUI(playersAndUI, playing3, playerCount);
                    break;
                }

            case 8:
                {
                    activateCharacters(playersAndUI, playerCount);
                    setUI(playersAndUI, playing4, playerCount);
                    break;
                }
            default:
                {
                    activateCharacters(playersAndUI, playerCount);
                    setUI(playersAndUI, playing4, playerCount);
                    break;
                }
                
        }
        StartCoroutine(StartCountDown(playersAndUI, playerCount));
    }

    IEnumerator StartCountDown(GameObject[] gameObjects, int count)
    {
         
        for (int i = 0; i < count; i += 2)
        {
            //OPTIMIZE LATER
            //COULD DISABLE FROM THE START IN INSPECTOR THEN JUST ACITVATE ON BOOT
            ParticleSystem woofingPart = gameObjects[i].GetComponentInChildren<ParticleSystem>(); 
             var p = woofingPart.emission;
             p.enabled = false;

            movement mvm = gameObjects[i].GetComponent<movement>();
            mvm.enabled = false;
        }
        int e = 3;
        while (e >= 0)
        {
            countdownText.text = e.ToString();
           // Debug.Log(i);
            yield return new WaitForSeconds(1);
            e--;
        }
        countdownText.text = "";
        for (int i = 0; i < count; i += 2)
        {
            ParticleSystem woofingPart = gameObjects[i].GetComponentInChildren<ParticleSystem>(); 
             var p = woofingPart.emission;
             p.enabled = true;

            movement mvm = gameObjects[i].GetComponent<movement>();
            mvm.enabled = true;
        }
        whistle.Play();
    }
    void activateCharacters(GameObject[] gameObjects, int count)
    {
        Vector2 spawnLocation = new Vector2(Random.Range(xRange.x, xRange.y), Random.Range(zRange.x,zRange.y)); 
        for(int i = 0; i < count;i++)
        {
            if(i %2 == 0)
            {
            gameObjects[i] = playersSpawn[i/2];
            gameObjects[i].transform.position =  new Vector3(Random.Range(spawnLocation.x-10, spawnLocation.x+10), 1, Random.Range(spawnLocation.y - 10, spawnLocation.y));
            gameObjects[i].transform.rotation = new Quaternion(0, Random.Range(0,5), 0, 1);
            }
            gameObjects[i].SetActive(true);
           if(i %2==0)
            {
               

             
            }
        }
    }

    void setUI(GameObject[] UIs,Transform[] UISetPosition, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (i % 2 == 1)
            {
                UIs[i].transform.position = UISetPosition[i/2].position;
            }
        }
    }

}
