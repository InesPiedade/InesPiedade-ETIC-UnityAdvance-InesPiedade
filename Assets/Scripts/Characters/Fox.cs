using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Fox : MonoBehaviour, IDamageable
{

    #region Declarations

    public static event Action<Fox> OnFoxDied;
    [Header("References")]
    private int hp = 100;
    private float minimumDis = 20;
    private float speed = 1;
    private float cooldownTimer = 2;
    private bool onCoolDown;
    private float timer;
    private FoxPool foxPool;

    [SerializeField] private Transform target;

    #endregion

    private void Start()
    {
        target = GameObject.Find("Chicken").transform;
    }

    private void Update()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        if(onCoolDown)
        {
            timer += Time.deltaTime;

            if(timer >= cooldownTimer)
            {
                onCoolDown = false;
                timer = 0;
            }
        }

        if(distance <= minimumDis && !onCoolDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.up = target.position - transform.position;
        }
    }

    public Fox Clone()
    {
        Fox clone = Instantiate(this);
        return clone;
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, 100);
        
        if(hp <= 0)
        {
            OnFoxDied?.Invoke(this);
            Destroy(gameObject);
        }
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
        foxPool.ReturnFox(gameObject);
    }

    public void SetFoxPool(FoxPool pool)
    {
        foxPool = pool;
    }
}
