using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public bool inverse = false;
    public bool lockY = false;
    public Transform target = null;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPos = target.position;
        if(inverse)
        {
            targetPos = 2 * transform.position - targetPos;
        }
        if (lockY)
        {
            targetPos.y = transform.position.y;
        }
        transform.LookAt(targetPos);
    }
}
