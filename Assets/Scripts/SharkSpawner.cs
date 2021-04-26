using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour
{
    public GameObject sharkPrefab = null;
    public Transform spawnPosition = null;
    public float minIntervalEasy = 8;
    public float maxIntervalEasy = 13;
    public float minIntervalHard = 4;
    public float maxIntervalHard = 10;

    public float spawnAreaWidth = 15;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onGameStart.AddListener(OnGameStart);
    }

    void OnGameStart()
    {
        StartCoroutine(SharkSpawnLoop());

    }

    IEnumerator SharkSpawnLoop()
    {
        while (!GameManager.isGameOver)
        {
            //Find location
            Vector3 spawnPos = Vector3.zero;
            for (int i = 0; i < 100; i++)
            {
                spawnPos = new Vector3(Random.Range(-spawnAreaWidth, spawnAreaWidth) * 0.5f, spawnPosition.position.y, spawnPosition.position.z);

                //check if position is valid
                if (Physics.OverlapSphere(spawnPos, 2.5f).Length == 0)
                {
                    yield return null;
                    break;
                }
            }

            GameObject newShark = Instantiate(sharkPrefab, spawnPos, Quaternion.identity, transform);
            Destroy(newShark, 30);

            yield return new WaitForSeconds(Random.Range(Mathf.Lerp(minIntervalEasy,minIntervalHard, GameManager.Difficulty) , Mathf.Lerp(maxIntervalEasy, maxIntervalHard, GameManager.Difficulty)));
        }
    }
}
