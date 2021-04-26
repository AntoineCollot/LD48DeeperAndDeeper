using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameOverFadeOut : MonoBehaviour
{
    public PostProcessProfile profile;
    public float fadeTime = 3;
    public float delay = 3;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onGameOver.AddListener(OnGameOver);
    }

    void OnGameOver(bool bloody)
    {
        StartCoroutine(FadeOut());
    }

    private void OnDestroy()
    {
        profile.TryGetSettings<ColorGrading>(out ColorGrading colorGrading);

        colorGrading.postExposure.value = 0;
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(delay);

        float t = 0;
        profile.TryGetSettings<ColorGrading>(out ColorGrading colorGrading);

        while (t < 1)
        {
            t += Time.deltaTime / fadeTime;

            colorGrading.postExposure.value = Mathf.Lerp(0, -20, t);

            yield return null;
        }
    }
}
