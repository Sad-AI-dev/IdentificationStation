using TMPro;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private TMP_Text multiplierLabel;

    private void Awake()
    {
        ScoreManager.instance.OnScoreChanged += ScoreManager_OnScoreChanged;
        ScoreManager.instance.OnMultiplierChanged += ScoreManager_OnMultiplierChanged;
    }

    private void OnDestroy()
    {
        ScoreManager.instance.OnScoreChanged -= ScoreManager_OnScoreChanged;
        ScoreManager.instance.OnMultiplierChanged -= ScoreManager_OnMultiplierChanged;
    }

    private void ScoreManager_OnScoreChanged(int newScore)
    {
        scoreLabel.text = newScore.ToString();
    }

    private void ScoreManager_OnMultiplierChanged(float newMultiplier)
    {
        multiplierLabel.text = newMultiplier.ToString();
    }
}
