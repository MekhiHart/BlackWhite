using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // * Private
    SpriteRenderer _player_Sprite;
    List<Color> _playerColor_List;

    float _horizontalInput;
    bool _verticalInput;
    bool _is_Grounded = false;
    float _jumpTimer;
    bool _is_Jumping = false;
    int _current_ColorIdx = 0;
    Animator animator;
    

    // * Public
    public float speed;
    public static float jumpForce;
    public float jumpDuration;


    // * Static
    public static bool _abilityUsed = false; // * Checked by other classes
    public static Rigidbody2D _rb; // * Used for trampoline
    public static BoxCollider2D playerCollider;
    public static Transform playerTransform;
    public static bool isInverted;


    // * Delegates and Events
    public delegate void playerEvents();
    public static event playerEvents shift_States;
    public static event playerEvents restartLevel;
    // Start is called before the first frame update


    // * Sounds
    public string jumpSound = "Jump";
    public string abilitySound = "Light_Switch";
    public string reverseTrampoline_Sound = "Reverse_Trampoline";
    public string landSound = " Player_Land";
    public string normalTrampoline_Sound = "Normal_Trampoline";
    void Start()
    {
        jumpForce = 10;
        isInverted = false;

        playerTransform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();

        _player_Sprite = GetComponent<SpriteRenderer>();

        _playerColor_List = new List<Color>();
        _playerColor_List.Add(Color.white);
        _playerColor_List.Add(Color.black);

        animator = GetComponent<Animator>();

        // * Ak sounds

    }
    // Update is called once per frame
    void Update() 
    {
        PowerController();
        Jump();

        // * Animation; Falling
        if (!isInverted){
            if (_rb.velocity.y < 0 && !_is_Grounded) animator.SetBool("Falling", true);
            else animator.SetBool("Falling", false);
        }
        else{
            if (_rb.velocity.y > 0 && !_is_Grounded) animator.SetBool("Falling", true);
            else animator.SetBool("Falling", false);
        }


        RestartLevel();


    } // *Update

    private void FixedUpdate() {
        HorizontalMovement();
    } // * FixedUpdatea

    private void HorizontalMovement(){
        _horizontalInput = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2 (_horizontalInput * speed, _rb.velocity.y);

        // * Animation
        if (_horizontalInput != 0 ) animator.SetBool("Running", true);
        else animator.SetBool("Running", false);

        if (_horizontalInput > 0) transform.localScale = new Vector3(4,transform.localScale.y,transform.localScale.z);
        if (_horizontalInput < 0) transform.localScale = new Vector3(-4,transform.localScale.y,transform.localScale.z);

    } // * HorizontalMovement

    private void Jump(){ // ! Jumps duration depends on jump button pressed
        _verticalInput = Input.GetKeyDown(KeyCode.Space); // * True if space pressed, otherwise False

        if (_is_Grounded && _verticalInput ){ // * Jumps if grounded and jump button is pressed
            // jumpForce = isInverted ? -jumpForce : jumpForce;
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce );
            _is_Jumping = true;
            _jumpTimer = jumpDuration; // * starts jump timer

        }

        if (Input.GetKey(KeyCode.Space) && _is_Jumping){ // * If the player is holding the jump button
            // * Once it ends, make sure player hits grounds first to restart timer again to prevent flying
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce ); // * Keeps jumping while holding button
            if (_jumpTimer > 0){
                _jumpTimer -= Time.deltaTime; // * Reduces time
            }

            else{ // * Stops jumping after timer ends
                _is_Jumping = false;             }
        }

        if (Input.GetKeyUp(KeyCode.Space)){ // * Stops jumping after letting go of jump button
            _is_Jumping = false; // * Prevents reducing fall speed after player lets go of jump button
            // animator.SetBool("Jumping", false);
        }


        // * Animation
        animator.SetBool("Jumping",_is_Jumping);

        // if (!isInverted){ // ! wtf is this solution lol; code of shame lmfao
        //     if (_rb.velocity.y > 0){ // * When the charcter is not inverted
        //         animator.SetBool("Jumping",true);
        //     }
        //     else if (_rb.velocity.y < 0) animator.SetBool("Jumping",false);
        //     // animator.SetBool("Jumping", _is_Jumping);
        // }
        // else{ // * When the character is inverted, all of it is just
        //     if (_rb.velocity.y < 0){
        //         animator.SetBool("Jumping",true);
        //     }
        //     else if (_rb.velocity.y > 0) animator.SetBool("Jumping",false);
        //     // animator.SetBool("Jumping", _is_Jumping);
        // }



    } // * Jump

    private void OnCollisionEnter2D(Collision2D other) { // * Comes from the boxcollider component
        if (other.gameObject.tag == "Ground"){ // * If player collided with a game object with atag of ground
            _is_Grounded = true; 
            _abilityUsed = false; // * resets the player ability

            print("Player colides with a gameOBject with the tag ground");
        }
    } // * OnCollisionEnter

    private void OnCollisionExit2D(Collision2D other) { 
        if (other.gameObject.tag == "Ground"){
            _is_Grounded = false;
        }
    }// * OnCOllisionExit

    private void Switch_SpriteColor(){
        if (_current_ColorIdx == 0){
            _current_ColorIdx += 1;
        }
        else{
            _current_ColorIdx = 0;
        }
        _player_Sprite.color = _playerColor_List[_current_ColorIdx];
        

    } // * Switch_SpriteColor

    private void PowerController(){ // * Only allows use of power once after a jump

        if (Input.GetKeyDown(KeyCode.RightShift)){
            if (!_abilityUsed){
                Switch_SpriteColor();
                _abilityUsed = true;
                shift_States(); // * Sends events to subscribers
            }
        }
    } // * PowerController

    private void RestartLevel(){ // * Sends a signal to GameManager to restart level
        // * restarts level if player falls towards the up and down direction
        if (transform.position.y < -40 || transform.position.y > 28) restartLevel(); 
    } // * RestartLevel
}
