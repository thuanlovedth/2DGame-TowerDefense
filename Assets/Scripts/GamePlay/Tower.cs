using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tower : MonoBehaviour
{
    [SerializeField] private float attackRate;
    [SerializeField] private float attackRadius;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject weapon;
    [SerializeField] private float speedRotate;
    [SerializeField] private GameObject pointToFire;
    [SerializeField] private int cost;
    [SerializeField] private GameObject uiInfo;
    [SerializeField] private Animator animWeapon;
    [SerializeField] private ProjectileType pType;

    private Transform target;
    private float attackCountDown = 0f;
    public const int id = 0;
    private bool openPanel = false;
    public GameObject location;
    private bool isAttack = false;
    private Transform[] targets;

    private void Awake()
    {
        animWeapon = GetComponentInChildren<Animator>();
    }
    public int Price => cost;
    
    private void Start()
    {
        InvokeRepeating("SelectTarget", 0f, 0.5f);
    }

    private void SelectTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDis = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (var item in enemies)
        {
            float dis = Vector2.Distance(transform.position, item.transform.position);
            if (dis < shortestDis)
            {
                shortestDis = dis;
                nearestEnemy = item;
            }
        }
        if (nearestEnemy != null && shortestDis <= attackRadius)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            animWeapon.SetInteger("State", 0);
            isAttack = false;
            return;
        }
        attackCountDown += Time.deltaTime;
        if (pType == ProjectileType.Crossbow)
        {
            RotateTowardsTarget();
        }
        if (attackCountDown >= 1f / attackRate)
        {
            isAttack = true;
            attackCountDown = 0;
        }
    }

    private void FixedUpdate()
    {
        if (isAttack)
        {
            isAttack = false;
            AttackTarget();
            PlaySound();
        }
    }

    private void RotateTowardsTarget()
    {
        var dir = target.position - transform.position;
        var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotate = Quaternion.Euler(new Vector3(0f, 0f, angleDirection + 10));
        weapon.transform.rotation = Quaternion.RotateTowards(weapon.transform.rotation, targetRotate, speedRotate * Time.deltaTime);
    }

    private void AttackTarget()
    {
        GameObject proj = Instantiate(projectile);
        proj.transform.position = pointToFire.transform.position;
        proj.GetComponent<Projectile>().Seek(target);
        proj.GetComponent<Projectile>().animWeapon = animWeapon;
    }

    private void OnMouseDown()
    {
        if (!openPanel)
        {
            uiInfo.SetActive(true);
            openPanel = true;
        }
        else
        {
            uiInfo.SetActive(false);
            openPanel = false;
        }
    }

    private void PlaySound()
    {
        if (AudioManager.HasInstance)
        {
            if(pType == ProjectileType.Crossbow)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_CROSSBOWFIRE);
                return;
            }
            if(pType == ProjectileType.Thunder)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_ELECTRICFIRE);
                return;
            }
            AudioManager.Instance.PlaySE(AUDIO.SE_FIREBURN);
            AudioManager.Instance.PlaySE(AUDIO.SE_FIRESPELL);
        }
    }
}
