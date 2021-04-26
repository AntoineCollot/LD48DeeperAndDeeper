using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public RockChunk[] chunkPool = null;
    public GameObject endChunk = null;
    public Transform spawnPosition = null;
    public float outOfBoundDist = 30;
    public static RockSpawner Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void OnChunkOutOfBound(RockChunk chunk)
    {
        if (GameManager.isEndSpawned)
            return;

        chunk.gameObject.SetActive(false);

        if (GameManager.Depth >= GameManager.MAX_DEPTH)
        {
            endChunk.transform.position = spawnPosition.position;
            endChunk.SetActive(true);
        }
        else
        {
            //Find an inactive chunk
            int maxTries = 100;
            int chunkId;
            do
            {
                chunkId = Random.Range(0, chunkPool.Length);
                maxTries--;
            }
            while (chunkPool[chunkId].gameObject.activeSelf);

            chunkPool[chunkId].transform.position = spawnPosition.position;
            chunkPool[chunkId].gameObject.SetActive(true);
        }
    }
}
