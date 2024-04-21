using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    // Kill player when contacted
    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerMovement player = collider.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.Die();
        }
    }
}
