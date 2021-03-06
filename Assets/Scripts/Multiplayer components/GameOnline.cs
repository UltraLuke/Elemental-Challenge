using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameOnline : MonoBehaviourPun, IPunObservable
{
    private int index = 2;

    [Header("Players")]
    public Transform[] spawners;
    private PlayerBodyOnline[] player = new PlayerBodyOnline[2];
    private string[] playerPrefabNames = new string[2];

    [Space]

    [Header("Bullet")]
    public GameObject[] bulletImages;

    [Space]

    [Header("Cameras")]
    public MainCamera cmra;

    [Space]

    [Header("Win")]
    public GameObject[] winText;
    private bool _lockGoal = false;

    [Space]

    [Header("Game Over")]
    public GameObject gameOverText;

    [Space]

    [Header("Health")]
    public GameObject[] textHealthObject;
    private Text[] textHealth = new Text[2];
    public int[] hPPoints = new int[2];

    [Space]

    [Header("Score")]
    public GameObject[] scoreTextObject;
    private Text[] scoreText = new Text[2];
    private static int[] score = new int[2];

    [Space]

    [Header("Score")]
    public string nextLevelName;

    public static int diffLevel;

    public static int[] Score { get => score; set => score = value; }

    private void Awake()
    {
        diffLevel = 0;
    }

    private void Start()
    {
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
            this.player[0] = player.GetComponent<PlayerBodyOnline>();
            this.player[0].InitialSettings(bulletImages[0], Winner, GameOver);
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
            this.player[1] = player.GetComponent<PlayerBodyOnline>();
            this.player[1].InitialSettings(bulletImages[1], Winner, GameOver);
        }

        MainSettings(true);

        if (SceneManager.GetActiveScene().name == "Tutorial Online" || SceneManager.GetActiveScene().name == "Tutorial")
        {
            Score[0] = 0;
            Score[1] = 0;
        }

        gameOverText.SetActive(false);
        _lockGoal = false;

        for (int i = 0; i < winText.Length; i++)
        {
            winText[i].SetActive(false);
        }
    }

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

    //[Header("Sounds")]
    //private AudioSource src;

    //[Space]

    //[Header("Other Objects")]
    //public GameObject divider;
    //public GameObject pauseUI;

    //[Space]

    //[Header("Difficulty Level")]

    //[Space]

    //public bool twoPlayersGame;
    //public static bool paused;



    //void Start()
    //{

    //    pauseUI.SetActive(false);

    //    initFireTime = PlayerBody.shootCd;
    //    initBreatheTime = PlayerBody.breatheTime;
    //    initSpecialTime = PlayerBody.specialSetTime;


    //    src = GetComponent<AudioSource>();

    //    currentTime = levelTime;
    //    timeUp.SetActive(false);

    //    paused = false;
    //}



    void Update()
    {
        //if (Input.GetButtonDown("Pause"))
        //{
        //    Pause();
        //}

        ShowData();
        //LevelTime();
    }

    public void Winner(bool isMaster)
    {
        photonView.RPC("RPCWinner", RpcTarget.All, isMaster);
    }
    public void GameOver(bool isMaster)
    {
        photonView.RPC("RPCGameOver", RpcTarget.All, isMaster);
    }
    void LoadLevel(string name)
    {
        photonView.RPC("LoadNextLevel", RpcTarget.All, name);
    }

    public void MainSettings(bool active)
    {
        PlayerData(0, PhotonNetwork.IsMasterClient);
        PlayerData(1, !PhotonNetwork.IsMasterClient);
    }

    public void PlayerData(int playerIndex, bool hideOrShow)
    {
        textHealthObject[playerIndex].SetActive(hideOrShow);
        textHealth[playerIndex] = textHealthObject[playerIndex].GetComponent<Text>();

        scoreTextObject[playerIndex].SetActive(hideOrShow);
        scoreText[playerIndex] = scoreTextObject[playerIndex].GetComponent<Text>();

        //shootBarObject[playerIndex].SetActive(hideOrShow);
        //breathBarObject[playerIndex].SetActive(hideOrShow);
        //specialBarObject[playerIndex].SetActive(hideOrShow);
    }

    public Text TextHP(int playerLife, Text pHP)
    {
        if (playerLife > 0)
        {
            pHP.text = "Life: " + playerLife;
        }
        else
        {
            pHP.text = "Dead";
        }

        return pHP;
    }

    public Text TextScore(int scorePoints, Text pointsText)
    {
        pointsText.text = "Score: " + scorePoints;

        return pointsText;
    }

    public void ShowData()
    {
        photonView.RPC("RPCUpdateValues", RpcTarget.All);
    }

    #region Unused methods
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


    //public void Restart()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    //public void LoadMenu()
    //{
    //    Pause();
    //    SceneManager.LoadScene("Main Menu");
    //}

    #endregion

    #region PunRPCs
    [PunRPC]
    void RPCWinner(bool isMaster)
    {
        if (_lockGoal) return;

        Debug.Log(isMaster + " - " + photonView.IsMine);

        if (isMaster)
        {
            if (PhotonNetwork.IsMasterClient)
                winText[0].SetActive(true);
            else
                winText[1].SetActive(true);
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
                winText[2].SetActive(true);
            else
                winText[0].SetActive(true);
        }

        cmra.GetComponent<AudioSource>().PlayOneShot(cmra.GetComponent<SoundManager>().Sound(8));

        Invoke("LoadNextLevel", 5f);

        _lockGoal = true;
    }

    [PunRPC]
    void RPCGameOver(bool isMaster)
    {
        if (isMaster == PhotonNetwork.IsMasterClient)
        {
            gameOverText.SetActive(true);
        }
    }

    [PunRPC]
    void RPCUpdateValues()
    {
        for (int i = 0; i < index; i++)
        {
            if(player[i] != null)
                hPPoints[i] = player[i].hP;

            textHealth[i] = TextHP(hPPoints[i], textHealth[i]);
            scoreText[i] = TextScore(score[i], scoreText[i]);

            //currFireTime[i] = player[i].GetComponent<PlayerBody>().shootTimer;
            //currBreatheTime[i] = player[i].GetComponent<PlayerBody>().breathe;
            //currSpecialTime[i] = player[i].GetComponent<PlayerBody>().specialTimer;

            //fireBar[i].UpdateBar(currFireTime[i], initFireTime);
            //breathBar[i].UpdateBar(currBreatheTime[i], initBreatheTime);
            //specialBar[i].UpdateBar(currSpecialTime[i], initSpecialTime);
        }
    }

    [PunRPC]
    void LoadNextLevel()
    {
        if(nextLevelName == "Main Menu")
        {
            PhotonNetwork.Disconnect();
        }
        PhotonNetwork.LoadLevel(nextLevelName);
    }
    #endregion

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Estoy escribiendo?
        if (stream.IsWriting)
        {
            //Escribo
            //stream.SendNext(score);
        }
        else
        {
            //Recibo
            //var tempScore = (int[])stream.ReceiveNext();

            //if (PhotonNetwork.IsMasterClient)
            //    score[1] = tempScore[1];
            //else
            //    score[0] = tempScore[0];
        }
    }
}
