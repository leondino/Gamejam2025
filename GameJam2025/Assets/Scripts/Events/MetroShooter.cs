using UnityEngine;

public class MetroShooter : FailEvent
{
    private PlayerControler thePlayer;
    private bool hasShotPlayer = false;

    [SerializeField] private Vector3 shootDirection;
    [SerializeField] private float shootForce;

    public override void FailAction()
    {
        base.FailAction();
        PushPlayerIn();
    }

    public void PushPlayerIn()
    {
        thePlayer = PlayerControler.Instance;
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
            if (!hasShotPlayer)
            {
                hasShotPlayer = true;
                ShootPlayerOut();
                Debug.Log("Player has been shot out of the metro");
            }
        }
    }
}
