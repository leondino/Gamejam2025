using System.Collections.Generic;
using UnityEngine;

public class HideObjectsFromCamera : MonoBehaviour
{
    class ObjectToHide
    {
        public GameObject gameObject;
        public Material ogMaterial;
    }

    // Declaration
    [SerializeField] Material transparentMaterial;

    //TODO: List with all objects that should be hidden + their materials (track with index)
    private List<ObjectToHide> objectsToHide;

    // Initialize in Awake
    void Awake()
    {
        objectsToHide = new List<ObjectToHide>();
    }

    // Switches the material to a transparent one when the object enters the trigger zone
    // and adds the object to the list of objects to hide for reference of the old material
    private void OnTriggerStay(Collider other)
    {
        // If the object is tagged as NoCameraHide, don't hide it
        if (other.gameObject.CompareTag("NoCameraHide"))
        {
            return;
        }

        bool alreadyHidden = false;

        foreach (ObjectToHide obj in objectsToHide)
        {
            if (obj.gameObject == other.gameObject)
            {
                alreadyHidden = true;
                break;
            }
        }

        //other.gameObject.layer = 6;
        if (!alreadyHidden)
        {
            if (other.gameObject.GetComponent<Renderer>() != null)
            {
                objectsToHide.Add(new ObjectToHide
                {
                    gameObject = other.gameObject,
                    ogMaterial = other.gameObject.GetComponent<Renderer>().material
                });

                other.gameObject.GetComponent<Renderer>().material = transparentMaterial;
            }
        }
    }

    // Resets the material back to the original when the object leaves the trigger zone
    private void OnTriggerExit(Collider other)
    {
        //other.gameObject.layer = 0;
        foreach (ObjectToHide obj in objectsToHide)
        {
            if (obj.gameObject == other.gameObject)
            {
                Renderer otherRenderer = other.gameObject.GetComponent<Renderer>();
                if (otherRenderer != null)
                {
                    other.gameObject.GetComponent<Renderer>().material = obj.ogMaterial;
                    objectsToHide.Remove(obj);
                    break;
                }
            }
        }
    }
}
