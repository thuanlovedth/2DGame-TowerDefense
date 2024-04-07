using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int HP;
    [SerializeField] private int maxHealth;
    [SerializeField] private Collider2D enemyCollider;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sR;
    [SerializeField] private int earnGold;
    [SerializeField] private HealthBar healthSlider;
    
    private int currentwayPoint = 0;
    private GameObject target;
    private int rand;
    private bool isDead = false;
    private float delay = 1f;
    private float currentSpeed;
    public float slowTime;

    public bool IsDead => isDead;

    private void Awake()
    {
        healthSlider = GetComponentInChildren<HealthBar>();
        currentSpeed = moveSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.UpdateHealthBar(HP, maxHealth);
        rand = Random.Range(0, 2);
        if (LevelManager.HasInstance)
        {
            if (rand == 0)
            {
                target = LevelManager.Instance.wayPoint1[currentwayPoint];
            }
            else
            {
                target = LevelManager.Instance.wayPoint2[currentwayPoint];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, currentSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, target.transform.position) <= 0.1f && isDead == false)
        {
            GetNextWayPoint();
        }
        Flip();
    }

    private void GetNextWayPoint()
    {
        if (currentwayPoint == LevelManager.Instance.wayPoint1.Length - 1)
        {
            EnemySpawn.onEnemyDestroy.Invoke();
            GameManager.Instance.DecreaseCastleLife();
            Destroy(gameObject);
            return;
        }
        currentwayPoint++;
        if (LevelManager.HasInstance)
        {
            if (rand == 0)
            {
                target = LevelManager.Instance.wayPoint1[currentwayPoint];
            }
            else
            {
                target = LevelManager.Instance.wayPoint2[currentwayPoint];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Projectile newP = collision.gameObject.GetComponent<Projectile>();
            EnemyWounded(newP.Attack);
        }
    }

    public void EnemyWounded(int hitPoints)
    {

        HP -= hitPoints;
        healthSlider.UpdateHealthBar(HP, maxHealth);
        if (HP <= 0 && !IsDead)
        {
            Die();
        }
        
    }

    private void Die()
    {
        if (LevelManager.HasInstance && GameManager.HasInstance)
        {
            isDead = true;
            currentSpeed = 0;
            anim.SetTrigger("Death");
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_MONSTERDEATH);
            }
            EnemySpawn.onEnemyDestroy.Invoke();
            GameManager.Instance.ReceiveGold(earnGold);
            Destroy(gameObject, delay);
        }
    }

    private void Flip()
    {
        if (transform.position.x - target.transform.position.x > 0)
        {
            sR.flipX = true;
        }
        else
        {
            sR.flipX = false;
        }
    }

    public void SlowSpeed(float pct)
    {
        currentSpeed = moveSpeed * (1 - pct);
        StartCoroutine(ResetSpeed());
    }

    private IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(slowTime);
        currentSpeed = moveSpeed;
    }
}
