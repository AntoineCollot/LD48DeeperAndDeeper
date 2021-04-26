using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DepthUI : MonoBehaviour
{
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.isGameOver)
            text.text = (-GameManager.Depth).ToString("000")+"m";
    }
}
