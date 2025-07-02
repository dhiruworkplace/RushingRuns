using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreboardRushing : MonoBehaviour
{
    public Transform parent;
    public PlayerDetail playerDetail;
    public Sprite panelImg;

    public List<string> names = new List<string>()
    {
        "John Doe",
        "Jane Smith",
        "Michael Johnson",
        "Emily Davis",
        "Daniel Martinez",
        "Sophia Hernandez",
        "David Brown",
        "Olivia Wilson",
        "James Garcia",
        "Emma Rodriguez",
        "Alexander Lee",
        "Isabella White",
        "Christopher Thomas",
        "Mia Lopez",
        "Matthew Walker",
        "Ava Hall",
        "Joshua Allen",
        "Charlotte Young",
        "Ethan King",
        "Abigail Scott",
        "Benjamin Green",
        "Madison Adams",
        "Samuel Baker",
        "Grace Nelson",
        "Andrew Clark",
        "Chloe Lewis",
        "Logan Perez",
        "Sofia Sanchez",
        "Jacob Roberts",
        "Avery Turner",
        "Ryan Parker",
        "Amelia Collins",
        "Nathan Edwards",
        "Ella Stewart",
        "Caleb Morris",
        "Lily Hughes",
        "Jack Foster",
        "Harper Mitchell",
        "Henry Morgan",
        "Scarlett Rivera",
        "Lucas Cook",
        "Zoe Bell",
        "Owen Russell",
        "Nora Sanders",
        "Mason Peterson",
        "Ella Morris",
        "Aiden Rogers",
        "Camila Reed",
        "Jackson Cooper",
        "Layla Bailey"
    };

    // Start is called before the first frame update
    void OnEnable()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        List<string> nameList = names.OrderBy(x => Random.value).ToList();

        List<PlayerData> players = new List<PlayerData>();
        PlayerData me = new PlayerData();
        me.me = true;
        me.name = "You";
        me.points = AppRushing.coins;
        players.Add(me);

        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
        int min = 100;
        int max = 999;
        if (me.points > 10)
        {
            min = (me.points - 5);
            max = (me.points + 5);
        }

        for (int i = 0; i < 9; i++)
        {
            PlayerData player = new PlayerData();
            player.me = false;
            player.name = nameList[i];
            player.points = Random.Range(min, max);
            players.Add(player);
        }
        players = players.OrderByDescending(x => x.points).ToList();

        for (int i = 0; i < 10; i++)
        {
            PlayerDetail pd = Instantiate(playerDetail, parent);
            pd.SetData(players[i].me, (i + 1), players[i].name, players[i].points);

            if (players[i].me)
            {
                pd.panel.sprite = panelImg;
            }
        }
    }
}

public class PlayerData
{
    public bool me;
    public string name;
    public int points;
}