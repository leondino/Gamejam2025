using System.Collections;
using UnityEngine;

public class ResetPlayerAfterEvent : MonoBehaviour
{
    [HideInInspector] public bool shouldReset;
    private float delay;
    [SerializeField]private bool resetOldLocation = true;
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
        animator.SetBool("doesTPose", true);

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
        rBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rBody.linearVelocity = Vector3.zero;
        rBody.angularVelocity = Vector3.zero;
        rBody.rotation = Quaternion.identity;
        animator.SetBool("doesTPose", false);
        gameObject.layer = 0;
    }
    public void TriggerReset(float delay, bool resetOldLocation)
    {
        this.delay = delay;
        //Debug.Log(resetOldLocation);
        this.resetOldLocation = resetOldLocation;
        shouldReset = true;
    }
}
