using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrampoline : Trampoline
{

    // ! Update function is inherited, and used the overided Trampoline_Effect as a helper function inside of Update
    protected override void Trampoline_Effect()
    {
        // throw new System.NotImplementedException();
        // trampolineForce = PlayerController.isInverted ? -trampolineForce : trampolineForce; // * Checks if player is inverted or not
        base.Trampoline_Effect(); // * Resets player ability
        if (Input.GetKeyDown(KeyCode.Space)) PlayerController._rb.velocity = new Vector2(PlayerController._rb.velocity.x, PlayerController.isInverted ? -trampolineForce : trampolineForce); // * Jump Effect depending on player's gravity
    }

}
