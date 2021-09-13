using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandlerScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    /// <summary>
    /// Change to another scene.
    /// </summary>
    /// <param name="next"></param>
    public void ChangeScene( string next ) {
        print("Loading "+next);
        SceneManager.LoadScene( next );
	}
    

    public void ExitGame() {
        print("Try to Exit");

         #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
         #elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
         #else
         Application.Quit();
         #endif
	}
}
