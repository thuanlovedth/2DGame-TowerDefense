using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider sliderHP;

    public void UpdateHealthBar(int currentHP, int maxHP)
    {
        float current = currentHP;
        float max = maxHP;
        sliderHP.value = current / max;
    }
}
