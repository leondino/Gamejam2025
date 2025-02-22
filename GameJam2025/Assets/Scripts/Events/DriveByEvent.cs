using System.Collections;
using UnityEngine;

public class DriveByEvent : MonoBehaviour
{
    [SerializeField] public float delay = 3f;
    public void CrashCar()
    {
        Rigidbody carRb = GetComponent<Rigidbody>();
        ResetPlayerAfterEvent playerReset = FindAnyObjectByType<ResetPlayerAfterEvent>();
        Vector3 forceDirection = transform.forward; // Apply force in the forward direction
        float forceAmount = 1000f; // Adjust this value for more or less force
        
        carRb.AddForce(forceDirection * forceAmount, ForceMode.Impulse);
        playerReset.TriggerReset(delay);
    }
}
