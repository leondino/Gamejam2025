using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public TextMeshProUGUI startingText;
    public float fadeDuration = 2f; // Duration of fade effect
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControler>())
        {
            StartCoroutine(FadeOutText());
        }
    }
    
    IEnumerator FadeOutText()
    {
        Color textColor = startingText.color;
        float startAlpha = textColor.a;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(startAlpha, 0, t / fadeDuration);
            textColor.a = alpha;
            startingText.color = textColor;
            yield return null;
        }

        // Ensure alpha is exactly 0 at the end
        textColor.a = 0;
        startingText.color = textColor;
    }
}