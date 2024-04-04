using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    public GameObject winnerPopUp;
    public TMP_Text currentpopUpText;
    public TMP_Text otherpopUpText;

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
