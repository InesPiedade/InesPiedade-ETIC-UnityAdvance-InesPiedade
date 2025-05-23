using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : MonoBehaviour, ICollectable
{
    #region Declarations

    private UIManager uiManager;
    private GameManager manager;


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
