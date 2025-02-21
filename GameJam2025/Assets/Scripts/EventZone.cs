using UnityEngine;

public class EventZone: MonoBehaviour 
{
    // Event that gets called when a player enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered is the player (based on tag)
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the zone!");
            // Call a custom function here (e.g., Start a cutscene, open a door)
            TriggerEvent();
        }
    }

    void TriggerEvent()
    {
        // Your custom event logic (e.g., activate an enemy, display a message)
        Debug.Log("Event triggered!");
    }
}
