using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum HandState { Aiming, Waving, Holding }
public class Hand : MonoBehaviour
{
    public HandState State { get; private set; }
    float progress = 0;
    const float AIMING_TIME = 2;
    const float WAVING_TIME = 5;
    const float WAVING_Y_OFFSET = 2;
    public Transform target;
    public Transform origin;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case HandState.Aiming:
                transform.position = Curves.QuadEaseOut(origin.position, target.position, Mathf.Clamp01(progress));
                transform.rotation = Quaternion.Slerp(origin.rotation, target.rotation, progress);
                progress += Time.deltaTime/ AIMING_TIME;
                if (progress > 1)
                    SetState(HandState.Holding);
                break;
            case HandState.Waving:
                Vector3 wavingPosition = new Vector3(origin.position.x, target.position.y - WAVING_Y_OFFSET, origin.position.z);
                if(progress<0.5f)
                {
                    transform.position = Curves.QuadEaseInOut(target.position, wavingPosition, Mathf.Clamp01(progress * 2));
                    transform.rotation = Quaternion.Slerp(target.rotation, origin.rotation, progress * 2);
                }
                else
                {
                    transform.position = Curves.QuadEaseInOut(wavingPosition, target.position, Mathf.Clamp01((progress - 0.5f)*2));
                    transform.rotation = Quaternion.Slerp(origin.rotation, target.rotation, (progress - 0.5f) * 2);
                }
                progress += Time.deltaTime/ WAVING_TIME;
                if(progress>1)
                    SetState(HandState.Holding);
                break;
            case HandState.Holding:
            default:
                transform.position = target.position;
                transform.rotation = target.rotation;
                break;
        }
    }

    public void SetState(HandState newState)
    {
        State = newState;
        progress = 0;

        if (newState == HandState.Holding)
            anim.SetBool("Grabbing", true);
        else
            anim.SetBool("Grabbing", false);
    }
}
