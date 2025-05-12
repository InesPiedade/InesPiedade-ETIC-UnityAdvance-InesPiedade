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
    private float damageCooldown = 2;
    private bool isMoving;
    private float radius = 2f;
    private int rayCount = 8;
    private float detactionRange = 5;
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
        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * (360f / rayCount) * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, radius, chickenLayer);
            Debug.DrawRay(transform.position, direction * radius, hit ? Color.red : Color.blue);

            if (hit)
            {
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }

    void Shoot()
    {
        animator.SetTrigger("Attack");
        GameObject newBullet = Instantiate(bulletPf, firePoint.position, transform.rotation);
        newBullet.GetComponent<Bullet>().GoInDirection(firePoint.up);


        //dentro do milho
        //OnTriggerEnter2D com galinha, dar comida
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
            StartCoroutine(Invincible());
        }
    }

    private IEnumerator Death()
    {
        animator.SetTrigger("Death");
        player.enabled = false;
        yield return new WaitForSeconds(1f);
        manager.GameOver();
    }

    private IEnumerator Invincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(damageCooldown);
        isInvincible = false;
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
            TakeDamage(50);
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
