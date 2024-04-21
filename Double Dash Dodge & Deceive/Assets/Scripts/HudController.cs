// Description: Start of some debugging functionality, with some user coordinates being displayed based on player position
// Language: C#


//-----------------------------------------------Imports and Dependencies---------------------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//---------------------------------------------------------Hud Controller Class------------------------------------------//

public class HudController : MonoBehaviour
{

//-------------------------------------------------Class Variables-------------------------------------------------//
    public GameObject player;
    private Text m_DebugCoords;

//--------------------------------------------------------Main logic-----------------------------------------------//

    // Start is called before the first frame update
    void Start()
    {
        m_DebugCoords = transform.Find("DebugCoords").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_DebugCoords.text = string.Format("X: {0:F4}\nY: {1:F4}", player.transform.position.x, player.transform.position.y);
    }
}
