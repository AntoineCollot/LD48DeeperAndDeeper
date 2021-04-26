using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomInitialRotation : MonoBehaviour
{
    public float maxTorque = 1;
    public float minTorque = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddTorque(Random.rotation.eulerAngles * Random.Range(minTorque, maxTorque), ForceMode.VelocityChange);
    }
}
