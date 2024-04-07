using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : MonoBehaviour
{
    [SerializeField] private GameObject touchThunder;
    [Range(0, 1)][SerializeField] private float speedSlowPercentage;
    [SerializeField] private float slowTime = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<Monster>();
            enemy.SlowSpeed(speedSlowPercentage);
            enemy.slowTime = slowTime;
            var newEffect = Instantiate(touchThunder, transform.position, Quaternion.identity);
            newEffect.GetComponent<ParticleSystem>().Play();
            Destroy(newEffect, 2f);
        }
    }
}
