using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    // ========================== VARIABLES

    [Header("GameObjects")]
    [Space]
    [SerializeField] AudioSource[] sound;
    [SerializeField] GameObject[] finishBalon;

    [Header("UI Panel")]
    [Space]
    public GameObject finishPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] Text levelText;
    [SerializeField] Text coinText;

    [Header(("Variables"))]
    [Space]
    [HideInInspector] public bool isGameStarted;
    private int level;
    private int coin;

    // =============================================== *** START

    public override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
        GetDatas();
    }

    private void Start()
    {
        LevelGenerate();
    }

    // ========================== GAME EVENTS && PANEL

    public void FinishLevel()
    {
        sound[0].Play();
        isGameStarted = false;
        StartCoroutine(FinishPanel());
        LevelUp();
        for (int i = 0; i < finishBalon.Length; i++)
        {
            float y = Random.Range(2f, 3f);
            finishBalon[i].transform.DOLocalMoveY(y, 5f);
        }
    }

    public void GameOver()
    {
        sound[1].Play();
        isGameStarted = false;
        StartCoroutine(OverPanel());
    }

    IEnumerator FinishPanel()
    {
        yield return new WaitForSeconds(3f);
        finishPanel.SetActive(true);
        gamePanel.SetActive(false);
        GetReward.instance.callFillBox();
        AddCoin(50);

    }

    IEnumerator OverPanel()
    {
        yield return new WaitForSeconds(3f);
        gameOverPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    // ========================== LEVEL SETUP

    private void LevelGenerate()
    {
        int i = level - 1;
        LevelGenerator.Instance.SpawnLevel(i);
        coinText.text = coin.ToString();
        levelText.text = "LEVEL " + level.ToString();
    }

    private void LevelUp()
    {
        level++;
        PlayerPrefs.SetInt("level", level);
    }

    public void SceneLoad()
    {
        SceneManager.LoadScene(0);
    }

    public void GetDatas()
    {
        // LEVEL
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        else
        {
            PlayerPrefs.SetInt("level", 1);
            level = 1;
        }

        // GEM
        if (PlayerPrefs.HasKey("coin"))
        {
            coin = PlayerPrefs.GetInt("coin");
        }
        else
        {
            PlayerPrefs.SetInt("coin", coin);
        }

        // SOUND
        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetInt("sound", 1);
        }
    }

    public void AddCoin(int newCoin)
    {
        sound[2].Play();
        int prevCoin = PlayerPrefs.GetInt("coin");
        PlayerPrefs.SetInt("coin", prevCoin + newCoin);
        coin = newCoin;
        coinText.text = coin.ToString();
    }

    // ========================== UI BUTTON

    public void StartButton()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        isGameStarted = true;
        PlayerControl.Instance.playerAnim.Play("Run");
        PlayerControl.Instance.confeti = GameObject.Find("Confeti").GetComponent<ParticleSystem>();
        for (int i = 0; i < finishBalon.Length; i++)
        {
            GameObject[] balon = GameObject.FindGameObjectsWithTag("Balon");
            finishBalon = balon;
        }
    }

    public void RestartButton()
    {
        //AdManager.Instance.ShowAdInterstitial();
    }

    // =============================================== *** END

}