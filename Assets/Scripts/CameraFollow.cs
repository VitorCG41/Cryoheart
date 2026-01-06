using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;
    public float speed = 15f;
    public Vector3 offset;

    void Update()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = -10f;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            speed * Time.deltaTime
        );
    }
}
