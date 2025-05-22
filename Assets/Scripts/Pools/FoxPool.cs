using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxPool : MonoBehaviour
{
    #region Declarations
    [Header("Game Objects")]
    [SerializeField] private GameObject foxPrefab;

    [Header("References")]
    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> foxPool;
    #endregion

    private void Start()
    {
        foxPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject fox = Instantiate(foxPrefab);
            fox.SetActive(false);
            fox.GetComponent<Fox>().SetFoxPool(this);
            foxPool.Enqueue(fox);
        }
    }

    public GameObject GetBullet()
    {
        if (foxPool.Count > 0)
        {
            GameObject bullet = foxPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject GenerateFox = GenerateObject(foxPrefab);
            return GenerateFox;
        }
    }

    public GameObject GenerateObject(GameObject PrefabToCreateFox)
    {
        GameObject fox = Instantiate(PrefabToCreateFox);
        fox.GetComponent<Fox>().SetFoxPool(this);
        return fox;
    }

    public void ReturnFox (GameObject fox)
    {
        fox.SetActive(false);
        foxPool.Enqueue(fox);
    }
}
