using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;

    public bool isKnockback = false;
    public bool isFrozen = false;
    public bool isBurning = false;

    private void Start()
    {
        Destroy(gameObject, lifeTime); // 수명 초과 시 파괴
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (isKnockback)
            {
                enemy.ApplyKnockback(2);
            }
            else if (isFrozen)
            {
                enemy.ApplyFrozen(1);
            }
            else if (isBurning)
            {
                enemy.ApplyBurning(3);
            }
            enemy.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
