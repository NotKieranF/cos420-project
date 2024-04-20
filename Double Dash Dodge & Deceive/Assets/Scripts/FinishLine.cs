using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public string popUp;
    public int playerNum;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player 1" && (playerNum == 1))
        {
            PopUp pop = GameObject.FindGameObjectWithTag("Player1").GetComponent<PopUp>();
            pop.runPopUp(popUp);

        }
        else if(collision.gameObject.name == "Player 2" && (playerNum == 2))
        {
            PopUp pop = GameObject.FindGameObjectWithTag("Player2").GetComponent<PopUp>();
            pop.runPopUp(popUp);

        }
    }

}
