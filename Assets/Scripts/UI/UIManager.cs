using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreTMP;
    public TextMeshProUGUI multiplierTMP;
    public TextMeshProUGUI sicknessTMP;
    private int score = 0;
    private int multiplier = 1;
    private int enemiesRecentlyKilled = 0;
    [Range(0f, 10f)]
    public float multiplierResetTime = 5f;
    private float accTime = 0f;
    [Range(0f, 10f)]
    public float sicknessScalingFactor = 1f;
    private float sickness = 0f;
    // Start is called before the first frame update
    void Start()
    {
        scoreTMP.text = "Score: " + score.ToString();
        multiplierTMP.text = "Multiplier: " + multiplier.ToString() + "X";
        sicknessTMP.text = "Sickness: " + sickness.ToString() + "%";
    }

    private void Update()
    {
        accTime += Time.deltaTime;
        if (accTime > multiplierResetTime)
        {
            ResetMultiplier();
            accTime = 0;
        }
    }

    public void IncrementScore(int scoreIncrement)
    {
        score += scoreIncrement * multiplier;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreTMP.text = "Score: " + score.ToString();
    }

    public void AdjustSicknessLevel(int numEnemiesAlive)
    {
        sickness = numEnemiesAlive * sicknessScalingFactor;
        sickness = Mathf.Clamp(sickness, 0f, 100f);
        UpdateSicknessText();
    }

    public void IncrementEnemiesKilled(int notUsed)
    {
        enemiesRecentlyKilled++;
        accTime = 0;
        if (enemiesRecentlyKilled <= 2)
            multiplier = 1;
        else if (enemiesRecentlyKilled <= 4)
            multiplier = 2;
        else if (enemiesRecentlyKilled <= 6)
            multiplier = 3;
        else if (enemiesRecentlyKilled <= 10)
            multiplier = 4;
        else
            multiplier = 5;
        UpdateMultiplierText();
    }

    private void ResetMultiplier()
    {
        enemiesRecentlyKilled = 0;
        multiplier = 1;
        UpdateMultiplierText();
    }

    private void UpdateMultiplierText()
    {

        multiplierTMP.text = "Multiplier: " + multiplier.ToString() + "X";
    }

    private void UpdateSicknessText()
    {
        sicknessTMP.text = "Sickness: " + sickness.ToString("F2") + "%";
    }
}
