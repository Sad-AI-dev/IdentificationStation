using TMPro;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private TMP_Text multiplierLabel;

    private string scoreBaseString;
    private string multiplierBaseString;

    private void Start()
    {
        ScoreManager.instance.OnScoreChanged += ScoreManager_OnScoreChanged;
        ScoreManager.instance.OnMultiplierChanged += ScoreManager_OnMultiplierChanged;

        scoreBaseString = scoreLabel.text;
        scoreLabel.text = scoreBaseString + "0";

        multiplierBaseString = multiplierLabel.text;
        multiplierLabel.text = multiplierBaseString + "1";
    }

    private void OnDestroy()
    {
        ScoreManager.instance.OnScoreChanged -= ScoreManager_OnScoreChanged;
        ScoreManager.instance.OnMultiplierChanged -= ScoreManager_OnMultiplierChanged;
    }

    private void ScoreManager_OnScoreChanged(int newScore)
    {
        scoreLabel.text = scoreBaseString + newScore;
    }

    private void ScoreManager_OnMultiplierChanged(float newMultiplier)
    {
        multiplierLabel.text = multiplierBaseString + newMultiplier;
    }
}
