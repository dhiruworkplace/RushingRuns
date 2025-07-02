using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public GameObject[] themes;
    private List<int> prices = new List<int>() { 10000, 20000, 30000, 40000, 50000, 60000 };

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("p0", 1);
        //PlayerPrefs.Save();

        CheckAllTheme();
    }

    private void CheckAllTheme()
    {
        for (int i = 0; i < themes.Length; i++)
        {
            GameObject m = themes[i];
            if (PlayerPrefs.GetInt("p" + i, 0) == 1)
            {
                m.transform.GetChild(3).gameObject.SetActive(true);

                //if (AppStairs.selectedTheme.Equals(i))
                //{
                //    m.transform.GetChild(3).gameObject.SetActive(true);
                //}
            }
        }
    }

    public void SelectTheme(int index)
    {
        if (PlayerPrefs.GetInt("p" + index, 0) == 1)
        {
            //if (!Container.selectedBg.Equals(index))
            {
                AppRushing.selectedTheme = index;
                CheckAllTheme();
            }
        }
        else
        {
            if (AppRushing.coins >= prices[index])
            {
                AppRushing.coins -= prices[index];
                themes[index].transform.GetChild(3).gameObject.SetActive(true);
                PlayerPrefs.SetInt("p" + index, 1);
                PlayerPrefs.Save();

                FindObjectOfType<HomeRushing>().SetCoins();
            }
        }
        AudioRushing.instance.PlaySound(0);
    }
}