using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float followSpeed;
    public float rotationSpeed;

    private void LateUpdate() //update after all gamelogic
    {
        transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);
    }
}
