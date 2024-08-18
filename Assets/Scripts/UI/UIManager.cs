using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreTMP;
    public TextMeshProUGUI multiplierTMP;
    public TextMeshProUGUI sicknessTMP;
    public GameObject infoScreen;
    public TextMeshProUGUI gluttonyLevelTMP;
    public TextMeshProUGUI speedLevelTMP;
    public TextMeshProUGUI immunityLevelTMP;
    public Slider sicknessBar;
    private int score = 0;
    private int multiplier = 1;
    [Range(0f, 10f)]
    public float sicknessScalingFactor = 1f;
    private float sickness = 0f;
    // Start is called before the first frame update
    private Resources resources;
    private float accSec;
    private float sicknessBarUpdateInterval = 1f;
    void Start()
    {
        resources = FindObjectOfType<Player>().GetComponent<Resources>();
        scoreTMP.text = "Score: " + score.ToString();
        multiplierTMP.text = "Multiplier: " + multiplier.ToString() + "X";
        //sicknessTMP.text = "Sickness: " + sickness.ToString() + "%";
        sicknessBar.value = 0f;
        sicknessBar.maxValue = 100f;
        gameOverScreen.SetActive(false);
        infoScreen.SetActive(false);
    }

    private void Update()
    {
        accSec += Time.deltaTime;
        if (accSec > sicknessBarUpdateInterval)
        {
            UpdateSicknessBarValue();
            accSec = 0;
        }
    }

    public void UpdateSicknessBarValue()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        sicknessScalingFactor = 1f;
        sicknessScalingFactor -= resources.immunityLevel * 0.08f;
        sickness = enemies.Length * sicknessScalingFactor;
        sickness = Mathf.Clamp(sickness, 0f, 100f);
        sicknessBar.value = sickness;
        if (sicknessBar.value >= sicknessBar.maxValue)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
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
        //sicknessScalingFactor = 1f;
        //sicknessScalingFactor -= resources.immunityLevel * 0.08f;
        //sickness = numEnemiesAlive * sicknessScalingFactor;
        //sickness = Mathf.Clamp(sickness, 0f, 100f);
        //UpdateSicknessText();
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
        //sicknessTMP.text = "Sickness: " + sickness.ToString("F2") + "%";
    }
    public void UpdateInfoScreen(bool active, int[] levels)
    {
        gluttonyLevelTMP.text = levels[0].ToString();
        speedLevelTMP.text = levels[1].ToString();
        immunityLevelTMP.text = levels[2].ToString();
        infoScreen.SetActive(active);
    }


}
