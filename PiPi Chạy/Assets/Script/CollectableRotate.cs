using UnityEngine;

public class CollectableRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.5f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0, Space.World);
    }
}
