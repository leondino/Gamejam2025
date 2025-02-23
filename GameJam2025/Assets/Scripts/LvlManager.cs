using System.Collections;
using TMPro;
using UnityEngine;

public class LvlManager : MonoBehaviour
{
    public TextMeshPro levelText; // Reference to the UI text
    public TextMeshPro levelUpText;
    private int level = 1; // Starting level
    public float duration = 1.5f; 

    void Start()
    {
        UpdateLevelText();
    }
    
    public void IncreaseLevel(int lvlsToAdd)
    {
        level = level + lvlsToAdd; // Increment level
        ShowLevelUpText(level);
    }

    void UpdateLevelText()
    {
        levelText.text = "Level: " + level;
    }
    
    void ShowLevelUpText(int newLevel)
    {
        levelUpText.text = "Level " + newLevel + "!"; 
        StartCoroutine(LevelUpAnimation());
    }

    IEnumerator LevelUpAnimation()
    {
        float time = 0;
        Vector3 startScale = Vector3.one;
        Vector3 endScale = Vector3.one * 1.5f; // Bigger size
        Color startColor = new Color(1, 1, 0, 0); // Yellow and transparent
        Color endColor = new Color(1, 1, 0, 1); // Fully visible

        while (time < duration)
        {
            levelText.enabled = false;
            time += Time.deltaTime;
            float progress = time / duration;

            levelUpText.transform.localScale = Vector3.Lerp(startScale, endScale, progress);
            levelUpText.color = Color.Lerp(startColor, endColor, progress);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // Pause

        // Fade out
        time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            levelUpText.color = Color.Lerp(endColor, startColor, time / duration);
            levelText.enabled = true;
            UpdateLevelText();
            yield return null;
        }
    }
}
