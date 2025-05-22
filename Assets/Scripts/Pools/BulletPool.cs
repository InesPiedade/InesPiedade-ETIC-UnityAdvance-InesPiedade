using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    #region Declarations
    [Header("Game Objects")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("References")]
    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> bulletPool;
    #endregion

    private void Start()
    {
        bulletPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullet.GetComponent<Bullet>().SetBulletPool(this);
            bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if(bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject GenerateBullet = GenerateObject(bulletPrefab);
            return GenerateBullet; 
        }
    }

    public GameObject GenerateObject(GameObject PrefabToCreatebullet)
    {
        GameObject bullet = Instantiate(PrefabToCreatebullet);
        bullet.GetComponent<Bullet>().SetBulletPool(this);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}