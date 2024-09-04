using TMPro;
using UnityEngine;

public class ScoreLabelStatic : MonoBehaviour
{
    [SerializeField] private TMP_Text label;

    private void Start()
    {
        if (ScoreManager.instance == null)
            return;

        label.text += ScoreManager.instance.score.ToString();
    }
}
