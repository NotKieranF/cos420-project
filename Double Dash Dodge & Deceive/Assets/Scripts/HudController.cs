using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public GameObject player;
    private Text m_DebugCoords;

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
