using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject canvasMenu;
	public GameObject canvasCred;
    public GameObject canvasDiff;
    public GameObject canvasMultiplayer;
    //public GameObject canvasMultiplayerLobby;
    //public GameObject canvasWaitingPlayers;

    public static bool enable2PGame;
    public static bool enableOnlineGame;
    public static int difficultLevel;

    void Start()
    {
        ClickMenu(0);
        canvasMultiplayer.SetActive(false);
    }

    public void ClickQuit()
    {
        Application.Quit();
        print("Quitted");
    }

	public void ClickGame(string scene)
	{
		SceneManager.LoadScene(scene);
	}

    public void ClickMenu(int subCanvas)
    {
        switch (subCanvas)
        {
            case 0:
                canvasMenu.SetActive(true);
                canvasCred.SetActive(false);
                canvasDiff.SetActive(false);
                canvasMultiplayer.SetActive(false);
                break;

            case 1:
                canvasCred.SetActive(true);
                canvasMenu.SetActive(false);
                break;

            case 2:
                canvasDiff.SetActive(true);
                canvasMultiplayer.SetActive(false);
                canvasMenu.SetActive(false);
                break;

            case 3:
                canvasMultiplayer.SetActive(true);
                canvasMenu.SetActive(false);
                //canvasMultiplayerLobby.SetActive(false);
                break;

            //case 4:
            //    canvasMultiplayerLobby.SetActive(true);
            //    canvasMultiplayer.SetActive(false);
            //    canvasWaitingPlayers.SetActive(false);
            //    break;

            //case 5:
            //    canvasWaitingPlayers.SetActive(true);
            //    canvasMultiplayerLobby.SetActive(false);
            //    break;
        }
    }
    //public void ClickMenu(MenuScreenType subCanvas)
    //{
    //    switch (subCanvas)
    //    {
    //        case MenuScreenType.mainmenu:
    //            canvasMenu.SetActive(true);
    //            canvasCred.SetActive(false);
    //            canvasDiff.SetActive(false);
    //            canvasMultiplayer.SetActive(false);
    //            break;

    //        case MenuScreenType.credits:
    //            canvasCred.SetActive(true);
    //            canvasMenu.SetActive(false);
    //            break;

    //        case MenuScreenType.difficulty:
    //            canvasDiff.SetActive(true);
    //            canvasMultiplayer.SetActive(false);
    //            canvasMenu.SetActive(false);
    //            break;

    //        case MenuScreenType.multiplayerMode:
    //            canvasMultiplayer.SetActive(true);
    //            canvasMenu.SetActive(false);
    //            break;
    //    }
    //}

    public void Set2PlayersGame(bool selection)
    {
        enable2PGame = selection;
    }
    public void SetOnlineGame(bool selection)
    {
        enableOnlineGame = selection;
    }

    public void SetDifficultLevel(int num)
    {
        Game.diffLevel = num;
    }
}
