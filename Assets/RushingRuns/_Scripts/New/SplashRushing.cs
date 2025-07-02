using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashRushing : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(ChangePage), 2.5f);
    }

    private void ChangePage()
    {
        SceneManager.LoadScene("Home");
    }
}