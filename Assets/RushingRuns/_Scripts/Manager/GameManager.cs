using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    // ========================== VARIABLES

    [Header("GameObjects")]
    [Space]
    [SerializeField] GameObject[] finishBalon;

    [Header("UI Panel")]
    [Space]
    public GameObject finishPanel;
    public TextMeshProUGUI rewardText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] TextMeshProUGUI levelText;
    //[SerializeField] Text coinText;

    [Header(("Variables"))]
    [Space]
    [HideInInspector] public bool isGameStarted;
    private int level;
    private int coin;

    [Space(5)]
    public TextMeshProUGUI coinsText;
    private int betValue;
    public GameObject betPanel;
    public GameObject infoPanel;
    public TMP_InputField betInput;
    public Button betBtn;

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
        betValue = 0;
        betPanel.SetActive(true);
        SetCoins();
    }

    private void SetCoins()
    {
        coinsText.text = AppRushing.coins.ToString();
    }

    // ========================== GAME EVENTS && PANEL

    public void FinishLevel()
    {
        AudioRushing.instance.PlaySound(3);
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
        AudioRushing.instance.PlaySound(4);
        isGameStarted = false;
        StartCoroutine(OverPanel());
    }

    IEnumerator FinishPanel()
    {
        yield return new WaitForSeconds(3f);
        finishPanel.SetActive(true);
        gamePanel.SetActive(false);
        AddCoin(1000);
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
        //coinText.text = coin.ToString();
        levelText.text = "LEVEL " + level.ToString();
    }

    private void LevelUp()
    {
        if ((AppRushing.selectedLevel + 1) > AppRushing.saveLevel && AppRushing.selectedLevel <= 15)
            AppRushing.saveLevel = (AppRushing.selectedLevel + 1);
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
        //AudioRushing.instance.PlaySound(1);
        newCoin = betValue > 0 ? (betValue * 2) : newCoin;
        rewardText.text = newCoin.ToString();
        AppRushing.coins += newCoin;
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

    public void OnHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Home");
        AudioRushing.instance.PlaySound(0);
    }

    public void OnLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Levels");
        AudioRushing.instance.PlaySound(0);
    }

    public void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
        AudioRushing.instance.PlaySound(0);
    }

    public void OnNext()
    {
        Time.timeScale = 1f;
        if (AppRushing.selectedLevel < 15)
        {
            AppRushing.selectedLevel++;
            SceneManager.LoadScene("Game");
        }
        else
        {
            AppRushing.selectedLevel = 0;
            SceneManager.LoadScene("Home");
        }
        AudioRushing.instance.PlaySound(0);
    }

    // =============================================== *** END

    public void OnBetInputChanged()
    {
        if (string.IsNullOrEmpty(betInput.text.Trim()) || betInput.text.Trim().EndsWith("0"))
        {
            betBtn.interactable = false;
            return;
        }
        else
            betBtn.interactable = true;
    }

    public void OnConfirm()
    {
        if (!string.IsNullOrEmpty(betInput.text.Trim()))
        {
            betValue = int.Parse(betInput.text.Trim());
            if (AppRushing.coins >= betValue)
            {
                AppRushing.coins -= betValue;
                SetCoins();
            }
            else
            {
                infoPanel.SetActive(true);
            }
        }
    }
}