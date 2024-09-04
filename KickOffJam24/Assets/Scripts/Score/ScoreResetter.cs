using UnityEngine;

public class ScoreResetter : MonoBehaviour
{
    public void ResetScore()
    {
        if (ScoreManager.instance == null)
            return;

        ScoreManager.instance.ResetScore();
    }
}
