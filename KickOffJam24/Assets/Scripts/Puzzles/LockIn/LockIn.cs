using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockIn : Puzzle
{
    [Header("Player Bar")]
    [SerializeField] List<RectTransform> PlayerBars;
    [SerializeField] int PlayerBarMin, PlayerBarMax;
    [SerializeField] float playerBarSpeed = 5;
    bool goUp;

    [Header("Target")]
    [SerializeField] List<RectTransform> Targets;
    [SerializeField] int TargetMin, TargetMax;

    bool done = false;


    int currentLock;

    void Start()
    {
        foreach (var target in Targets)
        {
            target.transform.localPosition = new Vector3(0, Random.Range(TargetMin, TargetMax), 0);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isOverlapping())
            {
                if (currentLock == Targets.Count - 1)
                {
                    OnPuzzleCompleted?.Invoke();
                    done = true;
                    return;
                }

                currentLock++;
                playerBarSpeed *= 1.5f;
            }
            else
            {
                OnMistakeMade?.Invoke();
                StartCoroutine(resetBar());
            }
        }
    }

    IEnumerator resetBar()
    {
        done = true;
        goUp = true;
        PlayerBars[currentLock].transform.localPosition = new Vector3(0, PlayerBarMin, 0);
        yield return new WaitForSeconds(0.5f);
        done = false;
    }

    bool isOverlapping()
    {
        //Check Top
        if (PlayerBars[currentLock].transform.localPosition.y < Targets[currentLock].transform.localPosition.y + Targets[currentLock].rect.height / 2 &&
            PlayerBars[currentLock].transform.localPosition.y > Targets[currentLock].transform.localPosition.y - Targets[currentLock].rect.height / 2)
        {
            return true;
        }
        return false;
    }

    private void FixedUpdate()
    {
        if (!done)
        {

            if (goUp)
            {
                PlayerBars[currentLock].localPosition += new Vector3(0, playerBarSpeed, 0);
                if (PlayerBars[currentLock].localPosition.y >= PlayerBarMax) goUp = false;
            }
            else
            {
                PlayerBars[currentLock].localPosition -= new Vector3(0, playerBarSpeed, 0);
                if (PlayerBars[currentLock].localPosition.y <= PlayerBarMin) goUp = transform;
            }
        }
    }


}
