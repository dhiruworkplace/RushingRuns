using UnityEngine;

public static class AppRushing
{
    public static int selectedTheme;
    public static int selectedLevel = 1;

    private static int _tempScore;
    public static int tempScore
    {
        get { return _tempScore; }
        set
        {
            _tempScore = value;
            if (value > score)
                score = value;
        }
    }

    public static int score
    {
        get { return PlayerPrefs.GetInt("score", 0); }
        set { PlayerPrefs.SetInt("score", value); }
    }

    public static int saveLevel
    {
        get { return PlayerPrefs.GetInt("savelevel", 1); }
        set
        {
            PlayerPrefs.SetInt("savelevel", value);
            PlayerPrefs.Save();
        }
    }

    public static int music
    {
        get { return PlayerPrefs.GetInt("music", 1); }
        set { PlayerPrefs.SetInt("music", value); }
    }

    public static int sound
    {
        get { return PlayerPrefs.GetInt("sound", 1); }
        set { PlayerPrefs.SetInt("sound", value); }
    }

    public static string name
    {
        get { return PlayerPrefs.GetString("name", "Guest" + Random.Range(1111, 9999)); }
        set { PlayerPrefs.SetString("name", value); }
    }

    public static int coins
    {
        get { return PlayerPrefs.GetInt("coins", 1000); }
        set
        {
            PlayerPrefs.SetInt("coins", value);
            tempScore = value;
        }
    }
}