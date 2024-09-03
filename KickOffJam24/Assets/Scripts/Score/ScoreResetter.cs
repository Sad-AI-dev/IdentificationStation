using UnityEngine;

public class ScoreResetter : MonoBehaviour
{
    public void ResetScore()
    {
        ScoreManager.instance.ResetScore();
    }
}
