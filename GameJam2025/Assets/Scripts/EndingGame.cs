using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EndingGame : MonoBehaviour
{
    [SerializeField] private float secsShowingFace, secsShowingSold;
    public Transform playerEndCameraPosition;
    public GameObject Cameras, UICamera;
    public GameObject milkSoldOverlay;

    public void StartGameEnding()
    {
        Destroy(UICamera);
        PlayerControler.Instance.playerInput.DeactivateInput();
        PlayerControler.Instance.animator.enabled = false;
        Cameras.GetComponent<HideObjectsFromCamera>().enabled = false;
        Cameras.GetComponent<CameraFollow>().enabled = false;
        Cameras.transform.position = playerEndCameraPosition.position;
        Cameras.transform.rotation = playerEndCameraPosition.rotation;
        Camera.main.orthographicSize = 0.05f;
        Camera.main.nearClipPlane = 0.1f;
        StartCoroutine(ShowMilkSold());
    }

    IEnumerator ShowMilkSold()
    {
        yield return new WaitForSeconds(secsShowingFace);
        milkSoldOverlay.SetActive(true);
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(secsShowingSold);
        Application.Quit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControler>())
        {
            StartGameEnding();
        }
    }
}
