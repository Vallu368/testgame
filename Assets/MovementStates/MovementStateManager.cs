using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float moveSpeed;
    [HideInInspector] public Vector3 dir;
    float horizontalInput, verticalInput;
    CharacterController controller;

    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

     MovementBaseState currentState;
    public PlayerIdleState idle = new PlayerIdleState();
    public PlayerWalkState walk = new PlayerWalkState();
    public PlayerRunState run = new PlayerRunState();
    public PlayerCrouchState crouch = new PlayerCrouchState();

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

        anim.SetFloat("hInput", horizontalInput);
        anim.SetFloat("vInput", verticalInput);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        dir = transform.forward * verticalInput + transform.right * horizontalInput;

        controller.Move(dir.normalized * moveSpeed * Time.deltaTime);
    }
    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        else return false;
    }
    void Gravity()
    {
        if(!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if(velocity.y < 0)
        {
            velocity.y = -2;
        }
        controller.Move(velocity * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }
}
