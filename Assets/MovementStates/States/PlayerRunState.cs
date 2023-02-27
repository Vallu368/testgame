using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Running", true);
    }
    public override void UpdateState(MovementStateManager movement)
    {
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitState(movement, movement.walk);
        }
        else if (movement.dir.magnitude < 0.01f)
        {
            ExitState(movement, movement.idle);
        }
    }
    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.SwitchState(state);
        movement.anim.SetBool("Running", false);

    }
}
