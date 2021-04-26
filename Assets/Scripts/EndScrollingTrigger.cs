using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScrollingTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EnvironmentScrolling.Instance.StopScrolling();

        GameManager.Win();
    }
}
