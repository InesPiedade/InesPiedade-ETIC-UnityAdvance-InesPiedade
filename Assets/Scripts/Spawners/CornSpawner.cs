using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnOne;
    [SerializeField] private Transform spawnTwo;
    [SerializeField] private Transform spawnThree;

    [SerializeField] private GameObject cornPrefab;
    private GameObject cornOne;
    private GameObject cornTwo;
    private GameObject cornThree;

    private bool cooldownOne = false;
    private bool cooldownTwo = false;
    private bool cooldownThree = false;

    private void Update()
    {
        CreateCorn();
    }
    public void CreateCorn()
    {
        if (spawnOne != null && cornOne == null && cooldownOne == false)
        {
            cornOne = Instantiate(cornPrefab, spawnOne.position, Quaternion.identity);
            StartCoroutine(CoolDown(1));
        }
        if (spawnTwo != null && cornTwo == null && cooldownTwo == false)
        {
            cornTwo = Instantiate(cornPrefab, spawnTwo.position, Quaternion.identity);
            StartCoroutine(CoolDown(2));
        }
        if (spawnThree != null && cornThree == null && cooldownThree == false)
        {
            cornThree = Instantiate(cornPrefab, spawnThree.position, Quaternion.identity);
            StartCoroutine(CoolDown(3));
        }
    }

    private IEnumerator CoolDown(int number)
    {
        if(number == 1)
        {
            cooldownOne = true;
            yield return new WaitForSeconds(20);
            cooldownOne = false;
        }
        if (number == 2)
        {
            cooldownTwo = true;
            yield return new WaitForSeconds(20);
            cooldownTwo = false;
        }
        if (number == 3)
        {
            cooldownThree = true;
            yield return new WaitForSeconds(20);
            cooldownThree = false;
        }

    }
}
