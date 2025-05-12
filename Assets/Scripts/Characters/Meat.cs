using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Meat : MonoBehaviour
{
    #region Declarations

    private Player player;
    private UIManager uiManager;

    #endregion

    private void Start()
    {
        uiManager = UIManager.instance;
    }
    
    public void GetMeat()
    {
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        //uiManager.AddMeat();
        Destroy(gameObject);
        uiManager.AddMeat();

    }
}
