using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;
    public string[] dialogLines;
    public int currentLine;

    // Start is called before the first frame update
    void Start()
    {
        dialogText.text = dialogLines[currentLine];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            // GetButtonUp is for strings so you miust get input window strings
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                currentLine += 1;

                if (currentLine >= dialogLines.Length)
                {
                    dialogBox.SetActive(false);
                    SceneManager.LoadScene(3);

                }
                else
                {
                    dialogText.text = dialogLines[currentLine];
                }
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                currentLine -= 1;
                dialogText.text = dialogLines[currentLine];
            }
        }
    }
}
