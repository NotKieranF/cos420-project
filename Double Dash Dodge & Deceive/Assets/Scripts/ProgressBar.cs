using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    private Slider progressBar;
    public GameObject player;
    public int playerNum;       //Number to indicate which player this progress bar is for.

    private void Awake()
    {
        progressBar = gameObject.GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //If this is player 1, then if their x position is >= to 0 then set the value of the progress bar to their current x position.
        if (playerNum == 1 && player.transform.position.x >= 0)
        {
            progressBar.value = player.transform.position.x;
        }

        //If this is player 2, then if their x position is <= to 0 then set the value of the progress bar to the absolute value of their current x position.
        else if (playerNum == 2 && player.transform.position.x <= 0)
        {
            progressBar.value = Math.Abs(player.transform.position.x);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If this is player 1, then if their x position is >= to 0 then set the value of the progress bar to their current x position.
        if (playerNum == 1 && player.transform.position.x >= 0)
        {
            progressBar.value = player.transform.position.x;
        }

        //If this is player 2, then if their x position is <= to 0 then set the value of the progress bar to the absolute value of their current x position.
        else if (playerNum == 2 && player.transform.position.x <= 0)
        {
            progressBar.value = Math.Abs(player.transform.position.x);
        }
    }
}
