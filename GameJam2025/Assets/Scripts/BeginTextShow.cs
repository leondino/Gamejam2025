using System.Collections;
using UnityEngine;

public class BeginTextShow : MonoBehaviour
{
    public float timeBeforeDissapear = 2f;
    private bool shouldDissapear = false;
    TMPro.TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        StartCoroutine(DissapearText());
    }


    IEnumerator DissapearText()
    {
        yield return new WaitForSeconds(timeBeforeDissapear);
        text.CrossFadeAlpha(0, timeBeforeDissapear, false);
        yield return new WaitForSeconds(timeBeforeDissapear);
        Destroy(gameObject);
    }
}
