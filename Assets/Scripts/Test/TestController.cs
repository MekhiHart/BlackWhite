using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{

    // * Private
    private Rigidbody2D _rigidBody;
    
    private float _horizontalInput;


    // * Public
    public float speed;
    // Start is called before the first frame update
    void Start() // * Callefed before the first frame is updated
    {

        _rigidBody = GetComponent<Rigidbody2D>(); // * Creates a reference to the RigidBody 2D of the gameObject its attached 

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){


        _rigidBody.velocity = new Vector2(1 * speed,_rigidBody.velocity.y);
        // HorizontalMovement();
    }


    void HorizontalMovement(){
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        print(_horizontalInput);

    }

    
}
