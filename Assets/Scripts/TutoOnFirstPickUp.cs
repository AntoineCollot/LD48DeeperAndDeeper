using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoOnFirstPickUp : MonoBehaviour
{
    public GameObject tuto = null;

    // Start is called before the first frame update
    void Start()
    {
        OxygenManagement.Instance.onFillUp.AddListener(OnFirstFillUp);
    }

    void OnFirstFillUp()
    {
        OxygenManagement.Instance.onFillUp.RemoveListener(OnFirstFillUp);


        tuto.SetActive(true);
        Destroy(tuto, 10);
    }
}
