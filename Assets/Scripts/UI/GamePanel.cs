using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentGold;
    [SerializeField] private TextMeshProUGUI currentHealth;
    [SerializeField] private TextMeshProUGUI currentWave;

    private void OnGUI()
    {
        if (GameManager.HasInstance)
        {
            currentGold.text = GameManager.Instance.Gold.ToString();
            currentHealth.text = GameManager.Instance.BaseHealth.ToString();
            currentWave.text = "Wave " + GameManager.Instance.Wave.ToString();
        }
    }

    public void OnClickSettingIcon()
    {
        if (UIManager.HasInstance)
        {
            Time.timeScale = 0f;
            UIManager.Instance.ActiveSettingPanel(true);
        }
    }

    public void OnClickPauseIcon()
    {
        if (UIManager.HasInstance && GameManager.HasInstance)
        {
            GameManager.Instance.PauseGame();
            UIManager.Instance.ActivePausePanel(true);
        }
    }
}
