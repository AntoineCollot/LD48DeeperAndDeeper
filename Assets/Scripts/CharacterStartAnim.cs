using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStartAnim : MonoBehaviour
{
    public float offset;
    float originalHeight;
    public float descentTime = 3;
    public float startDelay = 2;
    public bool isPlayableCharacter = false;
    public GameObject startColliders = null;

    [Header("On First Fill Up")]
    public MeshRenderer charBottle = null;
    public MeshRenderer charMask = null;
    public LineRenderer resprirationLine = null;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = transform.position;
        originalHeight = startPos.y;
        startPos.y = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 10)).y+ offset;
        transform.position = startPos;

        if (isPlayableCharacter)
        {
            GameManager.Instance.onGameStart.AddListener(OnGameStart);
            OxygenManagement.Instance.onFillUp.AddListener(OnFirstFillUp);
        }
    }

    void OnGameStart()
    {
        StartCoroutine(DescentToOriginalHeight());
    }

    void OnFirstFillUp()
    {
        OxygenManagement.Instance.onFillUp.RemoveListener(OnFirstFillUp);

        charMask.enabled = true;
        charBottle.enabled = true;
        resprirationLine.enabled = true;
    }

    IEnumerator DescentToOriginalHeight()
    {
        yield return new WaitForSeconds(startDelay);
        GetComponent<Animator>().SetBool("IsDrowning", true);
        startColliders.SetActive(false);

        float t = 0;
        float startYPos = transform.position.y;
        while(t<1)
        {
            t += Time.deltaTime / descentTime;

            Vector3 pos = transform.position;
            pos.y = Curves.QuadEaseInOut(startYPos, originalHeight, Mathf.Clamp01(t));
            transform.position = pos;

            yield return null;
        }
    }
}
