using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] Hand handPrefab = null;
    [SerializeField] LineVerletRope linePrefab = null;
    public int handCount = 5;
    public float originsAmplitude = 3;
    public float originsFrenquency = 3;
    Hand[] hands;

    [Header("Spawn")]
    public float minSpawnInterval = 0.2f;
    public float maxSpawnInterval = 1f;
    [Header("Waving")]
    public float minWavingInvterval = 2;
    public float maxWavingInvterval = 5;

    [SerializeField] Transform[] handTargets = new Transform[5];

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onGameStart.AddListener(OnGameStart);
    }

    void OnGameStart()
    {
        StartCoroutine(SpawnHands());
        StartCoroutine(SendHandsWavingLoop());
    }

    private void Update()
    {
        WaveHandOrigin();
    }
    void WaveHandOrigin()
    {
        Vector3 originLeft = transform.position;
        Vector3 originRight = transform.position;
        originLeft.x -= originsAmplitude * 0.5f;
        originRight.x += originsAmplitude * 0.5f;
        for (int i = 0; i < handCount; i++)
        {
            if (hands != null && hands[i] != null && hands.Length > i && hands[i].origin != null)
            {
                Vector3 pos = Curves.QuadEaseInOut(originLeft, originRight, Mathf.PerlinNoise(i * 7.33f, Time.time * originsFrenquency));
                pos.x += CharController.Instance.transform.position.x * 0.5f;
                hands[i].origin.position = pos;
            }
        }
    }

    IEnumerator SpawnHands()
    {
        hands = new Hand[handCount];
        for (int i = 0; i < handCount; i++)
        {
            hands[i] = Instantiate(handPrefab, transform);
            Transform handOrigin = new GameObject("HandOrigine_" + i).transform;
            handOrigin.SetParent(transform, false);
            handOrigin.localEulerAngles = new Vector3(90, 0, 0);
            hands[i].origin = handOrigin;
            hands[i].target = handTargets[i];

            LineVerletRope newLine = Instantiate(linePrefab, transform);
            newLine.CableEnd = hands[i].transform;
            newLine.CableStart = hands[i].origin;

            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    IEnumerator SendHandsWavingLoop()
    {
        while(true)
        {
            int randHand = Random.Range(0, 5);
            if (hands[randHand] != null && hands[randHand].State == HandState.Holding)
                hands[randHand].SetState(HandState.Waving);
            yield return new WaitForSeconds(Random.Range(minWavingInvterval, maxWavingInvterval));
        }
    }
}
