using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Benutze hier ein c#event bzw. das Observer Pattern. Um zu schieﬂen, muss der PlayerController nichts von der Waffe wissen, und umgekehrt auch nicht.
    //Somit spare ich mir die Referenzen der Waffen im PlayerController.
    public delegate void OnShoot(Vector3 spawnPos);
    public static event OnShoot shootBullet;

    public delegate void OnInteract(PlayerController player);
    public static event OnInteract interact;

    public delegate void GameState(GameStates state);
    public static event GameState changeGameState;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] private Transform bulletSpawnPos;

    [SerializeField] private CharacterController charController;

    public Transform PickUpPosition;
    public Transform DropPosition;

    private CharacterController controller;

    public Transform groundCheck;
    //[SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private Vector3 halfBoxLength;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        CheckIfGrounded();

        Move();

        Jump();

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        Shoot();

        Pause();

        Interact();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(groundCheck.position, halfBoxLength);
        Gizmos.color = Color.red;
    }

    void CheckIfGrounded()
    {
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isGrounded = Physics.CheckBox(groundCheck.position, halfBoxLength, Quaternion.identity, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (shootBullet != null)
            {
                shootBullet(bulletSpawnPos.position);
            }
        }
    }

    void Pause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (changeGameState != null)
            {
                changeGameState(GameStates.Pause);
            }
        }
    }

    void Interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            interact(this);
        }
    }

    public void DisableCharacterController()
    {
        charController.enabled = false;
    }
    public void EnableCharacterController()
    {
        charController.enabled = true;
    }
}
