using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteGameSession : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            Destroy(FindObjectOfType<GameSession>().gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
