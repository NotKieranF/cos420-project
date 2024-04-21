using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    // Set player's checkpoint when contacted
    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerMovement player = collider.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.currentCheckpoint = this;
        }
    }
}
