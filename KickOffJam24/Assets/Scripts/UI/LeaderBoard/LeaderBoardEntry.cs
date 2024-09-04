using TMPro;
using UnityEngine;

public class LeaderBoardEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text numLabel;
    [SerializeField] private TMP_Text scoreLabel;

    public void PopulateEntry(int num, int score)
    {
        numLabel.text = num + ".";
        scoreLabel.text = score.ToString();
    }

    public void ApplyColor(Color color)
    {
        numLabel.color = color;
        scoreLabel.color = color;
    }
}
