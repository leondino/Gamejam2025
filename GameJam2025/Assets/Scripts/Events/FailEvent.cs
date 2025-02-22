using UnityEngine;

public class FailEvent : MonoBehaviour
{
    public float resetDelay = 3f;
    public bool resetOldLocation = false;

    public virtual void FailAction()
    {
        Debug.Log(resetOldLocation);
        Debug.Log("FailAction");
        PlayerControler.Instance.playerReseter.TriggerReset(resetDelay, resetOldLocation);
    }
}
