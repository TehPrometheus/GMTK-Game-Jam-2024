using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreTMP;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreTMP.text = "Score: " + score.ToString();
    }

    public void IncrementScore(int scoreIncrement)
    {
        score += scoreIncrement;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreTMP.text = "Score: " + score.ToString();
    }
}
