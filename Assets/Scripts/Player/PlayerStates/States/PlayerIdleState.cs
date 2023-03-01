using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {

    }
    public override void UpdateState(MovementStateManager movement)
    {
        if(movement.dir.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift)) movement.SwitchState(movement.run);
            else movement.SwitchState(movement.walk);
        }
        if (movement.IsGrounded() && Input.GetKeyDown(KeyCode.Space)) movement.SwitchState(movement.jump);
        if (Input.GetKeyDown(KeyCode.LeftControl)) movement.SwitchState(movement.crouch);




    }
}
