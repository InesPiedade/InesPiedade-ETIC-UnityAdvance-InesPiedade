using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Declarations
    [Header("References")]
    private int damage = 50;
    private int speed = 5;

    [Header("Game Objects")]
    private Rigidbody2D rb;

    private BulletPool bulletPool;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3);
    }

    public void GoInDirection(Vector2 direction)
    {
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fox collidedFox = collision.GetComponent<Fox>();

        if(collidedFox != null)
        {
            collidedFox.TakeDamage(damage);
        }

        Cow collidedCow = collision.GetComponent<Cow>();
        if (collidedCow != null)
        {
            collidedCow.TakeDamage(damage);
        }
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);

        bulletPool.ReturnBullet(gameObject);
    }

    public void SetBulletPool(BulletPool pool)
    {
        bulletPool = pool;
    }
}
