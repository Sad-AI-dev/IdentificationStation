using UnityEngine;

public class ItemBobber : MonoBehaviour
{
    [SerializeField] private float amplitude;
    [SerializeField] private float bobRange;
    [SerializeField] private float rotationSpeed;

    private float baseYPos;

    private void Start()
    {
        baseYPos = transform.position.y;
    }

    private void Update()
    {
        Bob();
        Rotate();
    }

    private void Bob()
    {
        float yOffset = Mathf.Sin(Time.time * amplitude) * bobRange;
        transform.position = new(transform.position.x, baseYPos + yOffset, transform.position.z);
    }

    private void Rotate()
    {
        transform.Rotate(new(0, rotationSpeed * Time.deltaTime, 0));
    }
}
