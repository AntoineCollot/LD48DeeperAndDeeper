using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public float normalForce = 1;
    public float boostForce = 5;
    public bool allowBoostUse { get; private set; }

    new Rigidbody rigidbody;

    [Header("Inputs")]
    float effectiveForce;
    float playerInputs;

    [Header("Scrolling")]
    float refScrollSpeed;

    [Header("Animations")]
    public Transform rootBone = null;
    Quaternion rootBoneRotation;
    public float rotationAnimRatio = 20;
    float currentRotationAnim;
    float refRotationSmooth;
    public float rotationSmooth = 0.2f;
    Animator anim;

    public static CharController Instance;

    private void Awake()
    {
        Instance = this;
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rootBoneRotation = rootBone.localRotation;
        OxygenManagement.Instance.onFillUp.AddListener(OnFirstFillUp);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameOver)
            return;

        playerInputs = Input.GetAxis("Horizontal");
        float targetScrollSpeed = 1;

        effectiveForce = normalForce;
        if (Input.GetKey(KeyCode.Space)&& allowBoostUse)
        {
            effectiveForce = boostForce;

            if(Mathf.Abs(playerInputs)<0.05f)
            {
                targetScrollSpeed = 3;
            }
        }

        EnvironmentScrolling.Instance.speedMultiplier = Mathf.SmoothDamp(EnvironmentScrolling.Instance.speedMultiplier, targetScrollSpeed, ref refScrollSpeed, 0.2f);

        //Animations
        currentRotationAnim = Mathf.SmoothDamp(currentRotationAnim, playerInputs, ref refRotationSmooth, rotationSmooth);
        rootBone.localRotation = rootBoneRotation * Quaternion.Euler(Vector3.right * currentRotationAnim * rotationAnimRatio);

        if(Input.GetKey(KeyCode.Space) && Mathf.Abs(playerInputs)>0.1f)
        {
            if(playerInputs>0)
                anim.SetInteger("BoostState", 1);
            else
                anim.SetInteger("BoostState", -1);
        }
        else
        {
            anim.SetInteger("BoostState", 0);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.isGameOver)
            return;
        rigidbody.AddForce(Vector3.right * playerInputs * effectiveForce, ForceMode.Acceleration);
    }

    void OnFirstFillUp()
    {
        OxygenManagement.Instance.onFillUp.RemoveListener(OnFirstFillUp);
        allowBoostUse = true;
    }
}
