using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coyote : MonoBehaviour
{
    float turnSpeed = 0.5f;
    float movementSpeed = 0.5f;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);


        coyoteMove();
        //sprint();
    }

    void coyoteMove()
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

    private void TurnTowards(Vector3 towardsVector)
    {
        float singleStep = turnSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, towardsVector, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection, transform.up);
    }

    public void TurnRight()
    {
        TurnTowards(transform.right);
    }

    public void TurnLeft()
    {
        TurnTowards(-transform.right);
    }

    public void MoveForward()
    {
        TurnTowards(transform.forward);
    }

    public void MoveBackwards()
    {
        TurnTowards(-transform.forward);
    }

    void sprint()
    {
        if (Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onSurface)
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Selection", true);

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + coyoteCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                cc.Move(moveDirection.normalized * coyoteSprint * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Selection", false);
            }
        }
    }
}
