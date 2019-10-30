using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // field that deteremines how fast the enemy sprite moves
    [SerializeField] float moveSpeed = 1f;
    public int health;
    private float dazeTime;
    public float startDazedTime;

    Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dazeTime <= 0)
        {
            moveSpeed = 1f;
        }
        else
        {
            moveSpeed = 0;
            dazeTime -= Time.deltaTime;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        // if enemy sprite is facing right
        // has the enemy sprite been flipped or not
        if (IsFacingRight())
        {
            if (dazeTime <= 0)
            {
                moveSpeed = 1f;
            }
            else
            {
                moveSpeed = 0;
                dazeTime -= Time.deltaTime;
            }
            // statement that moves the enemy sprite right on the x axis
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            if (dazeTime <= 0)
            {
                moveSpeed = 1f;
            }
            else
            {
                moveSpeed = 0;
                dazeTime -= Time.deltaTime;
            }
            // statement that moves the enemy sprite left on the x axis
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    // method that returns true if enemy sprite is facing right and false if facing left
    // method return value is changed based on what triggerExit returns
    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    // Does not need to be in updates because this method is based on the trigger checkbox
    // located on the enemy game object's Box Collider 2D component saying the box is a trigger
    // How: When the moment the box collider exits a trigger volume, exits the collision,
    // (i.e it finishes the collision with another collider)
    // then we trigger the local scale, flipping the enemy. 
    private void OnTriggerExit2D(Collider2D collision)
    {
        // the - minus is so that the enemy sprite does the opposite of what it was originally doing
        // i.e force the enemy to switch directions by changing velocity x axis value 1 to -1 or vice versa
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    public void TakeDamage(int damage)
    {
        dazeTime = startDazedTime;
        health -= damage;
        Debug.Log("Damage Taken");
    }
}
