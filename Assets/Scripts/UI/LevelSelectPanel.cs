using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectPanel : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private int unlockedLevelNumber;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.HasInstance)
        {
            unlockedLevelNumber = PlayerPrefs.GetInt(GameManager.Instance.NextLevel);
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.HasInstance)
        {
            PlayerPrefs.SetInt(GameManager.Instance.NextLevel, GameManager.Instance.CurrentLevel);
            unlockedLevelNumber = PlayerPrefs.GetInt(GameManager.Instance.NextLevel);
        }
        if (unlockedLevelNumber <= buttons.Length)
        {
            for (int i = 0; i < unlockedLevelNumber; i++)
            {
                buttons[i].interactable = true;
                buttons[i].GetComponent<LevelSelection>().unlocked = true;
            }
        }
    }

    public void PressSelection(string _Levelname)
    {
        if (GameManager.HasInstance && UIManager.HasInstance)
        {
            GameManager.Instance.ChangeScene(_Levelname);
            GameManager.Instance.StartGame();
            if (LevelManager.HasInstance)
            {
                LevelManager.Instance.StartSpawn();
            }
            UIManager.Instance.ActiveLevelSelectPanel(false);
            UIManager.Instance.ActiveGamePanel(true);
        }
    }

    public void OnCLickExitButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveLevelSelectPanel(false);
            UIManager.Instance.ActiveMenuPanel(true);
        }
    }


}
