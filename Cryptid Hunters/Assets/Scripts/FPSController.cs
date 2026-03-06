using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

[RequireComponent(typeof(CharacterController))]
public class NewMonoBehaviourScript : MonoBehaviour
{
    public Camera playerCam;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    [SerializeField] private float jumpHeight = 7f;
    [SerializeField] private float gravity = 50f;

    public float lookSpeed = 2f;
    public float lookXLimit = 50f;

    UnityEngine.Vector3 moveDirection = UnityEngine.Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;


    void Start()
    {
        //initialize a character controller
        //lock cursor for camera
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        UnityEngine.Vector3 forward = transform.TransformDirection(UnityEngine.Vector3.forward);
        UnityEngine.Vector3 right = transform.TransformDirection(UnityEngine.Vector3.right);

        //running
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded) {
            moveDirection.y = jumpHeight;
        } else {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded) {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // camera rotation and player model coupling
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove) {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCam.transform.localRotation = UnityEngine.Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= UnityEngine.Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
