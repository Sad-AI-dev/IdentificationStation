using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Colors { Red, Blue, Green, Yellow }
public class SimonSays : Puzzle
{
    List<Colors> order;
    List<Colors> orderInput;

    [SerializeField] List<GameObject> buttons;
    [SerializeField] int StartingColors = 3;
    [SerializeField] int MaxOrderCount = 5;
    [SerializeField] float ShowColorTime = 0.6f;
    [SerializeField] float TimeBetweenShowingOrders = 5;

    [SerializeField] bool inputStarted;

    Camera cam;

    private void Start()
    {
        StartPuzzle();
        ResetInputAndPlay();
        cam = Camera.main;
    }
    private void Update()
    {
        ReadPlayerInput();



    }


    void StartPuzzle()
    {
        order.Clear();
        for (int i = 0; i < StartingColors; i++)
        {
            order.Add((Colors)Enum.ToObject(typeof(Colors), UnityEngine.Random.Range(0, 4)));
        }
    }
    IEnumerator ShowColors()
    {
        for (int i = 0; i < order.Count; i++)
        {
            if (!inputStarted)
            {
                //Turn on light
                yield return new WaitForSeconds(ShowColorTime);
                //Turn off light
            }
        }

        yield return new WaitForSeconds(TimeBetweenShowingOrders);

        if (!inputStarted)
        {
            StartCoroutine(ShowColors());
        }
    }
    void ReadPlayerInput()
    {
        Vector3 mousepos = Input.mousePosition;
        mousepos.z = 10f;
        mousepos = cam.ScreenToWorldPoint(mousepos);

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cam.transform.position, mousepos - cam.transform.position, out hit, Mathf.Infinity))
            {
                if (buttons.Contains(hit.transform.gameObject))
                {
                    inputStarted = true;

                    orderInput.Add((Colors)buttons.IndexOf(hit.transform.gameObject));

                    //Check if input wrong
                    if (order[orderInput.Count - 1] != orderInput[orderInput.Count - 1])
                    {
                        //Reset if not
                        ResetInputAndPlay();

                    }

                    //Check if sequence correct;
                    if (order.Count == orderInput.Count)
                    {
                        if (order.Count == MaxOrderCount)
                        {
                            OnPuzzleCompleted?.Invoke();
                            ResetPuzzle();
                        }
                        else
                        {
                            order.Add((Colors)Enum.ToObject(typeof(Colors), UnityEngine.Random.Range(0, 4)));
                            ResetInputAndPlay();
                        }
                    }
                }
            }
        }
    }
    private void ResetInputAndPlay()
    {
        inputStarted = false;
        orderInput.Clear();
        StartCoroutine(ShowColors());
    }

    private void ResetPuzzle()
    {
        orderInput.Clear();
        order.Clear();
    }

}
