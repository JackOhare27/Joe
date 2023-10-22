using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class scenenManagement : MonoBehaviour
{
    private gameManager game;
    public GameObject[] playerUI,mapUI, creditsUI;
    EventSystem evSys;
    private void Awake()
    {
        game = GetComponent<gameManager>();

        evSys = EventSystem.current;
    }
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void balls(int e)
    {//wont change??
        game.numOfPlayers = e;
      
    }
    public void quit()
    {
        Application.Quit();
    }
    
    public void DisablePlayerCount()
    {
        for(int i = 0; i < playerUI.Length;i++)
        {
            playerUI[i].SetActive(false);
        }
        for (int i = 0; i < mapUI.Length; i++)
        {
            mapUI[i].SetActive(true);
        }
        evSys.SetSelectedGameObject(mapUI[1]);
    }

    public void showCredits()
    {
        for(int i = 0; i < playerUI.Length;i++)
        {
            playerUI[i].SetActive(false);
            
        }
        for (int i = 0; i < creditsUI.Length; i++)
        {
            creditsUI[i].SetActive(true);
        }
        evSys.SetSelectedGameObject(creditsUI[0]);
    }
    //uh oh
    public void showMenu()
    {
        for(int i = 0; i < mapUI.Length;i++)
        {
            mapUI[i].SetActive(false);
        }
        for(int i = 0; i < playerUI.Length;i++)
        {
            playerUI[i].SetActive(true);   
        }
        for (int i = 0; i < creditsUI.Length; i++)
        {
            creditsUI[i].SetActive(false);
        }
        evSys.SetSelectedGameObject(playerUI[2]);
    }



}
