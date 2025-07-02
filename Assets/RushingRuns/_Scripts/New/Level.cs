using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public int levelNo;
    public TextMeshProUGUI levelNoText;
    public GameObject lockObj;

    // Start is called before the first frame update
    void Start()
    {
        levelNo = transform.GetSiblingIndex() + 1;
        levelNoText.text = levelNo.ToString("00");

        lockObj.SetActive(AppRushing.saveLevel < levelNo);
    }

    public void OnClick()
    {
        if (!lockObj.activeSelf)
        {
            AppRushing.selectedLevel = levelNo;
            SceneManager.LoadScene("Game");
            AudioRushing.instance.PlaySound(0);
        }
    }
}