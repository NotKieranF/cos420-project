// Description: Checkpoint Controller to control where a players last checkpoint was
// Language: C#

//------------------------------------------Imports and Dependencies---------------------------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------Checkpoint Controller Class------------------------------------------------//

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
