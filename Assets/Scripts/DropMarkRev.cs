using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMarkRev : MonoBehaviour
{
    Animator myAnimator;
    BoxCollider2D myFeet;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        //myAnimator = GetComponent<Animator>();
        myAnimator = GameObject.Find("Guame").GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        body = GameObject.Find("Check Mark").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        reactivate();
    }

    private void reactivate()
    {
        int index = FindObjectOfType<MomManager>().currentLine;
        if (index == 23)
        {
            GameObject.Find("Check Mark").GetComponent<CheckDeath>().enabled = true;
            body.bodyType = RigidbodyType2D.Dynamic;

        }
    }
}
