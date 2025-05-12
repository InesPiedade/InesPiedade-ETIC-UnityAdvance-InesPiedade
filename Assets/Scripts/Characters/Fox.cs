using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Fox : MonoBehaviour, IDamageable
{

    #region Declarations


    [Header("References")]
    private int hp = 100;
    private float minimumDis = 20;
    private float speed = 1;
    private float cooldownTimer = 2;
    //private int damageOnContact = 5;
    private bool onCoolDown;
    private float timer;

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

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, 100);
        
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }



}
