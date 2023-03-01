using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Crouching", true);
    }
    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.run);

        if (movement.IsGrounded() && Input.GetKeyDown(KeyCode.Space)) ExitState(movement, movement.jump);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(movement.dir.magnitude < 0.1f) ExitState(movement, movement.idle);
            else ExitState(movement, movement.walk);
        }
        if (movement.verticalInput < 0) movement.currentMoveSpeed = movement.crouchBackSpeed;
        else movement.currentMoveSpeed = movement.crouchSpeed;
    }
    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.SwitchState(state);
        movement.anim.SetBool("Crouching", false);

    }
}
