using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    public void OnClickHomeIcon()
    {
        if (UIManager.HasInstance && GameManager.HasInstance)
        {
            GameManager.Instance.SetStartState();
            GameManager.Instance.ChangeScene("Menu");
            UIManager.Instance.ActiveGamePanel(false);
            UIManager.Instance.ActiveMenuPanel(true);
            UIManager.Instance.ActiveLosePanel(false);
        }
    }

    public void OnClickRestartIcon()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.RestartGame();
        }
    }
}
