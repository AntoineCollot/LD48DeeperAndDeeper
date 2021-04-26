using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawner : MonoBehaviour
{
    public GameObject bottlePrefab = null;
    public Transform bottleSpawnPosition = null;
    public float minInterval = 8;
    public float maxInterval = 13;

    public float spawnAreaWidth = 17;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onGameStart.AddListener(OnGameStart);
    }

    void OnGameStart()
    {
        StartCoroutine(BottleSpawnLoop());
    }


    IEnumerator BottleSpawnLoop()
    {
        while(!GameManager.isGameOver)
        {
            //Find location
            Vector3 spawnPos = Vector3.zero;
            for (int i = 0; i < 100; i++)
            {
                spawnPos = new Vector3(Random.Range(-spawnAreaWidth, spawnAreaWidth) * 0.5f, bottleSpawnPosition.position.y, bottleSpawnPosition.position.z);

                //check if position is valid
                if(Physics.OverlapSphere(spawnPos, 0.3f).Length==0)
                {
                    break;
                }
            }

            GameObject newBottle = Instantiate(bottlePrefab, spawnPos, Quaternion.identity, transform);
            Destroy(newBottle, 30);

            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        }
    }
}
