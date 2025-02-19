using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private PlayerControler thePlayer;
    [SerializeField] private Vector3 ofset;

    private void Awake()
    {
        thePlayer = FindFirstObjectByType<PlayerControler>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = thePlayer.transform.position + ofset;
    }
}
