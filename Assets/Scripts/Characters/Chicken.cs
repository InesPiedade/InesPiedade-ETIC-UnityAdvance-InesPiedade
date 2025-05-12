using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour, IDamageable, IInteractable
{

    #region Declarations

    [Header("References")]
    private int hp = 50;
    private int corn;

    private UIManager uiManager;
    private Player player;

    [Header("GameOjects")]
    [SerializeField] private GameObject aperance1;
    [SerializeField] private GameObject aperance2;
    [SerializeField] private GameObject aperance3;
    [SerializeField] private GameObject aperance4;
    [SerializeField] private GameObject aperance5;
    [SerializeField] private GameObject aperance6;
    [SerializeField] private GameManager manager;

    #endregion

    private void Start()
    {
        uiManager = UIManager.instance;

        aperance1.SetActive(true);
        aperance2.SetActive(false);
        aperance3.SetActive(false);
        aperance4.SetActive(false);
        aperance5.SetActive(false);
        aperance6.SetActive(false);
    }

    private void Update()
    {
        if (uiManager.FeedChi1 >= 50)
        {
            aperance1.SetActive(false);
            aperance2.SetActive(false); 
            aperance3.SetActive(false);
            aperance4.SetActive(false);
            aperance5.SetActive(false);
            aperance6.SetActive(true);
            StartCoroutine(Victory());
        }
        else if (uiManager.FeedChi1 >= 45)
        {
            aperance1.SetActive(false);
            aperance2.SetActive(false);
            aperance3.SetActive(false);
            aperance4.SetActive(false);
            aperance5.SetActive(true);
            aperance6.SetActive(false);
        }
        else if (uiManager.FeedChi1 >= 35)
        {
            aperance1.SetActive(false);
            aperance2.SetActive(false);
            aperance3.SetActive(false);
            aperance4.SetActive(true);
            aperance5.SetActive(false);
            aperance6.SetActive(false);
        }
        else if (uiManager.FeedChi1 >= 25)
        {
            aperance1.SetActive(false);
            aperance2.SetActive(false);
            aperance3.SetActive(true);
            aperance4.SetActive(false);
            aperance5.SetActive(false);
            aperance6.SetActive(false);
        }
        else if (uiManager.FeedChi1 >= 15)
        {
            aperance1.SetActive(false);
            aperance2.SetActive(true);
            aperance3.SetActive(false);
            aperance4.SetActive(false);
            aperance5.SetActive(false);
            aperance6.SetActive(false);
        }
    }
    private IEnumerator Victory()
    {
        yield return new WaitForSeconds(1f);
        manager.WinScreen();
    }

    public void Interact()
    {
        uiManager.FeedChi();
    }


    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, 100);

        if (hp <= 0)
        {
            Destroy(gameObject);
            manager.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Fox fox = collision.transform.GetComponent<Fox>();
        if (fox != null)
        {
            TakeDamage(50);
        }
    }


}