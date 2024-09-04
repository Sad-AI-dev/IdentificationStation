using UnityEngine;

public class ItemBobber : MonoBehaviour
{
    [SerializeField] private float amplitude;
    [SerializeField] private float bobRange;

    [Space]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 rotationVector;

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
        Vector3 toRotate = rotationVector * (rotationSpeed * Time.deltaTime);
        transform.Rotate(toRotate);
    }
}
