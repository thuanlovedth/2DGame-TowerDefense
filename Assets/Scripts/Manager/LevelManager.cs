using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : BaseManager<LevelManager>
{
    [SerializeField] public GameObject[] wayPoint1;
    [SerializeField] public GameObject[] wayPoint2;

    private bool isSpawn;
    private bool isPlayAgain;

    public bool IsSpawn => isSpawn;

    public bool IsPlayAgain => isPlayAgain;

    protected override void Awake()
    {
        base.Awake();

    }

    private void Start()
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }

    private void Update()
    {
        if (GameManager.Instance && UIManager.HasInstance)
        {
            if (GameManager.Instance.BaseHealth <= 0)
            {
                Time.timeScale = 0f;
                if (AudioManager.HasInstance)
                {
                    AudioManager.Instance.PlaySE(AUDIO.SE_LOSE);
                }
                UIManager.Instance.ActiveLosePanel(true);
                return;
            }
        }
    }

    public void StartSpawn()
    {
        isSpawn = true;
    }

    public void EndSpawn()
    {
        isSpawn = false;
    }

    public void ResetGamePlay()
    {
        isPlayAgain = true;
    }

    public void RunningGame()
    {
        isPlayAgain = false;
    }
}
