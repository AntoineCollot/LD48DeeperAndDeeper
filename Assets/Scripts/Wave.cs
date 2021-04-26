using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float amplitude = 3;
    public float frequency = 3;
    Vector3 origin;
    float offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = Random.Range(0f, 10000f);
        origin = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 originLeft = origin;
        Vector3 originRight = origin;
        originLeft.x -= amplitude * 0.5f;
        originRight.x += amplitude * 0.5f;

        Vector3 pos = Curves.QuadEaseInOut(originLeft, originRight, Mathf.PerlinNoise(offset, Time.time * frequency));
        pos.x += CharController.Instance.transform.position.x * 0.5f;
        transform.localPosition = pos;
    }
}
