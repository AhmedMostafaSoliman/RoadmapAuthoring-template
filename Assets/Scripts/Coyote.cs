using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Coyote : MonoBehaviour
{
    [Header("Coyote animation and gravity")]
    public CharacterController cc;
    public float gravity = -9.81f;
    //public float gravity = -5.81f;
    public Animator animator;

    [Header("Coyote animation and motion and velocity")]
    public float turnCalmTime = 0.1f;
    public float turnCalmVelocity;
    public float jumpRange = 1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;

    [Header("Coyote Script Camera")]
    public Transform coyoteCamera;
    //public GameObject endGameMenuUI;

    [Header("Coyote Movement")]
    public float coyoteSpeed = 1.9f;
    //public float playerSprint = 3f;
    public float coyoteSprint = 1f;

    private int curframe = 0;

    // a hard coded array of values for both the x and y axis
    public float[] x = { 0.0f, 1.0f, 2.0f, 3.0f, -1.0f, -1.0f, -1.0f};
    public float[] y = { 0.0f, 1.0f, 2.0f, 3.0f, -1.0f, -1.0f, -1.0f };

    public InputData _inputData;
    // Update is called once per frame

    void Start()
    {
        _inputData = FindObjectOfType<InputData>();
    }
    void Update()
    {
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightStick))
        {
            x[curframe] = rightStick.x;
            y[curframe] = rightStick.y;
        }
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);


        coyoteMove();
        curframe = (curframe + 1) % x.Length;
    }

    void coyoteMove()
    {
        float horizontalAxis = x[curframe];
        float verticalAxis = y[curframe];

        Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;


        //Player walk or move around
        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("Walk", true);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + coyoteCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cc.Move(moveDirection.normalized * coyoteSpeed * Time.deltaTime);
            // apply a transform to the object itself too
            transform.Translate(moveDirection.normalized * coyoteSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }


    void CoyoteMoveOld()
    {
        
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

        //Player walk or move around
        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            //animator.SetBool("Running", false);
            //animator.SetBool("RiffleWalk", false);
            //animator.SetBool("IdleAim", false);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + coyoteCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cc.Move(moveDirection.normalized * coyoteSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Selection", false);
        }    

    }

}