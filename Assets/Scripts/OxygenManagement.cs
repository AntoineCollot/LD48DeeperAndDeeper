using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OxygenManagement : MonoBehaviour
{
    public float idleOxygenLossPerSec = 0.05f;
    public float boostOxygenLossPerSec = 0.2f;
    public float oxygenLevel;
    public float startDelay = 3;
    bool hasPickedUpABottle = false;

    public UnityEvent onFillUp = new UnityEvent();

    public static OxygenManagement Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        oxygenLevel = 1;
    }

    private void Start()
    {
        GameManager.Instance.onGameStart.AddListener(OnGameStart);
    }

    void OnGameStart()
    {
        StartCoroutine(OxygenLoop());
    }

    public void FillUp()
    {
        hasPickedUpABottle = true;
        oxygenLevel = 1;
        onFillUp.Invoke();
    }

    IEnumerator OxygenLoop()
    {
        yield return new WaitForSeconds(startDelay);

        while (!GameManager.isEndSpawned)
        {
            if (Input.GetKey(KeyCode.Space)&&hasPickedUpABottle)
            {
                oxygenLevel -= Time.deltaTime * boostOxygenLossPerSec;
            }
            else
            {
                oxygenLevel -= Time.deltaTime * idleOxygenLossPerSec;
            }

            if (oxygenLevel <= 0)
                GameManager.GameOver();

            yield return null;
        }
    }
}
