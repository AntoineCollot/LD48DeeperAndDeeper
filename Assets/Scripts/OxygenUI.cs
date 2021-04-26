using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OxygenUI : MonoBehaviour
{
    public Image bottleFill = null;
    public TextMeshProUGUI oxygenValueText = null;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameOver)
            return;
        oxygenValueText.text = (OxygenManagement.Instance.oxygenLevel * 100).ToString("00") + "%";
        bottleFill.fillAmount = OxygenManagement.Instance.oxygenLevel;
    }
}
