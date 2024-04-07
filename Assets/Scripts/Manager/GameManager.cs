using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GameManager : BaseManager<GameManager>
{
    private const string levelUnlock = "levelUnlocked";

    private int baseLife;
    private int gold;
    private int wave;
    private bool isPlay = false;
    private bool isRestart = false;
    private int currentLevel = 1;

    public int BaseHealth => baseLife;
    public int Gold => gold;
    public int Wave => wave;
    public bool IsPlaying => isPlay;
    public bool IsRestart => isRestart;
    public string NextLevel => levelUnlock;
    public int CurrentLevel => currentLevel;

    protected override void Awake()
    {
        base.Awake();
        PlayerPrefs.SetInt(levelUnlock, 1);
    }

    private void Start()
    {
        SetStartState();
    }

    public void SetStartState()
    {
        baseLife = 20;
        gold = 1000;
        wave = 1;
    }

    public void UpdateWave(int value)
    {
        wave = value;
    }

    public void ReceiveGold(int coin)
    {
        gold += coin;
    }

    public void SpendGold(int coin)
    {
        gold -= coin;
    }

    public void DecreaseCastleLife()
    {
        baseLife--;
    }

    public void StartGame()
    {
        isPlay = true;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (isPlay == true)
        {
            isPlay = false;
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        if (!isPlay)
        {
            isPlay = true;
            Time.timeScale = 1f;
        }
    }

    public void RestartGame()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveLosePanel(false);
            UIManager.Instance.ActiveWinPanel(false);
            SetStartState();
            LoadScene(GetActiveScene().buildIndex);
            StartGame();
            if (!isRestart)
            {
                isRestart = true;
            }
        }
    }

    public void EndGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void ChangeScene(string sceneName)
    {
        LoadScene(sceneName);
    }

    public void ResetRestartValue()
    {
        if (isRestart == true)
        {
            isRestart = false;
        }
    }

    public void ReturntoLevelSelect()
    {
        if (isPlay)
        {
            isPlay = false;
        }
    }

    public void UpdateCurrentLevel()
    {
        currentLevel++;
    }
}
