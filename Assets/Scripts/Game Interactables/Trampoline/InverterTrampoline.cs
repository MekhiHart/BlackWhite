using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterTrampoline : Trampoline
{

    protected override void Trampoline_Effect()
    {
        base.Trampoline_Effect(); // * Resets player ability

        if (Input.GetKeyDown(KeyCode.Space)){
            // PlayerController
            PlayerController.playerTransform.localScale = new Vector3(PlayerController.playerTransform.localScale.x,PlayerController.playerTransform.localScale.y * -1, PlayerController.playerTransform.localScale.z); // * Inverts y scale
            PlayerController._rb.gravityScale *= -1; // * Inverts Gravity of player when player Jump on this trampoline 
            PlayerController.isInverted = !PlayerController.isInverted; // * Makes it the opposite of what it is
            PlayerController.jumpForce *= -1; // * only changes jumpForce in this script
            
        } 
    }
}
