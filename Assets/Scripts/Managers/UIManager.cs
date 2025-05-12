using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    #region Declarations
    public static UIManager instance;

    [SerializeField] private Text cornText;
    [SerializeField] private Text chiFeedText;
    [SerializeField] private Text meatText;
    [SerializeField] private Text seedsText;
    [SerializeField] private GameObject chiFase1;
    [SerializeField] private GameObject chiFase2;
    [SerializeField] private GameObject chiFase3;
    [SerializeField] private GameObject chiFase4;
    [SerializeField] private GameObject chiFase5;
    [SerializeField] private GameObject chiFase6;
    [SerializeField] private GameObject cornPrefab;
    [SerializeField] private Image hearts;

    private int corn;
    private int feedChi;
    private int meat;
    private int seeds;
    private int amountToRestore = 50;

    private Player player;
    private Corn cornScript;

    public int FeedChi1 { get => feedChi; }

    #endregion

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddCorn()
    {
        corn = corn + 1;
        corn = Mathf.Clamp(corn, 0, 100);
        cornText.text = corn.ToString();

    }

    public void AddSeed()
    {
        seeds++;
        seedsText.text = seeds.ToString();
    }

    public void AddMeat()
    {
        meat++;
        meatText.text = meat.ToString();
    }

    public void LessMeat()
    {
        meat--;
        meatText.text = meat.ToString();
    }

    public void FeedChi()
    {
        feedChi = corn + feedChi;
        corn = 0;

        chiFeedText.text = feedChi.ToString();
        cornText.text = corn.ToString();
    }

    public void FillHearts(int health, int maxHealth)
    {
        hearts.fillAmount = (float)health / (float)maxHealth;
    }
}
