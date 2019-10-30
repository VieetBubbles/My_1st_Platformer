using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightExit : MonoBehaviour
{
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        setExitAnimation();
    }

    private void setExitAnimation()
    {
        int index = FindObjectOfType<DialogManager>().currentLine;
        if (index == 5)
        {
            myAnimator.SetTrigger("boo");
        }

    }
}
