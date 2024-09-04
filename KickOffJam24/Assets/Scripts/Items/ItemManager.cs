using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private ItemBuilder itemBuilder;

    [Header("Settings")]
    [SerializeField] private GameObject itemPrefab;

    [Space]
    [SerializeField] private Transform itemSpawnPoint;
    [SerializeField] private Transform itemFocusPoint;
    [SerializeField] private Transform itemDespawnPoint;

    [Header("Item Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private AnimationCurve moveToCenterCurve;
    [SerializeField] private AnimationCurve moveToEndCurve;

    private Item activeItem;

    private bool itemIsMoving;
    private bool itemIsComplete;

    private float itemMovementProgress;

    private void Awake()
    {
        Item.OnItemCompleted += Item_OnItemCompleted;
        ItemSkipper.OnItemSkipped += ItemSkipper_OnItemSkipped;
    }

    private void Start()
    {
        SpawnNextItem();
    }

    private void OnDestroy()
    {
        Item.OnItemCompleted -= Item_OnItemCompleted;
        ItemSkipper.OnItemSkipped -= ItemSkipper_OnItemSkipped;
    }

    private void SpawnNextItem()
    {
        GameObject obj = Instantiate(itemPrefab, itemSpawnPoint.position, Quaternion.identity);
        activeItem = obj.GetComponent<Item>();

        activeItem.Init(itemBuilder.BuildItemData());

        itemIsMoving = true;
        itemMovementProgress = 0;
    }

    //======== Item Movement ==========
    private void Update()
    {
        if (!itemIsMoving)
            return;

        MoveItem();
    }

    private void MoveItem()
    {
        if (itemIsComplete)
            MoveItemToEnd();
        else
            MoveItemToCenter();
    }

    private void MoveItemToCenter()
    {
        float lerpProgress = moveToCenterCurve.Evaluate(itemMovementProgress);
        Vector3 itemPos = Vector3.Lerp(itemSpawnPoint.position, itemFocusPoint.position, lerpProgress);
        activeItem.transform.position = itemPos;

        itemMovementProgress += moveSpeed * Time.deltaTime;

        if (itemMovementProgress < 0.99f)
            return;

        activeItem.transform.position = itemFocusPoint.position;
        HandleItemArriveCenter();
    }

    private void HandleItemArriveCenter()
    {
        itemIsMoving = false;

        activeItem.transform.position = itemFocusPoint.position;

        //Spawn first puzzle
        activeItem.StartPuzzles();
    }

    private void MoveItemToEnd()
    {
        float lerpProgress = moveToEndCurve.Evaluate(itemMovementProgress);
        Vector3 itemPos = Vector3.Lerp(itemFocusPoint.position, itemDespawnPoint.position, lerpProgress);
        activeItem.transform.position = itemPos;

        itemMovementProgress += moveSpeed * Time.deltaTime;

        if (itemMovementProgress < 0.99f)
            return;

        HandleItemDespawn();
    }

    private void HandleItemDespawn()
    {
        itemIsMoving = false;
        itemIsComplete = false;

        Destroy(activeItem.gameObject);
        activeItem = null;

        SpawnNextItem();
    }

    // ============ Handle Item Completion ============
    private void Item_OnItemCompleted()
    {
        itemMovementProgress = 0;
        itemIsComplete = true;
        itemIsMoving = true;
    }

    // ========== Skip Item ===========
    public void ItemSkipper_OnItemSkipped()
    {
        if (activeItem == null)
            return;



        activeItem.SkipItem();
        Item_OnItemCompleted();
    }
}
