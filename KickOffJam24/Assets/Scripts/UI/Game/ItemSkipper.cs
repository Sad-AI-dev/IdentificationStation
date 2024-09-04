using DevKit;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemSkipper : MonoBehaviour
{
    public static event Action OnItemSkipped;

    public int itemSkips;
    [SerializeField] private Button button;

    public void SkipItem()
    {
        if (itemSkips <= 0)
            return;

        itemSkips--;
        OnItemSkipped?.Invoke();
        AudioManager.instance.Stop("Hum");

        if (itemSkips == 0)
            button.interactable = false;
    }
}
