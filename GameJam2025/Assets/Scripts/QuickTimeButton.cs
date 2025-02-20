using UnityEngine;

public class QuickTimeButton : MonoBehaviour
{
    const int SHRINK_SPEED_MULTIPLIER = 2;

    public QuickTimeEventHandler currentQuickTimeEvent;
    public float secondBeforeShrink = 1f;
    [Range(1, 50)]
    public int shrinkPercentageSpeed = 5;

    private bool removeButton = false;
    private bool isClicked = false;
    private float timer;
    private float minScaleToRemove = 0.2f;

    public void ButtonConstructor(QuickTimeEventHandler myEventHandler)
    {
        currentQuickTimeEvent = myEventHandler;
        timer = 0;
        shrinkPercentageSpeed = currentQuickTimeEvent.buttonShrinkPercentageSpeed;
        secondBeforeShrink = currentQuickTimeEvent.secondBeforeButtonShrink;
    }

    public void OnButtonClick()
    {
        removeButton = true;
        isClicked = true;
        currentQuickTimeEvent.currentClicks++;
    }

    /// <summary>
    /// Shrinks the button when it is clicked or when the time runs out
    /// </summary>
    /// <param name="isClicked">Determines if the button is dissapearing because of being clicked or not</param>
    private void RemoveButton(bool isClicked)
    {
        if (isClicked) 
        {
            shrinkPercentageSpeed *= SHRINK_SPEED_MULTIPLIER;
        }
        float buttonScale = transform.localScale.x * ((100 - (float)shrinkPercentageSpeed) / 100);
        transform.localScale = new Vector3(buttonScale, buttonScale, 1);

        // Remove button when it reaches the minimum scale
        if (buttonScale < minScaleToRemove)
        {
            if (!isClicked)
            {
                currentQuickTimeEvent.EventFailed();
                Debug.Log("Button not clicked in time!!!");
            }
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        // Starts shrinking button at the end of the timer
        if (timer < secondBeforeShrink)
        {
            timer += Time.deltaTime;
            if (timer >= secondBeforeShrink)
            {
                removeButton = true;
                timer = 0;
            }
        }

        if (removeButton)
        {
            RemoveButton(isClicked);
        }
    }
}
