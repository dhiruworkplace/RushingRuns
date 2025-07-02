using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsRushing : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("Home");
        AudioRushing.instance.PlaySound(0);
    }
}