using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //scene loading
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        ExitOnPress();  
    }

    public void LoadTutorial() //set as public so method can be accessed in inspector. 
    {
        SceneManager.LoadScene(1); //assigned original game as 1, menu is 0, innovative is 2
    }

    public void LoadPlay()
    {
        //supposed to load index 1,
        //but only have scene3 rn.
        SceneManager.LoadScene(2);
    }

    //Load all levels
    

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //used to end game in editor
#endif
#if UNITY_STANDALONE
        Application.Quit(); //forces Unity to be closed, so wouldn't work in editor
#endif
    }

    private void ExitOnPress()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }
}
