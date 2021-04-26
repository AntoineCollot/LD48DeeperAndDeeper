using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShark : MonoBehaviour
{
    public Transform circlePivot = null;
    public Transform idleTarget = null;
    public Transform sharkModel = null;
    public float rotationSpeed = 30;
    public float closeUpTime = 1;
    public float retreatTime = 2;
    public enum SharkState { Idle, Attack}
    public SharkState sharkState = SharkState.Idle;
    public Animator anim;
    public float detectionRadius;
    Camera cam;
    public LayerMask detectionLayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;   
    }

    // Update is called once per frame
    void Update()
    {
        if (sharkState == SharkState.Idle)
        {
            circlePivot.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);

            sharkModel.position = idleTarget.position;
            sharkModel.rotation = idleTarget.rotation;

            DetectPlayer();
        }
    }

    void DetectPlayer()
    {
        Vector3 toCam = cam.transform.position - idleTarget.position;
        if (Physics.SphereCast(new Ray(idleTarget.position, toCam.normalized), detectionRadius, toCam.magnitude, detectionLayer))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if(sharkState!=SharkState.Attack && !GameManager.isGameOver && !GameManager.isEndSpawned)
            StartCoroutine(AttackAnim());
    }

    IEnumerator AttackAnim()
    {
        sharkState = SharkState.Attack;
        anim.SetTrigger("Attack");
        float t = 0;
        GetComponent<AudioSource>().Play();

        while(t<1)
        {
            t += Time.deltaTime / closeUpTime;

            sharkModel.position = Curves.CircEaseInOut(idleTarget.position, CharController.Instance.transform.position, Mathf.Clamp01(t));
            Quaternion lookAtRotation = Quaternion.LookRotation(CharController.Instance.transform.position - idleTarget.position);
            sharkModel.rotation = Quaternion.Slerp(idleTarget.rotation, lookAtRotation, Mathf.Clamp01(t * 3));

            if (t > 0.8f)
            {
                anim.SetTrigger("Bite");
                GameManager.GameOver(true);
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.3f);

        t = 0;
        Vector3 retreatPos = idleTarget.position + Vector3.forward * 10;
        while(t<1)
        {
            t += Time.deltaTime / retreatTime;

            sharkModel.localScale = Vector3.one * Curves.QuadEaseIn(1, 0, Mathf.Clamp01(t));
            sharkModel.position = Curves.QuadEaseIn(CharController.Instance.transform.position, retreatPos, Mathf.Clamp01(t));

            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(idleTarget.position, detectionRadius);
    }
}
