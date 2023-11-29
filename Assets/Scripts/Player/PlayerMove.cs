using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    public PlayerEventChannelSO pecSO;
    public CharacterController charCtrl;
    public Transform playerBody;
    public Transform playerCam;
    public float rotSpeed;
    Vector3 vel;
    float xRotation = 0f;

    [Header("Movement Values")]
    public float jumpHeight;
    public float moveSpeed;

    [Header("On Ground Detection")]
    public float gravity = -9.81f;
    bool onGround;
    [SerializeField]
    Transform groundCheck;
    float groundDistance = 0.1f;
    [SerializeField]
    LayerMask groundMask;

    [SerializeField] Vector3Variable playerPosition;
    private void Awake()
    {
        charCtrl = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (onGround && vel.y < 0)
        {
            vel.y = -0.5f;
        }
        
        vel.y += gravity * Time.deltaTime;
        charCtrl.Move(vel * Time.deltaTime);
        playerPosition.value = transform.position;
    }

    public void Movement(Vector3 moveDir)
    {
        charCtrl.Move(moveDir * moveSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        Debug.Log("Player on the ground: " + onGround);
        if (onGround)
        {
            Debug.Log(vel.y);
            vel.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log(vel.y);
        }
    }


    public void CameraMove(float mouseX, float mouseY)
    {
        playerBody.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -75f, 75f);
        playerCam.localRotation = Quaternion.Lerp(playerCam.localRotation, Quaternion.Euler(xRotation, 0f, 0f), rotSpeed);
    }
  

    private void OnEnable()
    {
        pecSO.OnEventMove_Input += Movement;
        pecSO.OnEventJump_Input += Jump;
        pecSO.OnEventCamera_Input += CameraMove;
    }

    private void OnDisable()
    {
        pecSO.OnEventMove_Input -= Movement;
        pecSO.OnEventJump_Input -= Jump;
        pecSO.OnEventCamera_Input -= CameraMove;
    }

}
