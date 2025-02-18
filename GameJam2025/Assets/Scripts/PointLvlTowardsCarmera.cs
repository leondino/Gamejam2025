using UnityEngine;

public class PointTowardsCamera: MonoBehaviour
{
    public Camera targetCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void LateUpdate()
    {
        transform.LookAt(targetCamera.transform);
        transform.rotation = Quaternion.LookRotation(targetCamera.transform.forward);
    }
}
