using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Jumping", true);
        movement.velocity.y += movement.jumpForce;
        movement.currentMoveSpeed = movement.jumpSpeed;
        Debug.Log("jump");

    }
    public override void UpdateState(MovementStateManager movement)
    {
        if (movement.IsGrounded() && movement.velocity.y < 0.01f)
        {
            Debug.Log("grounded");
            if (movement.dir.magnitude < 0.01f) ExitState(movement, movement.idle);
            else if (Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.run);
            else ExitState(movement, movement.walk);

        }
        else return;
    }
    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.SwitchState(state);
        movement.anim.SetBool("Jumping", false);
    }


}
