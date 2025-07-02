using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetail : MonoBehaviour
{
    public bool me;
    public Image panel;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI points;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetData(bool me, int _rank, string name, int _points)
    {
        playerName.text = name;
        points.text = _points.ToString();
    }
}