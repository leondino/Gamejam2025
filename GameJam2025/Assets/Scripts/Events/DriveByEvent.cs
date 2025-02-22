using System.Collections;
using UnityEngine;

public class DriveByEvent : FailEvent
{
    public override void FailAction()
    {
        base.FailAction();
        CrashCar();
    }

    public override void SuccesAction()
    {
        base.SuccesAction();
        PlayerControler.Instance.transform.position += Vector3.forward * 2.5f;
        CrashCar();
    }

    public void CrashCar()
    {
        Rigidbody carRb = GetComponent<Rigidbody>();
        Vector3 forceDirection = transform.forward; // Apply force in the forward direction
        float forceAmount = 1000f; // Adjust this value for more or less force
        
        carRb.AddForce(forceDirection * forceAmount, ForceMode.Impulse);
    }
}
