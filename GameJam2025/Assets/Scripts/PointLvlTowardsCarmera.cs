using UnityEngine;

public class PointTowardsCamera : MonoBehaviour
{
    public Camera targetCamera;

    private PlayerControler thePlayer;
    [SerializeField] private Vector3 ofset;

    private void Awake()
    {
        thePlayer = FindFirstObjectByType<PlayerControler>();
        UpdatePositionRotation();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void LateUpdate()
    {
        UpdatePositionRotation();
    }

    /// <summary>
    /// Updates the position and rotation of the object to point towards the camera and follow the player
    /// </summary>
    private void UpdatePositionRotation()
    {
        transform.position = thePlayer.transform.position + ofset;
        transform.LookAt(targetCamera.transform);
        transform.rotation = Quaternion.LookRotation(targetCamera.transform.forward);
    }
}
