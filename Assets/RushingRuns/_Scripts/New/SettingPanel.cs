using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    [Space(5)]
    public GameObject musicOn, musicOff;
    public GameObject soundOn, soundOff;

    private void Start()
    {
        soundOn.SetActive(AppRushing.sound.Equals(1));
        musicOn.SetActive(AppRushing.music.Equals(1));
        soundOff.SetActive(AppRushing.sound.Equals(0));
        musicOff.SetActive(AppRushing.music.Equals(0));
    }

    public void SetMusic()
    {
        if (AppRushing.music.Equals(1))
        {
            AppRushing.music = 0;
            AudioRushing.instance.PauseMusic();
        }
        else
        {
            AppRushing.music = 1;
            AudioRushing.instance.PlayMusic();
        }
        musicOn.SetActive(AppRushing.music.Equals(1));
        musicOff.SetActive(AppRushing.music.Equals(0));
        AudioRushing.instance.PlaySound(0);
    }

    public void SetSound()
    {
        if (AppRushing.sound.Equals(1))
        {
            AppRushing.sound = 0;
        }
        else
        {
            AppRushing.sound = 1;
        }
        soundOn.SetActive(AppRushing.sound.Equals(1));
        soundOff.SetActive(AppRushing.sound.Equals(0));
        AudioRushing.instance.PlaySound(0);
    }
}