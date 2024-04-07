using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ProjectileType
{
    Crossbow,
    Thunder,
    Fire
}

public class Projectile : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private ProjectileType pType;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private bool isMove;

    public int Attack => attackDamage;
    public ProjectileType PType => pType;
    private Transform target;
    public Animator animWeapon;
    private float delay = 1f;
    private float timeActive = 1f;
    private bool isHit = false;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (PType == ProjectileType.Crossbow && isMove)
        {
            MoveArrow();
            return;
        }
        if (PType == ProjectileType.Thunder && isMove)
        {
            MoveThunder();
            return;
        }
    }

    private void FixedUpdate()
    {
        if (pType == ProjectileType.Fire && !isMove)
        {
            FireCircleTime();
            return;
        }
        if(isHit && AudioManager.HasInstance)
        {
            if(pType == ProjectileType.Crossbow)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_ARROWHIT);
            }
            else
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_ELECTRICHIT);
            }
            return;
        }
     

    }

    private void MoveArrow()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                animWeapon.SetInteger("State", 0);
                isHit = true;
                var newEffect = Instantiate(impactEffect, transform.position, Quaternion.identity);
                Destroy(newEffect, delay);
                Destroy(gameObject);
            }
            else
            {
                Vector2 dir = target.position - transform.position;
                var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
                Quaternion targetRotate = Quaternion.Euler(new Vector3(0f, 0f, angleDirection));
                transform.rotation = targetRotate;
                animWeapon.SetInteger("State", 1);
                transform.position = Vector2.MoveTowards(transform.position, target.position, attackSpeed * Time.deltaTime);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void MoveThunder()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                animWeapon.SetInteger("State", 0);
                isHit = true;
                Destroy(gameObject);
            }
            else
            {
                animWeapon.SetInteger("State", 1);
                transform.position = Vector2.MoveTowards(transform.position, target.position, attackSpeed * Time.deltaTime);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FireCircleTime()
    {
        if(target != null)
        {
            animWeapon.SetInteger("State", 1);
        }
        Destroy(gameObject, timeActive);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(pType == ProjectileType.Fire)
            {
                collision.gameObject.GetComponent<Monster>().EnemyWounded(Attack);
            }
        }
    }
}
