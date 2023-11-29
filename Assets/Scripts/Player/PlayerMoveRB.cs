using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoveRB : MonoBehaviour
{
    [SerializeField] GameObjectEventChannelSO hitPlayerEventChannel;
    //public PlayerEventChannelSO pEC_SO;
    public Transform playerBody;
    public Transform playerCam;
    Rigidbody rb;
    [Header("Move Speed and Camera Speed")]
    Vector3 camLocalPos;
    Quaternion camLocalRot;
    float horizontal;
    float vertical;
    [SerializeField] Animator anim;
    [SerializeField] float lookSensitivity;
    [SerializeField] FloatVariable lookSens;
    float xRot = 0f;
    public float rotSpeed;
    //public float rotSmoothTime = 0.1f;
    //Vector3 rotSmoothVel;
    public float moveSpeed;
    [Header("Dashing")]
    public bool canDash;
    public float dashMultiplier;
    public float dashInterval;
    Vector3 newMoveDir;
    Vector3 currentCamRot;
    [Space]
    [Header("Camera Bobbing & Tilting")]
    public float bobbingSpeed;
    public float bobbingIntensity;
    float bobbingMidpoint;
    //tracks current phase of sine wave
    float bobbingTimer;
    public float tiltAngle;
    public float tiltSmoothing;
    [Space]
    [SerializeField] Vector3Variable playerPosition;
    [SerializeField] BoolVariable playerDead;
    bool dash;
    KeyCode lastPress;
    float timeSince;
    Coroutine hitDashRoutine;
    [HideInInspector] public Vector3 hitTargetVector3;
    [SerializeField] float speed;
    //bool camLock;
    [SerializeField] BoolVariable camLock;
    private void Awake()
    {
        //anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        playerDead.RuntimeValue = false;
        Time.timeScale = 1f;
        bobbingMidpoint = playerCam.localPosition.y;
    }

    private void Update()
    {
        float bobbingWave = 0f;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        camLocalPos = playerCam.localPosition;
        camLocalRot = playerCam.localRotation;
        Vector3 moveDir = transform.right * horizontal + transform.forward * vertical;
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (lastPress != KeyCode.D)
            {
                lastPress = KeyCode.D;
                timeSince = Time.time;
                dash = false;
            }
            else
            {
                if (Time.time - timeSince < 0.25f)
                {
                    lastPress = KeyCode.Z;
                    dash = true;
                }
                else
                {
                    dash = false;
                }
                timeSince = Time.time;
            }

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (lastPress != KeyCode.A)
            {
                lastPress = KeyCode.A;
                timeSince = Time.time;
                dash = false;
            }
            else
            {
                if (Time.time - timeSince < 0.25f)
                {
                    lastPress = KeyCode.Z;
                    dash = true;
                }
                else
                {
                    dash = false;
                }
                timeSince = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastPress = KeyCode.W;
            dash = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            lastPress = KeyCode.S;
            dash = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dash = true;
        }
        if (dash && canDash)
        {
            dash = false;
            //anim.SetTrigger("Dash");
            canDash = false;
            newMoveDir = moveDir.normalized * (moveSpeed * dashMultiplier) * Time.fixedDeltaTime;
            rb.AddForce(newMoveDir, ForceMode.Impulse);
            StartCoroutine(DashCD());
        }
        else
        {
            newMoveDir = moveDir.normalized * moveSpeed * Time.fixedDeltaTime;


            if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
            {
                bobbingTimer = 0f;
            }
            else
            {
                bobbingWave = Mathf.Sin(bobbingTimer);
                bobbingTimer += bobbingSpeed * Time.deltaTime;

                if (bobbingTimer > Mathf.PI * 2)
                {
                    bobbingTimer = bobbingTimer - (Mathf.PI * 2);
                }
            }

            if (bobbingWave != 0f)
            {

                float translateAmount = bobbingWave * bobbingIntensity;
                float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
                totalAxes = Mathf.Clamp(totalAxes, 0f, 1f);
                translateAmount *= totalAxes;
                camLocalPos.y = bobbingMidpoint + translateAmount;
            }
            else
            {
                camLocalPos.y = bobbingMidpoint;
            }

            //Quaternion tiltRotation = Quaternion.Euler(0f, 0f, horizontal * tiltAngle);
            //playerCam.localRotation = Quaternion.Slerp(playerCam.localRotation, tiltRotation, tiltSmoothing * Time.deltaTime);

        }

        playerCam.localPosition = camLocalPos;
        
        playerPosition.value = transform.position;
        CameraMovement();
    }



    void FixedUpdate()
    {
        //CameraMovement();
        rb.AddForce(newMoveDir, ForceMode.Force);
    }


    public void CameraMovement()
    {
        if (!playerDead.RuntimeValue && !camLock.RuntimeValue)
        {
            //float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
            //float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

            float mouseX = Input.GetAxisRaw("Mouse X") * lookSens.RuntimeValue;
            float mouseY = Input.GetAxisRaw("Mouse Y") * lookSens.RuntimeValue;

            playerBody.Rotate(Vector3.up * mouseX);
            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, -75f, 75f);
            //float tiltZRot = -horizontal * tiltAngle;
            // Quaternion newRot = Quaternion.Euler(xRot, 0f, tiltZRot);
            //playerCam.localRotation = Quaternion.Slerp(camLocalRot, newRot, tiltSmoothing);
            Quaternion newRot = Quaternion.Euler(xRot, 0, 0);
            playerCam.localRotation = newRot;
            //camLocalRot = Quaternion.Slerp();
            //playerCam.localRotation = camLocalRot;

        }
    }

    IEnumerator DashCD()
    {
        yield return new WaitForSeconds(dashInterval);
       // anim.ResetTrigger("Dash");
        canDash = true;
    }



    public void CharacterMovement(Vector3 moveDir)
    {
        newMoveDir = moveDir.normalized * moveSpeed * Time.fixedDeltaTime;
        //rb.MovePosition(playerBody.position + moveDir);
    }

    public void Knockback(GameObject obj)
    {
        Vector3 dir = (playerPosition.value - obj.transform.position).normalized;
        dir.y = 0;
        rb.AddForce(dir * 100, ForceMode.Impulse);
    }

    public void HitDash()
    {
        if(hitDashRoutine == null)
        {
            hitDashRoutine = StartCoroutine(hitDashCR());
        }
        
            
    } 
    IEnumerator hitDashCR()
    {
        Vector3 originalPos = transform.position;
        Vector3 targetPos = hitTargetVector3 - (hitTargetVector3 - transform.position).normalized * 3;
        targetPos.y = transform.position.y;
        Vector3 targetDir = (targetPos - transform.position).normalized;


        if (Vector3.Distance(transform.position, hitTargetVector3) < 3)
        {
            hitDashRoutine = null;
            yield break;
        }
        rb.isKinematic = true;
        camLock.RuntimeValue = true;
        yield return new WaitForSeconds(0.1f);
        //playerBody.localRotation = Quaternion.LookRotation(targetPos, transform.up);
        float t = 0;
        while (true)
        {
            t += Time.deltaTime;
            if (t > 1)
            {
                break;
            }
            if (Vector3.Distance(transform.position, targetPos) <= 0.05f || Vector3.Distance(transform.position, originalPos) >= (hitTargetVector3 - transform.position).magnitude)
            {
                break;
            }
            else
            {
                rb.MovePosition(transform.position + targetDir * speed * Time.deltaTime);
            }
            yield return null;
        }
        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
        camLock.RuntimeValue = false;
        hitDashRoutine = null;
        
    }
    private void OnEnable()
    {
        //pEC_SO.OnEventMove_Input += CharacterMovement;
        //hitPlayerEventChannel.OnEventRaised += Knockback;
        //pEC_SO.OnEventCamera_Input += CameraMovement;
    }

    private void OnDisable()
    {
        //pEC_SO.OnEventMove_Input -= CharacterMovement;
        //hitPlayerEventChannel.OnEventRaised -= Knockback;
        //pEC_SO.OnEventCamera_Input -= CameraMovement;
    }


   
}
