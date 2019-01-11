using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class PlayerMovement : MonoBehaviour
{
    public InputManager inputManager;

    // Jumping:
    private Rigidbody rb;
    public LayerMask groundLayer;
    public float jumpForce;
    private BoxCollider col;
    private bool normalJump;
    private bool wallJump;

    public bool moveUp;
    public bool moveDown;

    // GroundCheck:
    protected Collider coll;


    // Movement
    public int speed;
    public int basespeed = 5;
    private Vector3 moveDirection;
    private bool view;      // Toggle FP, TP 

    // Camera stuff:
    public Camera mainCam;
    public Camera uiCam;
    private Vector3 offsetX;
    private Vector3 offsetY;
    private Vector3 firstPersonOffset;
    private float turnSpeed = 2.0f;

    public bool rotate = true;
    public bool move = true;
   

    void Start()
    {
        // Player
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        coll = GetComponent<Collider>();
        view = false;
        wallJump = true;
        
        
        offsetX = new Vector3(0f, 2.0f, 2.0f);
        offsetY = new Vector3(0f, 1.0f, 2.0f);
        firstPersonOffset = new Vector3(0.0f, 0.5f, 0.0f);


        // Mouse:
        Cursor.lockState = CursorLockMode.Locked;

        // Input registration
        inputManager.RegisterAction(InputManager.Keys.toggleView, ToggleView);
        inputManager.RegisterAction(InputManager.Keys.jump, Jump);
        inputManager.RegisterAction(InputManager.Keys.movement, () => Move(inputManager.data));
    }

    public void ToggleView()
    {
        view = !view;
    }

    public void Jump()
    {
        if (normalJump)
            rb.AddForce(new Vector3(0, 13.5f, 0));
    }

    public void Move(InputManager.CallbackData data)
    {
        if (move)
        {
            var forward = mainCam.transform.forward;
            var right = mainCam.transform.right;
            var up = Vector3.up;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            Vector3 moveDir;
            if (moveUp)
            {
                moveDir = 0.01f * speed * forward * data.xAxis + 0.01f * speed * right * data.yAxis + 0.01f * speed * up * 1;
            }
            else if (moveDown)
            {
                moveDir = 0.01f * speed * forward * data.xAxis + 0.01f * speed * right * data.yAxis + 0.01f * speed * up * -1;
            }
            else
            {
                moveDir = 0.01f * speed * forward * data.xAxis + 0.01f * speed * right * data.yAxis;
            }

            if (view)   // Rotation in third person.
            {
                if (moveDir.magnitude > 0.001)
                {
                    transform.rotation = Quaternion.LookRotation(moveDir);
                }
            }
            moveDir *= 1; //walkSpeed;

            //Player movement
            transform.position += moveDir;
            //Camera movement
            moveDir.y = 0;
            mainCam.transform.position += moveDir;
            uiCam.transform.position += moveDir;
        }
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        firstPersonOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * firstPersonOffset;
        offsetX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;
        offsetY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offsetY;  // Y rotation is kinda weird right now.

        if (view)   // Third person view
        {   
            mainCam.transform.position = transform.position + offsetX; // + offsetY;
            uiCam.transform.position = mainCam.transform.position;
            var temp = transform.position;
            temp.y += 0.5f;
            mainCam.transform.LookAt(temp);
            uiCam.transform.LookAt(temp);
        }
        else        // First person view
        {
            mainCam.transform.position = transform.position + firstPersonOffset;
            uiCam.transform.position = mainCam.transform.position;
            
            float y = Input.GetAxis("Mouse X");
            float x = Input.GetAxis("Mouse Y");
           

            if (rotate)
            {
                // Rotate player:
                transform.eulerAngles = transform.eulerAngles - new Vector3(0, y * -turnSpeed, 0);
                // Rotate camera:
                mainCam.transform.eulerAngles = mainCam.transform.eulerAngles - new Vector3(x, y * -turnSpeed, 0);  // Is now doing some weird stuff when rotated beyond top/bottom.
                uiCam.transform.eulerAngles = mainCam.transform.eulerAngles;

                // Set rotation relative to player:
                mainCam.transform.eulerAngles = new Vector3(mainCam.transform.eulerAngles.x, transform.eulerAngles.y, mainCam.transform.eulerAngles.z);
                uiCam.transform.eulerAngles = mainCam.transform.eulerAngles;
            }
        }

        if (checkGround())
        {
            wallJump = true;
            normalJump = true;
        }
        else
            normalJump = false;

        speed = basespeed;

        // Boundary:         // Should set spawn points.
        if (transform.position.y < -10)
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);

    }


    void OnCollisionStay(Collision col)
    {
        ContactPoint contact = col.contacts[0];             //Walljump:

        if (Input.GetKey(KeyCode.Space) && contact.normal.y < 0.3 && wallJump       // walljump if a wall
            && contact.otherCollider.material.name != "invisibleWall (Instance)")  // && not invisible wall
        {
            rb.velocity = new Vector3(0, 0, 0);
            RaycastHit hit;
            Physics.Raycast(contact.point, -Vector3.up, out hit);
            if (hit.distance > 0.1)
            {
                rb.velocity = new Vector3(0, 0, 0);
                Debug.DrawRay(contact.point, contact.normal, Color.red, 3f, true);
                rb.velocity += Vector3.up * 6.2f;
                wallJump = false;
            }
        }
    }

    bool checkGround()
    {
        var temp1 = transform.position; temp1.x -= col.size.x / 2; temp1.z -= col.size.z / 2;
        var temp2 = transform.position; temp2.x += col.size.x / 2; temp2.z -= col.size.z / 2;
        var temp3 = transform.position; temp3.x -= col.size.x / 2; temp1.z += col.size.z / 2;
        var temp4 = transform.position; temp4.x += col.size.x / 2; temp1.z += col.size.z / 2;
        if (Physics.Raycast(temp1, Vector3.down, col.bounds.extents.y + 0.4f) || Physics.Raycast(temp2, Vector3.down, col.bounds.extents.y + 0.4f)
            || Physics.Raycast(temp3, Vector3.down, col.bounds.extents.y + 0.4f) || Physics.Raycast(temp4, Vector3.down, col.bounds.extents.y + 0.4f))
            return true;
        else
            return false;
    }

}