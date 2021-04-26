using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleBubbles : MonoBehaviour
{
    ParticleSystem particles;
    ParticleSystem.EmissionModule emission;
    public AudioSource boostAudio = null;
    float originalVolume;
    float refVolume;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        emission = particles.emission;
        originalVolume = boostAudio.volume;
        boostAudio.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        emission.enabled = Input.GetKey(KeyCode.Space) && !GameManager.isGameOver && CharController.Instance.allowBoostUse;
        float targetVolume = 0;
        if (emission.enabled)
            targetVolume = originalVolume;
        boostAudio.volume = Mathf.SmoothDamp(boostAudio.volume, targetVolume, ref refVolume, 0.1f);
    }
}
