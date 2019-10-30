using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDeath : MonoBehaviour
{
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //myAnimator = GetComponent<Animator>();
        myAnimator = GameObject.Find("Guame").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // destroy the coin sprite: which is a part of this game object
        myAnimator.SetTrigger("dead");
        Destroy(gameObject);
    }
    
}
