using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ResetPlayerAfterEvent : MonoBehaviour
{
    public bool shouldReset;
    public float delay;
    public void FixedUpdate()
    {
        if (shouldReset)
        {   Debug.Log("RESET PLAYER");
            this.ResetPlayer();
            this.shouldReset = false;
        }
    }
    
    public void ResetPlayer()
    {
        PlayerControler thePlayer = FindAnyObjectByType<PlayerControler>();
        thePlayer.gameObject.layer = 7;
        thePlayer.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        Vector3 playerPosition = thePlayer.transform.position;
        
        StartCoroutine(SetPositionAfterDelay(playerPosition));
    }
    
    IEnumerator SetPositionAfterDelay(Vector3 newPosition)
    {
        yield return new WaitForSeconds(delay);
        transform.position = newPosition;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void TriggerReset(float delay)
    {
        this.delay = delay;
        shouldReset = true;
    }
}
