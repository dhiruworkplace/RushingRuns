using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeRushing : MonoBehaviour
{
    public TextMeshProUGUI questTimer;
    public TextMeshProUGUI coinsText;
    public GameObject coinPanel;

    // Start is called before the first frame update
    void Start()
    {
        SetCoins();
        //InvokeRepeating(nameof(CheckQuest), 0f, 1f);
    }

    private void StopTimer()
    {
        CancelInvoke(nameof(CheckQuest));
        coinPanel.SetActive(true);
    }

    private void CheckQuest()
    {
        DateTime lastDT = new DateTime();
        if (!PlayerPrefs.HasKey("lastSpin"))
        {
            //PlayerPrefs.SetString("lastSpin", DateTime.Now.AddHours(24).ToString());
            //PlayerPrefs.Save();

            questTimer.text = "CLAIM BONUS";
            StopTimer();
            return;
        }
        lastDT = DateTime.Parse(PlayerPrefs.GetString("lastSpin"));

        TimeSpan diff = (lastDT - DateTime.Now);
        questTimer.text = string.Format("{0:D2}:{1:D2}:{2:D2}", diff.Hours, diff.Minutes, diff.Seconds);

        if (diff.TotalSeconds <= 0)
        {
            StopTimer();
            questTimer.text = "CLAIM BONUS";
        }
    }

    private void StartTimer()
    {
        PlayerPrefs.SetString("lastSpin", DateTime.Now.AddHours(24).ToString());
        PlayerPrefs.Save();

        InvokeRepeating(nameof(CheckQuest), 0f, 1f);
        coinPanel.SetActive(false);
    }

    public void ClaimBtn()
    {
        if (questTimer.text.Equals("CLAIM BONUS"))
        {
            AppRushing.coins += 1000;
            SetCoins();

            Invoke(nameof(StartTimer), 0f);
        }
    }

    public void Play()
    {
        AppRushing.selectedLevel = AppRushing.saveLevel;
        SceneManager.LoadScene("Game");
    }

    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }

    public void SetCoins()
    {
        coinsText.text = AppRushing.coins.ToString();
    }

    public void Click()
    {
        AudioRushing.instance.PlaySound(0);
    }
}