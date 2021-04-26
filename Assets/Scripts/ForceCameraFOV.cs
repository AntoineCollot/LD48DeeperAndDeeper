using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCameraFOV : MonoBehaviour
{
    public float fixedHorizontalFOV = 85;

    Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        cam.fieldOfView = 2 * Mathf.Atan(Mathf.Tan(fixedHorizontalFOV * Mathf.Deg2Rad * 0.5f) / cam.aspect) * Mathf.Rad2Deg;
    }
}
