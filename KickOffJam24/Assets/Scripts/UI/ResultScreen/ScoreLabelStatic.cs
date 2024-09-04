using TMPro;
using UnityEngine;

public class ScoreLabelStatic : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private LeaderBoard leaderBoard;

    [Header("Colors")]
    [SerializeField] private Color highScoreColor;

    private void Start()
    {
        if (ScoreManager.instance == null)
            return;

        bool highScore = leaderBoard.TryRegisterScore(ScoreManager.instance.score);

        label.text += ScoreManager.instance.score.ToString();
        if (highScore)
            label.color = highScoreColor;

        leaderBoard.ShowLeaderBoard();
    }
}
