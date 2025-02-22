using UnityEngine;

public class FailEvent : MonoBehaviour
{
    public float resetDelay = 3f;
    public bool resetOldLocation = false;
    public bool disableCamera = false;

    public virtual void FailAction()
    {
        PlayerControler.Instance.playerReseter.TriggerReset(resetDelay, resetOldLocation, disableCamera);
    }

    public virtual void SuccesAction()
    {

    }
}
