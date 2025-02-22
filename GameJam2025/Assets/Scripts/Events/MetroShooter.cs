using UnityEngine;

public class MetroShooter : FailEvent
{
    private PlayerControler thePlayer;
    private Rigidbody playerRB;

    [SerializeField] private Vector3 shootDirection;
    [SerializeField] private float shootForce;

    private void Awake()
    {
        thePlayer = PlayerControler.Instance;
    }

    public override void FailAction()
    {
        base.FailAction();
        PushPlayerIn();
    }

    public void PushPlayerIn()
    {
        thePlayer.playerRB.AddForce(Vector3.left * 10, ForceMode.Impulse);
    }

    public void ShootPlayerOut()
    {
        thePlayer.animator.SetBool("doesTPose", true);
        thePlayer.playerRB.AddForce(shootDirection * shootForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControler>())
        {
            ShootPlayerOut();
            Debug.Log("Player has been shot out of the metro");
        }
    }
}
