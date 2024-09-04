using DevKit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Colors { Red, Blue, Green, Yellow }
public class SimonSays : Puzzle
{
    List<Colors> order = new List<Colors>();
    List<Colors> orderInput = new List<Colors>();

    [SerializeField] List<GameObject> buttons;
    [SerializeField] List<Material> buttonMaterials;
    [SerializeField] List<Light> buttonLights;
    [SerializeField] int StartingColors = 3;
    [SerializeField] int MaxOrderCount = 5;
    [SerializeField] float ShowColorTime = 0.6f;

    bool inputStarted;
    bool canInput = false;

    Camera cam;

    private void OnDestroy()
    {
        for (int i = 0; i < buttonMaterials.Count; i++)
        {
            buttonMaterials[i].DisableKeyword("_EMISSION");
        }
    }

    private void Awake()
    {
        cam = Camera.main;
    }

    private IEnumerator Start()
    {
        yield return null;
        // Turn off emission
        for (int i = 0; i < buttonMaterials.Count; i++)
            buttonMaterials[i].DisableKeyword("_EMISSION");

        yield return new WaitForSeconds(1);

        StartPuzzle();
        ResetInputAndPlay();
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
        canInput = false;
        for (int i = 0; i < order.Count; i++)
        {
            if (!inputStarted)
            {
                //Turn on light
                buttonMaterials[(int)order[i]].EnableKeyword("_EMISSION");
                buttonLights[(int)order[i]].gameObject.SetActive(true);
                AudioManager.instance.PlayOneShot(order[i].ToString());

                yield return new WaitForSeconds(ShowColorTime);
                buttonMaterials[(int)order[i]].DisableKeyword("_EMISSION");
                buttonLights[(int)order[i]].gameObject.SetActive(false);


                yield return new WaitForSeconds(0.2f);
                //Turn off light
            }
        }
        canInput = true;
    }

    IEnumerator BlinkLight(int index)
    {
        buttonMaterials[index].EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.1f);
        buttonMaterials[index].DisableKeyword("_EMISSION");
    }

    void ReadPlayerInput()
    {
        Vector3 mousepos = Input.mousePosition;
        mousepos.z = 10f;
        mousepos = cam.ScreenToWorldPoint(mousepos);

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0) && canInput)
        {
            if (Physics.Raycast(cam.transform.position, mousepos - cam.transform.position, out hit, Mathf.Infinity))
            {
                if (buttons.Contains(hit.transform.gameObject))
                {
                    inputStarted = true;

                    StartCoroutine(BlinkLight(buttons.IndexOf(hit.transform.gameObject)));
                    AudioManager.instance.PlayOneShot(((Colors)buttons.IndexOf(hit.transform.gameObject)).ToString());
                    orderInput.Add((Colors)buttons.IndexOf(hit.transform.gameObject));

                    //Check if input wrong
                    if (order[orderInput.Count - 1] != orderInput[orderInput.Count - 1])
                    {
                        //Reset if not
                        OnMistakeMade?.Invoke();
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
                            Invoke(nameof(ResetInputAndPlay), 2);
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
