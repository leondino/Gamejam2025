using UnityEngine;
using UnityEngine.Events;

public class QuickTimeEventHandler : MonoBehaviour
{
    public UnityEvent OnEventSucceeded = new UnityEvent();
    public UnityEvent OnEventFailed = new UnityEvent();

    [SerializeField] private Canvas canvas;
    public GameObject quickTimeButton;
    private float timer;
    [SerializeField] private float quickTimeFrequency = 3f;
    [SerializeField] private int maxClicks = 10;
    public int currentClicks = 0;

    public float maxButtonScale, minButtonScale;
    [Range(1, 50)]
    public int buttonShrinkPercentageSpeed = 5;
    public float secondBeforeButtonShrink = 1f;
    private float baseButtonRadius;
    private bool eventCompleted = false;
    private PlayerControler thePlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        timer = quickTimeFrequency;
        canvas = FindAnyObjectByType<Canvas>();
        baseButtonRadius = quickTimeButton.GetComponent<RectTransform>().rect.width/2;
        thePlayer = FindAnyObjectByType<PlayerControler>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer < quickTimeFrequency)
        {
            timer += Time.deltaTime;
            if (timer >= quickTimeFrequency)
            {
                SpawnQuickTimeButton();
                timer = 0;
            }
        }

        // Check if the player has reached the maximum amount of clicks to complete the event
        if ((currentClicks >= maxClicks) &! eventCompleted)
        {
            EventSucceeded();
        }
    }

    private void EndQuickTimeEvent() 
    {
        timer = quickTimeFrequency;
        thePlayer.IsInQuickTimeEvent = false;
        thePlayer.playerInput.ActivateInput();
        eventCompleted = true;     
    }

    public void EventSucceeded()
    {
        EndQuickTimeEvent();
        OnEventSucceeded.Invoke();
        Debug.Log("Quick time event succeeded!");
    }

    public void EventFailed()
    {
        EndQuickTimeEvent();
        OnEventFailed.Invoke();
        Debug.Log("Quick time event FAILED!!!"); 
    }

    /// <summary>
    /// Spawns a quick time button at a random location on the screen
    /// with a random scale between min and max ButtonScale
    /// </summary>
    private void SpawnQuickTimeButton()
    {
        float ButtonScaler = Random.Range(minButtonScale, maxButtonScale);
        float buttonRadius = baseButtonRadius * ButtonScaler;
        Vector2 randomSpawnLocation = new Vector2(Random.Range(buttonRadius, Screen.width - buttonRadius), 
            Random.Range(buttonRadius, Screen.height - buttonRadius));
        GameObject newQuickTimeButton = Instantiate(quickTimeButton, randomSpawnLocation, new Quaternion(), canvas.transform);
        newQuickTimeButton.transform.localScale = new Vector3(ButtonScaler, ButtonScaler, 1);
        newQuickTimeButton.GetComponent<QuickTimeButton>().ButtonConstructor(this);
    }

    /// <summary>
    /// Starts the quick time event 
    /// </summary>
    private void StartQuickTimeEvent()
    {
        timer = 0;
        thePlayer.IsInQuickTimeEvent = true;
        thePlayer.playerInput.DeactivateInput();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!eventCompleted)
        {
            StartQuickTimeEvent();
        }
    }
}
