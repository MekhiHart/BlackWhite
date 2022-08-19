using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : StateMutable
{
    // Start is called before the first frame update

    public float trampolineForce;
    public Color base_SpriteColor;
    public Color switch_BaseColor;
    protected void Update() {
        if (_col.IsTouching(PlayerController.playerCollider)){
            Trampoline_Effect();
            // if (Input.GetKeyDown(KeyCode.Space)) print("JUMP"); // * Trampoline Force
        }
    }

    protected override void Start()
    {
        _col = GetComponent<BoxCollider2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        _col.enabled = !state ;
        // spriteRender.color = state == false ? base_SpriteColor : switch_BaseColor;
        spriteRender.enabled = !state;
    }


    protected override void ChangeState() 
    {
        if (!is_stateLocked){
            state = !state;
            _col.enabled = !state; // * Allows state changes
            spriteRender.enabled = !state;
        } 
        // else{ // * State is always locked, just change the color of it
        //     spriteRender.color = spriteRender.color == Color.black ? Color.white : Color.black;
        // }

    }
    protected virtual void Trampoline_Effect(){
        PlayerController._abilityUsed = false; // ! Restarts player ability and allows another use of ability; keep this when inheriting from this class
    } // * Used for the effect that would 
    
}
