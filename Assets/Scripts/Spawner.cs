using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject foxPrefab;
    private float foxSpawnRate = 40;
    private float foxSpawned = 0;
    private bool cooldown;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    void Update()
    {
        if(foxSpawned <= foxSpawnRate && cooldown == false)
        {
            CreateFox();
            StartCoroutine(CoolDown());
        }
    }

    private void CreateFox()
    {
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);

        Vector2 foxSpawnPos = new Vector2(randX,randY);

        Instantiate(foxPrefab, foxSpawnPos, Quaternion.identity);

        foxSpawned = foxSpawned + 1;
    }

    private IEnumerator CoolDown()
    {
            cooldown = true;
            yield return new WaitForSeconds(2);
            cooldown = false;
    }
}
