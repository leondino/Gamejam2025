using UnityEngine;

public class MeteoriteSmash : FailEvent
{
    private Rigidbody rBody;
    private Vector3 hitDirection = new Vector3(0, 0, 0);
    [SerializeField] private float hitForce = 10;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public override void FailAction()
    {
        base.FailAction();
        HitPlayer();
    }

    public void HitPlayer()
    {     
        hitDirection = PlayerControler.Instance.transform.position - transform.position;
        hitDirection.Normalize();
        rBody.useGravity = true;
        rBody.AddForce(hitDirection * hitForce, ForceMode.Impulse);
    }
}
