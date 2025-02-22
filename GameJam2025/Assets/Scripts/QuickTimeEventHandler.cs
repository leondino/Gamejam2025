using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class QuickTimeEventHandler : MonoBehaviour
{
    public UnityEvent OnEventSucceeded = new UnityEvent();
    public UnityEvent OnEventFailed = new UnityEvent();

    private Canvas canvas;
    [SerializeField] private string eventDescription;
    [SerializeField] private float descriptionDisplayTime = 3f;
    private bool descriptionBeenDisplayed = false;
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
    [HideInInspector] public bool eventCompleted = false;
    private PlayerControler thePlayer;

    [SerializeField] private TextMeshProUGUI quickTimeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // If somehow the description display time is less than the quick time frequency, set it to the quick time frequency
        if (descriptionDisplayTime < quickTimeFrequency)
        {
            descriptionDisplayTime = quickTimeFrequency;
        }
        timer = descriptionDisplayTime;
        canvas = FindAnyObjectByType<Canvas>();
        baseButtonRadius = quickTimeButton.GetComponent<RectTransform>().rect.width/2;
        thePlayer = FindAnyObjectByType<PlayerControler>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer < descriptionDisplayTime)
        {
            timer += Time.deltaTime;
            if (timer >= quickTimeFrequency && descriptionBeenDisplayed)
            {
                SpawnQuickTimeButton();
                timer = 0;
            }

            if (timer >= descriptionDisplayTime &! descriptionBeenDisplayed)
            {
                RemoveDescription();
            }
        }

        // Check if the player has reached the maximum amount of clicks to complete the event
        if ((currentClicks >= maxClicks) &! eventCompleted)
        {
            EventSucceeded();
        }
    }

    private void RemoveDescription()
    {       
        quickTimeText.gameObject.SetActive(false);
        descriptionBeenDisplayed = true;
        SpawnQuickTimeButton();
        timer = 0;
    }

    private void EndQuickTimeEvent() 
    {
        timer = descriptionDisplayTime;
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
        quickTimeText.gameObject.SetActive(false);
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
        quickTimeText.text = eventDescription;
        quickTimeText.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!eventCompleted && other.GetComponent<PlayerControler>())
        {
            StartQuickTimeEvent();
        }
    }
}
