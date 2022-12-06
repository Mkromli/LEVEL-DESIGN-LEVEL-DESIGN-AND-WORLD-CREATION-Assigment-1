using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FpsController : MonoBehaviour
{
    public bool canRun = false;
    public bool canCrouch = false;
    public bool canMove = true;
    


    public float walkingSpeed = 7.5f;
    public float sneakingSpeed = 4f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    private bool _canJump = true;
    public float sneakHeight = 1f;

    
    private bool _isRunning;
    private bool _isSneaking;

    private float curSpeedX;
    private float curSpeedY;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;


    public bool gravityOn = true;

    
    

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //crouching logic
        if(Input.GetKey(KeyCode.C) && canCrouch)
        {
            _isSneaking = true;
            _isRunning = false;
            
            
            characterController.height = sneakHeight;
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, characterController.transform.position.y + 0.25f, playerCamera.transform.position.z);

            
            
            
            
            
        }
        else
        {
            bool areaClear = true;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + new Vector3(0, sneakHeight, 0), 0.5f);
            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider.transform.tag != "grab" && hitCollider.transform.tag != "Player")
                {
                    areaClear = false;
                }
            }
            if(areaClear)
            {
                _isSneaking = false;
            
            
                characterController.height = 2f;
                playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, characterController.transform.position.y + 0.75f , playerCamera.transform.position.z);
            }
            
            
            
        }
        


        //movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        
        
        if(canRun == true && _isSneaking == false)
        {
            _isRunning = Input.GetKey(KeyCode.LeftShift);
        }
        //setting current speed based on movement parameters
        if(_isRunning)
        {
            curSpeedX = runningSpeed * Input.GetAxis("Vertical");
            curSpeedY = runningSpeed * Input.GetAxis("Horizontal");
        }
        else if(_isSneaking)
        {
            curSpeedX = sneakingSpeed * Input.GetAxis("Vertical");
            curSpeedY = sneakingSpeed * Input.GetAxis("Horizontal");
        }
        else if(canMove)
        {
            curSpeedX = walkingSpeed * Input.GetAxis("Vertical");
            curSpeedY = walkingSpeed * Input.GetAxis("Horizontal");
        }

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //float curSpeedX = canMove ? (_isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        //float curSpeedY = canMove ? (_isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        
        //disable jump if stuck under ledge
        if(Physics.CheckSphere(transform.position + new Vector3(0, sneakHeight, 0), 0.2f) == true && _isSneaking)
        {
            _canJump = false;
        }
        else
        {
            _canJump = true;
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && _canJump && gravityOn)
        {
            moveDirection.y = jumpSpeed;
        }
        else if(gravityOn == true)
        {
            moveDirection.y = movementDirectionY;
        }

        
        if (!characterController.isGrounded && gravityOn)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        
        characterController.Move(moveDirection * Time.deltaTime);

        //camera movement
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }



        //Copyright Emmy Hammarström 2022 ;3

    }
}