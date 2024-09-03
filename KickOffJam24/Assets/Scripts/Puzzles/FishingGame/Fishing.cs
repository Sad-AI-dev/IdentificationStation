using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : Puzzle
{
    [SerializeField] RectTransform target, playerBar;
    [SerializeField] Slider CompletionSlider;

    [SerializeField] int min, max;
    [SerializeField] float barSpeed = 8;
    [SerializeField] float targetSpeed = 6;
    [SerializeField] float progressionSpeed = 2.5f;
    [SerializeField] float regressionSpeed = 1;

    bool completed = false;

    Vector3 target_targetPosition;
    Coroutine targetPos;

    private void Start()
    {
        targetPos = StartCoroutine(targetChangePos());
    }

    private void Update()
    {
        if (CompletionSlider.value >= CompletionSlider.maxValue)
        {
            OnPuzzleCompleted?.Invoke();
            completed = true;
        }
    }

    private void FixedUpdate()
    {
        if (completed) { return; }

        if (Input.GetMouseButton(0))
        {
            if (playerBar.localPosition.y < max)
            {
                playerBar.transform.localPosition += new Vector3(0, barSpeed, 0);
            }
        }
        else
        {
            if (playerBar.localPosition.y > min)
            {
                playerBar.transform.localPosition -= new Vector3(0, barSpeed, 0);
            }
        }

        if (target.localPosition != target_targetPosition)
        {
            if (target_targetPosition.y > target.localPosition.y)
            {
                target.localPosition += new Vector3(0, targetSpeed, 0);
            }
            if (target_targetPosition.y < target.localPosition.y)
            {
                target.localPosition += new Vector3(0, -targetSpeed, 0);
            }
        }

        if (isOverlapping()) CompletionSlider.value += progressionSpeed;
        else { CompletionSlider.value -= regressionSpeed; }
    }

    bool isOverlapping()
    {
        //Check Top
        if (target.transform.localPosition.y < playerBar.transform.localPosition.y + playerBar.rect.height / 2 &&
            target.transform.localPosition.y > playerBar.transform.localPosition.y - playerBar.rect.height / 2)
        {
            return true;
        }
        return false;
    }

    IEnumerator targetChangePos()
    {
        int pos = Random.Range(min, max);
        target_targetPosition = new Vector3(0, pos, 0);
        yield return new WaitForSeconds(Random.Range(1.5f, 3f));
        targetPos = StartCoroutine(targetChangePos());
    }
}
