using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameOnline : MonoBehaviourPunCallbacks
{
    public Transform[] spawners;
    public GameObject[] bulletImages;
    private string[] playerPrefabNames = new string[2];

    [Space]

    [Header("Cameras")]
    public MainCamera cmra;

    private void Start()
    {
        //for (int i = 0; i < spawners.Length; i++)
        //{
        //    if (PlayerSettings.charactersSelected[i] == 0)
        //    {
        //        playerPrefabNames[i] = "Player 1 Online";
        //    }
        //    else if (PlayerSettings.charactersSelected[i] == 1)
        //    {
        //        playerPrefabNames[i] = "Player 2 Online";
        //    }

        //    GameObject player;

        //    if (PhotonNetwork.IsMasterClient)
        //    {
        //        player = PhotonNetwork.Instantiate("Players/" + playerPrefabNames[i], spawners[i].position, Quaternion.identity);
        //    }
        //    else
        //    {
        //        player = PhotonNetwork.Instantiate("Players/" + playerPrefabNames[i], spawners[i].position, Quaternion.identity);
        //    }

        //    player.GetComponent<PlayerBodyOnline>().InitialSettings(bulletImages[i]);

        //    if (photonView.IsMine)
        //    {
        //        cmra.mainPlayer = player;
        //    }
        //}

        Debug.Log(PlayerSettings.charactersSelected[0]);
        Debug.Log(PlayerSettings.charactersSelected[1]);

        GameObject player;

        if (PhotonNetwork.IsMasterClient)
        {
            if (PlayerSettings.charactersSelected[0] == 0)
            {
                playerPrefabNames[0] = "Player 1 Online";
            }
            else if (PlayerSettings.charactersSelected[0] == 1)
            {
                playerPrefabNames[0] = "Player 2 Online";
            }

            player = PhotonNetwork.Instantiate("Players/" + playerPrefabNames[0], spawners[0].position, Quaternion.identity);

            player.GetComponent<PlayerBodyOnline>().InitialSettings(bulletImages[0]);

        }
        else
        {
            if (PlayerSettings.charactersSelected[1] == 0)
            {
                playerPrefabNames[1] = "Player 1 Online";
            }
            else if (PlayerSettings.charactersSelected[1] == 1)
            {
                playerPrefabNames[1] = "Player 2 Online";
            }
            player = PhotonNetwork.Instantiate("Players/" + playerPrefabNames[1], spawners[1].position, Quaternion.identity);

            player.GetComponent<PlayerBodyOnline>().InitialSettings(bulletImages[1]);
        }

        //if (photonView.IsMine)
        //{
        //    cmra.mainPlayer = player;
        //}
    }


    //private int index;

    //[Header("Players")]
    //public GameObject[] player;

    //[Space]

    //[Header("Health")]
    //public GameObject[] textHealthObject;
    //private Text[] textHealth = new Text[2];
    //private int[] hPPoints = new int[2];

    //[Space]

    //[Header("Score")]
    //public GameObject[] scoreTextObject;
    //private Text[] scoreText = new Text[2];
    //public static int[] score = new int[2];

    //[Space]

    //[Header("Shoot Bar")]
    //public GameObject[] shootBarObject;
    //private SimpleHealthBar[] fireBar = new SimpleHealthBar[2];
    //public float initFireTime;
    //public float[] currFireTime;

    //[Space]

    //[Header("Breath Bar")]
    //public GameObject[] breathBarObject;
    //private SimpleHealthBar[] breathBar = new SimpleHealthBar[2];
    //public float initBreatheTime;
    //public float[] currBreatheTime;

    //[Space]

    //[Header("Special Bar")]
    //public GameObject[] specialBarObject;
    //private SimpleHealthBar[] specialBar = new SimpleHealthBar[2];
    //public float initSpecialTime;
    //public float[] currSpecialTime;

    //[Space]

    //[Header("Time")]
    //public float levelTime;
    //public static float currentTime;
    //private int cleanTime;
    //public Text timeText;
    //public GameObject timeUp;

    //[Space]

    //[Header("Win Text")]
    //public GameObject[] winText;

    //[Space]

    //[Header("Cameras")]
    //public GameObject[] cmra;

    //[Space]

    //[Header("Game Over")]
    //public GameObject gameOverText;

    //[Space]

    //[Header("Sounds")]
    //private AudioSource src;

    //[Space]

    //[Header("Other Objects")]
    //public GameObject divider;
    //public GameObject pauseUI;

    //[Space]

    //[Header("Difficulty Level")]
    //public static int diffLevel;

    //[Space]

    //public bool twoPlayersGame;
    //private bool lockGoal;
    //public static bool paused;
    //void Start()
    //{
    //    if (SceneManager.GetActiveScene().buildIndex == 1)
    //    {
    //        twoPlayersGame = MainMenu.enable2PGame;
    //        score[0] = 0;
    //        score[1] = 0;
    //    }

    //    pauseUI.SetActive(false);

    //    for (int i = 0; i < player.Length; i++)
    //    {
    //        textHealth[i] = textHealthObject[i].GetComponent<Text>();
    //        scoreText[i] = scoreTextObject[i].GetComponent<Text>();

    //        fireBar[i] = shootBarObject[i].GetComponent<SimpleHealthBar>();
    //        breathBar[i] = breathBarObject[i].GetComponent<SimpleHealthBar>();
    //        specialBar[i] = specialBarObject[i].GetComponent<SimpleHealthBar>();

    //        currFireTime[i] = player[i].GetComponent<PlayerBody>().shootTimer;
    //        currBreatheTime[i] = player[i].GetComponent<PlayerBody>().breathe;
    //        currSpecialTime[i] = player[i].GetComponent<PlayerBody>().specialTimer;

    //        hPPoints[i] = player[i].GetComponent<PlayerBody>().hP;
    //    }
    //    initFireTime = PlayerBody.shootCd;
    //    initBreatheTime = PlayerBody.breatheTime;
    //    initSpecialTime = PlayerBody.specialSetTime;

    //    MainSettings(twoPlayersGame);

    //    src = GetComponent<AudioSource>();

    //    currentTime = levelTime;
    //    timeUp.SetActive(false);

    //    for (int i = 0; i < winText.Length; i++)
    //    {
    //        winText[i].SetActive(false);
    //    }

    //    gameOverText.SetActive(false);

    //    lockGoal = false;

    //    paused = false;
    //}

    //void Update()
    //{
    //    if (Input.GetButtonDown("Pause"))
    //    {
    //        Pause();
    //    }

    //    ShowData();
    //    LevelTime();

    //    GameOver();

    //    if (!lockGoal)
    //        Winner();
    //}

    //public void MainSettings(bool active)
    //{
    //    PlayerData(0, true);
    //    PlayerData(1, active);

    //    if (active)
    //        index = 2;
    //    else
    //        index = 1;

    //    cmra[0].SetActive(active);
    //    cmra[1].SetActive(active);

    //    divider.SetActive(active);
    //}

    //public void PlayerData(int playerIndex, bool hideOrShow)
    //{
    //    player[playerIndex].SetActive(hideOrShow);
    //    textHealthObject[playerIndex].SetActive(hideOrShow);
    //    scoreTextObject[playerIndex].SetActive(hideOrShow);
    //    shootBarObject[playerIndex].SetActive(hideOrShow);
    //    breathBarObject[playerIndex].SetActive(hideOrShow);
    //    specialBarObject[playerIndex].SetActive(hideOrShow);
    //}

    //public Text TextHP(int playerLife, Text pHP)
    //{
    //    if (playerLife > 0)
    //    {
    //        pHP.text = "Life: " + playerLife;
    //    }
    //    else
    //    {
    //        pHP.text = "Dead";
    //    }

    //    return pHP;
    //}

    //public Text TextScore(int scorePoints, Text pointsText)
    //{
    //    pointsText.text = "Score: " + scorePoints;

    //    return pointsText;
    //}

    //public void ShowData()
    //{
    //    for (int i = 0; i < index; i++)
    //    {
    //        hPPoints[i] = player[i].GetComponent<PlayerBody>().hP;
    //        textHealth[i] = TextHP(hPPoints[i], textHealth[i]);
    //        scoreText[i] = TextScore(score[i], scoreText[i]);

    //        currFireTime[i] = player[i].GetComponent<PlayerBody>().shootTimer;
    //        currBreatheTime[i] = player[i].GetComponent<PlayerBody>().breathe;
    //        currSpecialTime[i] = player[i].GetComponent<PlayerBody>().specialTimer;

    //        fireBar[i].UpdateBar(currFireTime[i], initFireTime);
    //        breathBar[i].UpdateBar(currBreatheTime[i], initBreatheTime);
    //        specialBar[i].UpdateBar(currSpecialTime[i], initSpecialTime);
    //    }
    //}

    //void LevelTime()
    //{
    //    cleanTime = (int)currentTime;

    //    if (currentTime >= 0)
    //    {
    //        timeText.text = cleanTime.ToString();
    //        currentTime -= Time.deltaTime;
    //    }

    //    if (currentTime <= 0)
    //        timeUp.SetActive(true);
    //}

    //public void Pause()
    //{
    //    if (Time.timeScale == 0f)
    //    {
    //        pauseUI.SetActive(false);
    //        player[0].GetComponent<Player1>().isPaused = false;
    //        player[1].GetComponent<Player2>().isPaused = false;
    //        Time.timeScale = 1f;
    //        Camera.main.GetComponent<SoundManager>().SetAudioSnapshot(1);

    //    }
    //    else if (Time.timeScale == 1f)
    //    {
    //        pauseUI.SetActive(true);
    //        player[0].GetComponent<Player1>().isPaused = true;
    //        player[1].GetComponent<Player2>().isPaused = true;
    //        Time.timeScale = 0f;
    //        Camera.main.GetComponent<SoundManager>().SetAudioSnapshot(2);
    //    }
    //}

    //public void Winner()
    //{
    //    if (player[0].GetComponent<PlayerBody>().winState)
    //    {
    //        if (twoPlayersGame)
    //        {
    //            winText[1].SetActive(true);
    //        }
    //        else
    //        {
    //            winText[0].SetActive(true);
    //        }
    //        lockGoal = true;
    //        src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(8));
    //        score[0] += 500;
    //    }
    //    else if (player[1].GetComponent<PlayerBody>().winState)
    //    {
    //        winText[2].SetActive(true);
    //        lockGoal = true;
    //        src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(8));
    //        score[1] += 500;
    //    }
    //}

    //public void GameOver()
    //{
    //    if ((twoPlayersGame && (!player[0].gameObject.activeSelf && !player[1].gameObject.activeSelf)) || (!twoPlayersGame && !player[0].gameObject.activeSelf))
    //    {
    //        gameOverText.SetActive(true);
    //    }
    //}

    //public void Restart()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    //public void LoadMenu()
    //{
    //    Pause();
    //    SceneManager.LoadScene("Main Menu");
    //}
}
