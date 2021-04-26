using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBottleOnDeath : MonoBehaviour
{
    public GameObject bottlePrefab = null;
    public Transform newBottleParent = null;
    public float delay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onGameOver.AddListener(OnGameOver);
    }

    void OnGameOver(bool bloodyDeath)
    {
        Invoke("SpawnBottle", delay);
    }

    void SpawnBottle()
    {
        gameObject.SetActive(false);
        Instantiate(bottlePrefab, transform.position, transform.rotation, newBottleParent);
    }
}
