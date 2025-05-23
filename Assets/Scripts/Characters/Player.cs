using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    #region Declarations

    [Header("References")]
    [SerializeField] private int hp = 100;
    private float speed = 5f;
    private bool isDead;
    private bool isInvincible;
    private float damageCooldown = 1;
    private bool isMoving;
    private float radius = 2f;
    private bool chiCheck;

    private Rigidbody2D rb;
    private Animator animator;
    private Player player;


    [Header("Game Objects")]
    [SerializeField] private GameObject bulletPf;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject cornBody;
    [SerializeField] private GameObject meatPrefab;
    [SerializeField] private Chicken chicken;
    [SerializeField] private LayerMask chickenLayer;
    [SerializeField] private GameManager manager;
    [SerializeField] private BulletPool bulletPool;

    private UIManager uiManager;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        uiManager = UIManager.instance;
    }

    void Update()
    {
        Vector2 playerMovement;
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + playerMovement * speed * Time.fixedDeltaTime);

        animator.SetFloat("MovingX", Mathf.Abs(playerMovement.x));
        animator.SetFloat("MovingY", Mathf.Abs(playerMovement.y));

        if (playerMovement.x != 0 | playerMovement.y != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            ChickenCheck();
        }

        FaceDirection();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RestoreHp(50);
        }
    }

    private void ChickenCheck()
    {
        Physics2D.CircleCast(transform.position, radius, Vector2.zero);
        Collider2D[] HitObjects = Physics2D.OverlapCircleAll(transform.position, radius);

        for (int i = 0; i < HitObjects.Length; ++i)
        {
            IInteractable interactable = HitObjects[i].transform.GetComponent<IInteractable>();

            if (HitObjects[i] == null)
            {
                continue;
            }
            if (interactable != null)
            {
                interactable.Interact();
            }

            Debug.Log(HitObjects[i].gameObject.name);
        }
    }

    void Shoot()
    {
        animator.SetTrigger("Attack");

        GameObject bullet = bulletPool.GetBullet();

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;

        bullet.GetComponent<Bullet>().GoInDirection(firePoint.up);
    }

    void FaceDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 directionToMouse = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = -directionToMouse;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, 100);
        animator.SetTrigger("Damage");
        uiManager.FillHearts(hp, 100);

        if (hp <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(Death());
        }
        else
        {
            isDead = false;
        }
    }

    private IEnumerator Death()
    {
        animator.SetTrigger("Death");

        yield return new WaitForSeconds(0.5f);
        manager.GameOver();
        Time.timeScale = 0f;
    }


    public void RestoreHp(int value)
    {
        animator.SetTrigger("Eating");
        hp += value;
        hp = Mathf.Clamp(hp, 0, 100);
        uiManager.LessMeat();
        uiManager.FillHearts(hp, 100);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Fox fox = collision.transform.GetComponent<Fox>();
        if (fox != null)
        {
            TakeDamage(20);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectable collectCorn = collision.transform.GetComponent<ICollectable>();
        Meat meat = collision.transform.GetComponent<Meat>();

        if (collectCorn != null)
        {
            animator.SetTrigger("Collect");
            collectCorn.CollectCorn();
        }
        if (meat != null)
        {
            animator.SetTrigger("GetMeat");
            meat.GetMeat();
        }
    }
}
