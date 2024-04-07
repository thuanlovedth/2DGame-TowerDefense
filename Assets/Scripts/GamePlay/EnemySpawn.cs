using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawn : MonoBehaviour
{
    private const string WaveKey = "Wave";

    [SerializeField] private GameObject spawnPoint;
    [SerializeField] public float timeBewteenWaves = 5f;
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private int maxWave = 10;
    [SerializeField] private int baseNum = 5;
    [SerializeField] private float enemyPerSecond = 0.5f;
    [SerializeField] private float factorIncrease = 0.75f;

    private float timeSinceLastSpawn = 0;
    private int currentWave;
    private int enemyAlive;
    private int enemyLeftToSpawn;
    private bool isEndWave = false;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroy);
        if (GameManager.HasInstance)
        {
            currentWave = GameManager.Instance.Wave;
            enemyLeftToSpawn = EnemiesPerWave();
        }
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (LevelManager.HasInstance && !LevelManager.Instance.IsSpawn)
        {
            return;
        }
        if (LevelManager.HasInstance && LevelManager.Instance.IsPlayAgain)
        {
            SetBeginState();
            enemyLeftToSpawn = EnemiesPerWave();
            LevelManager.Instance.RunningGame();
            isEndWave = false;
        }
        if (GameManager.HasInstance && GameManager.Instance.IsRestart == true)
        {
            GameManager.Instance.ResetRestartValue();
            SetBeginState();
            StartCoroutine(StartWave());
            isEndWave = false;
        }
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= (1f / enemyPerSecond) && enemyLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemyLeftToSpawn--;
            enemyAlive++;
            timeSinceLastSpawn = 0f;
            if(enemyLeftToSpawn == 0)
            {
                isEndWave = true;
            }
        }
        if (enemyAlive == 0 && enemyLeftToSpawn == 0 && isEndWave)
        {
            StartCoroutine(EndWave());
        }
        if (GameManager.HasInstance && GameManager.Instance.BaseHealth <= 0 && LevelManager.HasInstance)
        {
            LevelManager.Instance.ResetGamePlay();
            LevelManager.Instance.EndSpawn();
            return;
        }
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseNum * Mathf.Pow(currentWave, factorIncrease));
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBewteenWaves);
        if (LevelManager.HasInstance)
        {
            LevelManager.Instance.StartSpawn();
        }
        enemyLeftToSpawn = EnemiesPerWave();
    }

    private void SpawnEnemy()
    {
        int i = Random.Range(0, _enemies.Length);
        GameObject newMonster = _enemies[i];
        Instantiate(newMonster, spawnPoint.transform.position, Quaternion.identity);
    }

    private void EnemyDestroy()
    {
        enemyAlive--;
    }

    private IEnumerator EndWave()
    {
        if (LevelManager.HasInstance)
        {
            LevelManager.Instance.EndSpawn();
        }
        timeSinceLastSpawn = 0f;
        if (GameManager.HasInstance)
        {
            if (currentWave < maxWave)
            {
                currentWave++;
                GameManager.Instance.UpdateWave(currentWave);
                StartCoroutine(StartWave());
            }
            else
            {
                if (LevelManager.HasInstance)
                {
                    LevelManager.Instance.ResetGamePlay();
                }
                if (GameManager.Instance.BaseHealth > 0)
                {
                    yield return new WaitForSeconds(1f);
                    Time.timeScale = 0f;
                    if (UIManager.HasInstance && AudioManager.HasInstance)
                    {
                        AudioManager.Instance.PlaySE(AUDIO.SE_WIN);
                        UIManager.Instance.ActiveWinPanel(true);
                    }
                }
            }
        }   
    }

    public void SetBeginState()
    {
        currentWave = 1;
        enemyAlive = 0;
    }
}

