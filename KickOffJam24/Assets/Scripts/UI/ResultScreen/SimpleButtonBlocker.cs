using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SimpleButtonBlocker : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private float unlockDelay;

    private IEnumerator Start()
    {
        button.interactable = false;
        yield return new WaitForSeconds(unlockDelay);
        button.interactable = true;
    }
}
