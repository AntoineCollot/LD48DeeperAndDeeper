using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDeathAnim : MonoBehaviour
{
    public GameObject deathBubbleParticles = null;
    public GameObject breathBubbleParticles = null;
    public GameObject BloodParticles = null;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onGameOver.AddListener(OnGameOver);
    }

    void OnGameOver(bool bloodyDeath)
    {
        GetComponent<Animator>().SetTrigger("Death");
        deathBubbleParticles.SetActive(true);
        breathBubbleParticles.SetActive(false);

        if (bloodyDeath)
            BloodParticles.SetActive(true);
    }
}
