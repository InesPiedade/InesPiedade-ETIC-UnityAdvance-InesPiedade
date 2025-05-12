using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour, IDamageable
{
    #region Declarations

    [Header("References")]
    private int hp = 100;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    private Transform currentTargetPoint;
    [SerializeField] private float speed;
    private SpriteRenderer CowSprite;

    [Header("Game Objects")]
    [SerializeField] private GameObject meatPrefab;
 

    #endregion

    private void Awake()
    {
        currentTargetPoint = pointA;
        CowSprite = GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        Move();
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
            MeatDrop();
        }
    }

    private void MeatDrop()
    {
        Vector2 meatSpawn = new Vector2(transform.position.x, transform.position.y);
        Instantiate(meatPrefab, meatSpawn, Quaternion.identity);
    }

    private void Move()
    {
        float distance = Vector2.Distance(currentTargetPoint.position, transform.position);

        transform.position = Vector2.MoveTowards(transform.position, currentTargetPoint.position, speed * Time.deltaTime);

        if (distance < .01f)
        {
            if(currentTargetPoint == pointA)
            {
                currentTargetPoint = pointB;
                CowSprite.flipX = true;
            }
            else if (currentTargetPoint == pointB)
            {
                currentTargetPoint = pointA;
                CowSprite.flipX = false;
            }
        }
    }
}
