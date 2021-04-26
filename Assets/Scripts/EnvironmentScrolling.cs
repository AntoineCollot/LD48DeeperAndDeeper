using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScrolling : MonoBehaviour
{
    public float startDelay = 3;
    public float minSpeed = 1;
    public float maxSpeed = 5;
    bool isScrolling = false;
    public float speedMultiplier = 1;

    public static EnvironmentScrolling Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.onGameStart.AddListener(OnGameStart);
    }

    void OnGameStart()
    {
        StartCoroutine(ScrollLoop());
    }

    public void StopScrolling()
    {
        isScrolling = false;
    }

    IEnumerator ScrollLoop()
    {
        yield return new WaitForSeconds(startDelay);
        isScrolling = true;
        float t = 0;
        while(t<1)
        {
            t += Time.deltaTime / 2;
            transform.Translate(Vector3.up * Mathf.Lerp(0, minSpeed, t) * Time.deltaTime);

            yield return null;
        }

        while(isScrolling)
        {
            transform.Translate(Vector3.up * Mathf.Lerp(minSpeed, maxSpeed, GameManager.Difficulty) * Time.deltaTime * speedMultiplier);
            yield return null;
        }
        //t = 0;
        //while (t < 1)
        //{
        //    t += Time.deltaTime;
        //    transform.Translate(Vector3.up * Mathf.Lerp(maxSpeed, 0, t) * Time.deltaTime);

        //    yield return null;
        //}
    }
}
