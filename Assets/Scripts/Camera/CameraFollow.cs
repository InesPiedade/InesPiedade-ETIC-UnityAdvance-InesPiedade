using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Declarations")]
    private Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if(player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y, -6);
        }
    }
}
