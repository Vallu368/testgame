using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Walking", true);
    }
    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movement, movement.run);
        }
        else if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            ExitState(movement, movement.crouch);
        }
        else if(movement.dir.magnitude < 0.01f)
        {
            ExitState(movement, movement.idle);
        }

        void ExitState(MovementStateManager movement, MovementBaseState state)
        {
            movement.SwitchState(state);
            movement.anim.SetBool("Walking", false);

        }
    }
}
