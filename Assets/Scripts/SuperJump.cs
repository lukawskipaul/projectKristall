using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : PowerUp {

    [SerializeField]
    BasicMove playerBasicMove;

    public override string PowerName
    {
        get
        {
            return "Super Jump";
        }
    }

    private void FixedUpdate()
    {
        if (this.IsActivated)
        {
            Jump();
        }
        
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift) && playerBasicMove.IsGrounded())
        {
            playerBasicMove.playerRigidbody.AddForce(Vector3.up * (playerBasicMove.jumpForce * playerBasicMove.superJumpModifier), ForceMode.Impulse);

        }
    }
}
