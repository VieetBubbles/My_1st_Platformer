using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    // field parameter that determines how far the enemy kicks the player
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    // State
    bool isAlive = true;

    // Cache component references
    // How you get access to each component and their respected fields
    // Unity Component name: your variable name for the component;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;

    // Start is called before the first frame update
    // Messages then methods
    void Start()
    {
        // Allows access to components once game starts
        // Make an instance of the components
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        // If you get to the point where the player state is now something not true,
        // then you would never get access to the input commands below the if statement.
        // I.e. it turns off the ability for the player to control the character
        // running arround in the world.
        if (!isAlive)
        {
            return;
        }
        Run();
        Jump();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // Value is between -1 to +1

        // create a new vector2 parameter value that only changes the x-axis velocity
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        // set the velocity field parameter values to be the new vector2 velocity values
        myRigidBody.velocity = playerVelocity;

        // bool that determines the animation for the player sprite when running
        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        // if bool = true then switch to Running animation, else if false then idle
        // Hint: switches the running parameter in animator to on if bool = true
        myAnimator.SetBool("Running", playerHorizontalSpeed);
    }

    private void Jump()
    {
        /* if my player sprite is not happening to be touching/colliding
         * with the ground then don't let my player jump and do nothing
         */
        // Possible to use this method bc script is affecting only player object
        // When moving player object the script will check when player sprite touches a "ground" layer
        // Specifically, if the myfeet hit box is touching the ground. 
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        // Gets the input button for the platform and if the player presses that
        // key -space key- listed in jump settings then change the player's y velocity
        if (Input.GetKeyDown(KeyCode.UpArrow))
        // Alternate method: if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            // create a new vector2 parameter value that only changes the y-axis velocity
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);


            // add the new vector2 parameter values to the original velocity values
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void ClimbLadder()
    {
        /* if my player sprite is not happening to be touching/colliding
         * with the ground then don't let my player climb and do nothing
         */
        // Possible to use this method bc script is affecting only player object
        // When moving player object the script will check when player sprite touches a "climbing" layer
        // Specifically, if the myfeet hit box is touching the ladder. 
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            // if we are not touching a layer that is defined as "Climbing" then
            // the climbing animation shouldn't happen
            myAnimator.SetBool("Climbing", false);
            // set the gravity pushing down on player sprite to default
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");

        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        // set the velocity field parameter values to be the new vector2 velocity values
        myRigidBody.velocity = climbVelocity;
        // set the gravity pushing down on the player when on the ladder to 0
        // i.e no gravity when on ladder
        myRigidBody.gravityScale = 0f;

        // bool that determines the animation for the player sprite when climbing
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;

        // if bool = true then switch to Climbing animation, else if false then idle
        // Hint: switches the climbing parameter in animator to on if bool = true
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    // In order to use this method, make sure that the enemy sprite's collider isTrigger checkbox
    // is checked in. Because the death animation parameter is a trigger. And you need to activate
    // the death animation trigger when you set it.
    // I.e the method "SetTrigger" won't work unless you checkbox the enemy's collider as a trigger.
    private void Die()
    {
        // if the player sprite is touching the enemy sprite containing the layer called "Enemy",
        // then
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            // make it so that the player can't have control of the player sprite.
            //:isAlive = false;
            // sets the current player sprite animation to be the die animation containing the Dying parameter
            //:myAnimator.SetTrigger("Dying");
            // and at the same time...
            // get the player sprite's rigidbody and throw it off into a certain direction
            GetComponent<Rigidbody2D>().velocity = deathKick;
            // Process the lives counter
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void FlipSprite()
    {
        // if the player is moving horizontally
        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        // if playerHorizontalSpeed is true... if playerHorizontalSpeed isn't idle
        if (playerHorizontalSpeed)
        {
            // reverse the current scaling of the x axis
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
}
