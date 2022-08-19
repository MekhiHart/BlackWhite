using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMutable : MonoBehaviour
{

    public bool state;
    public bool is_stateLocked;
    protected  BoxCollider2D _col; 
    protected SpriteRenderer spriteRender;

    
    // Start is called before the first frame update

    protected virtual void Start()
    {
        _col = GetComponent<BoxCollider2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        _col.enabled = state == false ? true : false;
        spriteRender.color = state == false ? Color.white : Color.black;

        // if (state == "Grey") 
        //     spriteRender.color = Color.grey;

        // else spriteRender.color = Color.white;
    } // * Update

    protected virtual void OnEnable() { // Subcribing Events
        PlayerController.shift_States += ChangeState;
    }

    protected virtual void OnDisable() { // Unscribing events
        PlayerController.shift_States -= ChangeState;
    }


    protected virtual void ChangeState(){
        if (!is_stateLocked){

            state = (state == true) ? false : true;
            _col.enabled = (_col.enabled ==  true) ? false : true; // * Allows state changes
        } 
        else{ // * State is always locked, just change the color of it
            spriteRender.color = spriteRender.color == Color.black ? Color.white : Color.black;
        }
    } // * ChangeState


}
