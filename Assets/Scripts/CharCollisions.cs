using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCollisions : MonoBehaviour
{
    public AudioSource contactAudio = null;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Rock" && !GameManager.isGameOver)
        {
            contactAudio.Play();
            GameManager.GameOver(true);
        }
        if (collision.collider.gameObject.tag == "Bottle")
            OnPickUpBottle(collision.collider.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rock" && !GameManager.isGameOver)
        {
            GameManager.GameOver(true);
            contactAudio.Play();
        }
        if (other.gameObject.tag == "Bottle")
            OnPickUpBottle(other.gameObject);
    }

    void OnPickUpBottle(GameObject bottle)
    {
        OxygenManagement.Instance.FillUp();
        Destroy(bottle);
    }
}
