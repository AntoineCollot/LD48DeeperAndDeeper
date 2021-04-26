using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockChunk : MonoBehaviour
{
    const float SIZE = 10;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y - RockSpawner.Instance.spawnPosition.position.y > RockSpawner.Instance.outOfBoundDist)
            RockSpawner.Instance.OnChunkOutOfBound(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(transform.position, new Vector3(18, SIZE, 1));
    }
}
