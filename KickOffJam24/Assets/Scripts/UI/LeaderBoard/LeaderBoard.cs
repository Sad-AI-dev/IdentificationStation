using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private bool showOnStart;
    [SerializeField] private LeaderBoardEntry entryPrefab;
    [SerializeField] private Transform entryHolder;

    [Header("Timings")]
    [SerializeField] private float showEntryDelay;

    [Header("Colors")]
    [SerializeField] private Color highScoreColor;

    private int registeredScore;

    private void Start()
    {
        if (showOnStart)
            StartCoroutine(ShowLeaderBoardCo());
    }

    public void ShowLeaderBoard()
    {
        StartCoroutine(ShowLeaderBoardCo());
    }

    // ========= Render Leaderboard =========
    private IEnumerator ShowLeaderBoardCo()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i > 0)
                yield return new WaitForSeconds(showEntryDelay);

            ShowLeaderBoardEntry(i);
        }
    }

    private void ShowLeaderBoardEntry(int index)
    {
        // Get Score
        string scoreAddress = "HighScore" + index;
        int highScore = PlayerPrefs.GetInt(scoreAddress);

        // Create Entry Object
        LeaderBoardEntry entry = Instantiate(entryPrefab, entryHolder);

        // Populate Entry
        entry.PopulateEntry(index + 1, highScore);

        if (registeredScore <= 0)
            return;

        if (registeredScore == highScore)
            entry.ApplyColor(highScoreColor);
    }

    // Register Score
    public bool TryRegisterScore(int score)
    {
        bool success = false;

        // Compile Leaderboard
        List<int> highScores = new(5);

        for (int i = 0; i < 5; i++)
            highScores.Add(PlayerPrefs.GetInt("HighScore" + i));

        // Update Leaderboard
        for (int i = 0; i < 5; i++)
        {
            if (highScores[i] >= score)
                continue;

            highScores.Insert(i, score);
            registeredScore = score;
            success = true;
            break;
        }

        // Re-compile Leaderboard
        for (int i = 0; i < 5; i++)
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);

        return success;
    }
}
