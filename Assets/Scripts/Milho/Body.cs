using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour, IInteractable
{
    #region Declarations

    [Header("References")]
    private Player player;

    [Header("Game Objects")]
    [SerializeField] private GameObject bodyPreFab;

    #endregion
    public void Interact()
    {
        
    }
}
