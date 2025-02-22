using UnityEngine;

public class FailEvent : MonoBehaviour
{
    public float resetDelay = 3f;
    public bool resetOldLocation = false;

    public virtual void FailAction()
    {
        PlayerControler.Instance.playerReseter.TriggerReset(resetDelay, resetOldLocation);
    }

    public virtual void SuccesAction()
    {

    }
}
