using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed = 5;
    public float crouchSpeed = 2, crouchBackSpeed = 1;
    public float jumpSpeed = 3;

    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float horizontalInput, verticalInput;
    [HideInInspector] public float animHzInput, animVInput;
    CharacterController controller;

    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;
    public Vector3 velocity;
    public float jumpForce;

     MovementBaseState currentState;
    public PlayerIdleState idle = new PlayerIdleState();
    public PlayerWalkState walk = new PlayerWalkState();
    public PlayerRunState run = new PlayerRunState();
    public PlayerCrouchState crouch = new PlayerCrouchState();
    public PlayerJumpState jump = new PlayerJumpState();


    [HideInInspector] public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        SwitchState(idle);
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
        currentState.UpdateState(this);

        anim.SetFloat("hInput", animHzInput);
        anim.SetFloat("vInput", animVInput);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        animHzInput = Input.GetAxis("Horizontal");
        animVInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        dir = transform.forward * verticalInput + transform.right * horizontalInput;

        controller.Move(dir.normalized * currentMoveSpeed * Time.deltaTime);
    }
    public bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        else return false;
    }
    void Gravity()
    {
        if(!IsGrounded())
        {
            anim.SetBool("isGrounded", false);
            velocity.y += gravity * Time.deltaTime;
        }
        else if(velocity.y < 0)
        {
            velocity.y = -2;
            anim.SetBool("isGrounded", true);
        }
        controller.Move(velocity * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }
}
