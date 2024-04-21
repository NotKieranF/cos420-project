// Description: Code to implement respawning player when they touch a trap
// Language: C#


//--------------------------------------------------Imports and Dependencies---------------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//-----------------------------------------------Trap Controller Class------------------------------------------------//
public class TrapController : MonoBehaviour
{
    
//---------------------------------------------Kill player when contacted----------------------------------------------//
    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerMovement player = collider.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.Die();
        }
    }
}
