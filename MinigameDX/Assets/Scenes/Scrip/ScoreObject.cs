using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Simple helper component representing one row in the leaderboard UI.
// Assign the username and score TMP fields and an optional background Image to highlight.
public class ScoreObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Highlight (optional)")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color highlightColor = new Color(1f, 0.92f, 0.6f);

    private Color defaultBgColor;

    private void Awake()
    {
        if (backgroundImage != null)
            defaultBgColor = backgroundImage.color;
    }

    // Set username and score text
    public void SetScore(string username, int score)
    {
        if (usernameText != null)
            usernameText.text = username;
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    // Visually highlight this row (e.g., player's own score)
    public void HighlightScore()
    {
        if (backgroundImage != null)
        {
            backgroundImage.color = highlightColor;
        }
        else
        {
            if (usernameText != null)
                usernameText.fontStyle = FontStyles.Bold;
            if (scoreText != null)
                scoreText.fontStyle = FontStyles.Bold;
        }
    }

    // Remove highlight
    public void UnHighlightScore()
    {
        if (backgroundImage != null)
        {
            backgroundImage.color = defaultBgColor;
        }
        else
        {
            if (usernameText != null)
                usernameText.fontStyle = FontStyles.Normal;
            if (scoreText != null)
                scoreText.fontStyle = FontStyles.Normal;
        }
    }
}