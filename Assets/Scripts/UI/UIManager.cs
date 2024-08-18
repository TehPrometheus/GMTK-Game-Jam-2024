using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreTMP;
    public TextMeshProUGUI multiplierTMP;
    public TextMeshProUGUI sicknessTMP;
    private int score = 0;
    private int multiplier = 1;
    [Range(0f, 10f)]
    public float sicknessScalingFactor = 1f;
    private float sickness = 0f;
    // Start is called before the first frame update
    private Resources resources;
    void Start()
    {
        resources = FindObjectOfType<Player>().GetComponent<Resources>();
        scoreTMP.text = "Score: " + score.ToString();
        multiplierTMP.text = "Multiplier: " + multiplier.ToString() + "X";
        sicknessTMP.text = "Sickness: " + sickness.ToString() + "%";
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
        sicknessScalingFactor = 1f;
        sicknessScalingFactor -= resources.immunityLevel * 0.08f; 
        sickness = numEnemiesAlive * sicknessScalingFactor;
        sickness = Mathf.Clamp(sickness, 0f, 100f);
        UpdateSicknessText();
    }

    public void UpdateMultiplierValue(int sizeLevel)
    {
        multiplier = sizeLevel;
        UpdateMultiplierText();
    }

    private void ResetMultiplier()
    {
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
