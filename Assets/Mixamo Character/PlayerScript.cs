using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.9f;
    //public float playerSprint = 3f;
    public float playerSprint = 1f;

    [Header("Player Health")]
    public float playerHealth = 120f;
    public float presentHealth;

    public int playerHealthInitalHealt2Int = 120;

    public Text playerHealthScoreText;
    public int convertCUrrentPlayerHealth2Int;

    //public HealthBarScript healthBar;

    [Header("Player Script Camera")]
    public Transform playerCamera;
    public GameObject endGameMenuUI;


    [Header("Player animation and gravity")]
    public CharacterController cc;
    public float gravity = -9.81f;
    //public float gravity = -5.81f;
    public Animator animator;

    [Header("Player animation and jumping and velocity")]
    public float turnCalmTime = 0.1f;
    public float turnCalmVelocity;
    public float jumpRange = 1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;

    float turnSpeed = 0.5f;
    float movementSpeed = 0.5f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        presentHealth = playerHealth;


        convertCUrrentPlayerHealth2Int = playerHealthInitalHealt2Int;//keepng track of health score in order to use it for scoreboard
        //healthBar.GiveFullHealthToPlayer(playerHealth);
    }
    private void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);


        playerMove();
        jump();
        sprint();
        checkIfPlayerIsBelowGroundLevel();
        //pointTowardsMouseDirection();
    }

    void playerMove()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;


        //Player walk or move around
        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            animator.SetBool("Running", false);
            animator.SetBool("RiffleWalk", false);
            animator.SetBool("IdleAim", false);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cc.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
        }
    }

    void jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.ResetTrigger("Jump");
        }
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
                animator.SetBool("Running", true);

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                cc.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Running", false);
            }
        }
    }

    public void playerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;

        convertCUrrentPlayerHealth2Int -= (int) takeDamage; //This is the current player's health displayed at scoreboard.       
        playerHealthScoreText.text = convertCUrrentPlayerHealth2Int + "/" + playerHealthInitalHealt2Int; //This is the current player's health displayed at scoreboard.
        
        //healthBar.SetHealth(presentHealth);

        if (presentHealth <= 0)
        {
            playerDie();
        }
    }

    private void playerDie()
    {
        endGameMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject, 1.0f);
        //Invoke(nameof(ReloadLevel), 1.3f);
        //ReloadLevel();
    }

    private void checkIfPlayerIsBelowGroundLevel()
    {
        if (transform.position.y < -2f)
        {
            playerDie();
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /*public void pointTowardsMouseDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }*/

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

}
