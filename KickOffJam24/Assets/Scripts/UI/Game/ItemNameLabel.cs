using System.Collections;
using TMPro;
using UnityEngine;

public class ItemNameLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text label;

    [Header("Timings")]
    [SerializeField] private float textAppearDelay;

    [Header("Colors")]
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color successColor;

    private ItemData itemData;
    private int puzzlesCompleted;

    private void Awake()
    {
        Item.OnItemCreated += Item_OnItemCreated;
        Item.OnItemCompleted += Item_OnItemCompleted;
        Puzzle.OnPuzzleCompleted += Puzzle_OnPuzzleCompleted;
        ItemSkipper.OnItemSkipped += ItemSkipper_OnItemSkipped;
    }

    private void OnDestroy()
    {
        Item.OnItemCreated -= Item_OnItemCreated;
        Item.OnItemCompleted -= Item_OnItemCompleted;
        Puzzle.OnPuzzleCompleted -= Puzzle_OnPuzzleCompleted;
        ItemSkipper.OnItemSkipped -= ItemSkipper_OnItemSkipped;
    }

    private void Item_OnItemCreated(ItemData itemData)
    {
        this.itemData = itemData;
        StartCoroutine(ItemCreatedCo());
    }

    private IEnumerator ItemCreatedCo()
    {
        label.text = "";
        puzzlesCompleted = 0;

        yield return new WaitForSeconds(textAppearDelay);

        string itemName = itemData.name + " of ";

        for (int i = 0; i < itemData.adjactives.Length; i++)
            itemName += "??? ";

        itemName += "???";

        label.text = itemName;
        label.color = defaultColor;
    }

    private void Item_OnItemCompleted()
    {
        label.color = successColor;

    }

    private void Puzzle_OnPuzzleCompleted()
    {
        puzzlesCompleted++;

        string namePieceToAdd = "";

        // Find next piece to add to name
        if (puzzlesCompleted <= itemData.adjactives.Length)
            namePieceToAdd = itemData.adjactives[puzzlesCompleted - 1];
        else
            namePieceToAdd = itemData.noun;

        //add next piece to name
        string[] namePieces = label.text.Split(' ');

        namePieces[puzzlesCompleted + 1] = namePieceToAdd;

        //re-construct name
        string newName = "";

        for (int i = 0; i < namePieces.Length - 1; i++)
            newName += namePieces[i] + " ";

        newName += namePieces[^1];
        label.text = newName;
    }

    private void ItemSkipper_OnItemSkipped()
    {
        puzzlesCompleted = 0;
    }
}
