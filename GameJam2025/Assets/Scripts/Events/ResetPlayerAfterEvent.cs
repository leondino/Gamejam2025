using System.Collections;
using UnityEngine;

public class ResetPlayerAfterEvent : MonoBehaviour
{
    [HideInInspector] public bool shouldReset;
    private float delay;
    private bool resetOldLocation = true, disableCamera = true;
    private Rigidbody rBody;
    private Animator animator;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        if (shouldReset)
        {
            Debug.Log("RESET PLAYER");
            this.shouldReset = false;
            ResetPlayer();
        }
    }
    private void ResetPlayer()
    {
        gameObject.layer = 7;
        gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        Camera.main.transform.parent.GetComponent<CameraFollow>().enabled = !disableCamera;

        StartCoroutine(SetPositionAfterDelay(resetOldLocation));
    }

    IEnumerator SetPositionAfterDelay(bool resetOldLocation)
    {
        Vector3 oldPosition = transform.position;
        yield return new WaitForSeconds(delay);
        if (resetOldLocation)
        {
            transform.position = oldPosition;
        }
        animator.SetBool("doesTPose", false);
        rBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rBody.linearVelocity = Vector3.zero;
        rBody.angularVelocity = Vector3.zero;
        rBody.rotation = Quaternion.identity;
        gameObject.layer = 0;
        Camera.main.transform.parent.GetComponent<CameraFollow>().enabled = true;
        PlayerControler.Instance.playerLvlManager.IncreaseLevel(Random.Range(1000, 9999));
    }
    public void TriggerReset(float delay, bool resetOldLocation, bool disableCamera)
    {
        this.delay = delay;
        //Debug.Log(resetOldLocation);
        this.resetOldLocation = resetOldLocation;
        this.disableCamera = disableCamera;
        shouldReset = true;
    }
}
