using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Portal : StateMutable
{
    // Start is called before the first frame update

    public delegate void portalDelgate(); // * Delegate
    public static event portalDelgate nextLevel; // * Event


    protected override void OnEnable() {
        PlayerController.shift_States += ChangeState;
    }

    protected override void OnDisable() {
        PlayerController.shift_States -= ChangeState;
    }

    protected override void ChangeState()
    {
        spriteRender.color = spriteRender.color == Color.white ?  Color.black : Color.white; // * Changes Sprite Color to opposite of current color
    } // * ChangeState

    private void OnTriggerEnter2D(Collider2D other) { // * When player touches 

        if (other.tag == "Player"){
            nextLevel(); // * Transmits Next Level, connected to GameManager
        }
        
    }

}
