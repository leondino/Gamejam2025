using TMPro;
using UnityEngine;

public class LvlManager : MonoBehaviour
{
    public TextMeshPro levelText; // Reference to the UI text
    private int level = 1; // Starting level

    void Start()
    {
        UpdateLevelText();
    }

    public void IncreaseLevel()
    {
        level++; // Increment level
        UpdateLevelText();
    }

    void UpdateLevelText()
    {
        levelText.text = "Level: " + level;
    }
}
