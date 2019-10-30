using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBattle : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D myRigidBody;
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            lanceAttack();
            // statement that moves the enemy sprite right on the x axis
            myRigidBody.velocity = new Vector2(moveSpeed * 2f, 0f);
        }
        else
        {
            lanceAttack();
            // statement that moves the enemy sprite left on the x axis
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
            
        }
        
    }

    private void lanceAttack()
    {
        myAnimator.SetBool("fire", true);
        moveSpeed = 5f;
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
}
