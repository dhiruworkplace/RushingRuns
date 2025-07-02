using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private void OnEnable()
    {
        AudioRushing.instance.PlaySound(0);
        Time.timeScale = 0f;
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Home");
        AudioRushing.instance.PlaySound(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
        AudioRushing.instance.PlaySound(0);
    }

    public void Continue()
    {
        AudioRushing.instance.PlaySound(0);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}