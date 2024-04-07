using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Image nonStar1;
    [SerializeField] private Image nonStar2;
    [SerializeField] private Sprite startLight;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.HasInstance)
        {
            if (GameManager.Instance.BaseHealth >= 10)
            {
                nonStar1.sprite = startLight;
            }
            if (GameManager.Instance.BaseHealth == 20)
            {
                nonStar2.sprite = startLight;
            }
        }
    }

    public void OnClickRestartIcon()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.RestartGame();
        }
    }

    public void OnClickOKIcon()
    {
        if (UIManager.HasInstance && GameManager.HasInstance)
        {
            GameManager.Instance.SetStartState();
            GameManager.Instance.ChangeScene("Menu");
            GameManager.Instance.UpdateCurrentLevel();
            UIManager.Instance.ActiveGamePanel(false);
            UIManager.Instance.ActiveLevelSelectPanel(true);
            UIManager.Instance.ActiveWinPanel(false);
        }
    }
}
