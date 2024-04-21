// Description: Code to use for the pop up text thats displayed when a player reaches the finish line
// Language: C#


//----------------------------------------------------Imports and Dependencies--------------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//-----------------------------------------------Pop Up Class-------------------------------------------//

public class PopUp : MonoBehaviour
{
    
//-------------------------------------------------Class Variables----------------------------------------------//
    public GameObject winnerPopUp;
    public TMP_Text currentpopUpText;
    public TMP_Text otherpopUpText;

//--------------------------------------------------------------Main logic----------------------------------------------------//
    public void runPopUp(string text)
    {
        if (otherpopUpText.text == "You win!")
        {
            return;
        }
        else
        {
            winnerPopUp.SetActive(true);
            currentpopUpText.text = text;
        }
    }
}
