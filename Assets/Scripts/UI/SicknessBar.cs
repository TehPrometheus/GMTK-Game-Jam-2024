using UnityEngine;
using UnityEngine.UI;

public class SicknessBar : MonoBehaviour
{
    public Slider m_HealthBar;

    void Start()
    {

        // Subscribe to the HealthChangedEvent with the UpdateHealthBarValue function
    }

    // Update is called once per frame
    void UpdateHealthBarValue(float newHealthValue)
    {
        m_HealthBar.value = newHealthValue;
    }

    private void OnDestroy()
    {
        // Unsubscribe from event
    }
}
