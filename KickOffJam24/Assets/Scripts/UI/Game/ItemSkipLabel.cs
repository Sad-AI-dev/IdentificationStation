using TMPro;
using UnityEngine;

public class ItemSkipLabel : MonoBehaviour
{
    [SerializeField] private ItemSkipper itemSkipper;
    [SerializeField] private TMP_Text label;

    private int totalItemSkips;

    private void Awake()
    {
        ItemSkipper.OnItemSkipped += ItemSkipper_OnItemSkipped;
    }

    private void Start()
    {
        totalItemSkips = itemSkipper.itemSkips;
        label.text = $"{totalItemSkips} / {totalItemSkips}";
    }

    private void OnDestroy()
    {
        ItemSkipper.OnItemSkipped -= ItemSkipper_OnItemSkipped;
    }

    private void ItemSkipper_OnItemSkipped()
    {
        label.text = $"{itemSkipper.itemSkips} / {totalItemSkips}";
    }
}
