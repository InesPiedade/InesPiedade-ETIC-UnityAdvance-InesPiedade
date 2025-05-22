using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : MonoBehaviour, ICollectable
{
    #region Declarations

    private UIManager uiManager;
    private GameManager manager;
    //private float timer = 0f;
    //private float offsetTime = 2f;


    #endregion
    private void Start()
    {
        uiManager = UIManager.instance;
    }
    public void CollectCorn()
    {
        uiManager.AddCorn();
        Destroy(gameObject);
    }


    public void CollectMeat()
    {
        throw new System.NotImplementedException();
    }
}
