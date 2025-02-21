using UnityEngine;

public class MetroShooter : MonoBehaviour
{
    private GameObject thePlayer;
    private Rigidbody playerRB;

    [SerializeField] private Vector3 shootDirection;
    [SerializeField] private float shootForce;

    private void Awake()
    {
        thePlayer = FindAnyObjectByType<PlayerControler>().gameObject;
        playerRB = thePlayer.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void PushPlayerIn()
    {
        thePlayer.layer = 7;
        playerRB.AddForce(Vector3.left * 10, ForceMode.Impulse);
        playerRB.constraints = RigidbodyConstraints.None; 
        //RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; //RigidbodyConstraints.None;
    }

    public void ShootPlayerOut()
    {
        thePlayer.GetComponent<Animator>().SetBool("doesTPose", true);
        playerRB.AddForce(shootDirection * shootForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        ShootPlayerOut();
        Debug.Log("Player has been shot out of the metro");
    }
}
