using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public void OnCLickResumeButton()
    {
        if(GameManager.HasInstance && UIManager.HasInstance)
        {
            GameManager.Instance.ResumeGame();
            UIManager.Instance.ActivePausePanel(false);
        }
    }

    public void OnClickRestartButton()
    {
        if (GameManager.HasInstance && UIManager.HasInstance)
        {
            UIManager.Instance.ActivePausePanel(false);
            GameManager.Instance.RestartGame();
        }
    }

    public void OnClickQuitButton()
    {
        if (UIManager.HasInstance && GameManager.HasInstance)
        {
            GameManager.Instance.SetStartState();
            if (LevelManager.HasInstance)
            {
                LevelManager.Instance.ResetGamePlay();
            }
            GameManager.Instance.ChangeScene("Menu");
            UIManager.Instance.ActiveGamePanel(false);
            UIManager.Instance.ActiveMenuPanel(true);
            UIManager.Instance.ActivePausePanel(false);
        }
    }
}
