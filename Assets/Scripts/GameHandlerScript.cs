//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

using Arena;

using Realm;

using Shared;

using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandlerScript : MonoBehaviour {

    /// <summary>
    /// Access the DialogBox for save/load dialogs.
    /// </summary>
    public GameObject dialogBox;
    public string puzzlesFolder = Shared.GlobalValues.SavedPuzzlesFolder;

    internal DialogBoxScript dialogScript;
    internal List<string> filenames;

	public void Awake() {
		dialogScript = dialogBox.GetComponent<DialogBoxScript>();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 //======================================================================================================================



    /// <summary>
    /// Show a confirm dialog box, and if true then switch the _mainScene.
    /// </summary>
    public void ConfirmAndExit() {

        dialogScript.ShowQuestionDialog( "Exit Edit Mode", null, "Are you done with edit mode?", 
            delegate(){ChangeScene( Shared.GlobalValues.EntrySceneName); }
        );

	}

    /// <summary>
    /// Show a confirm dialog box, and if true then switch the _mainScene.
    /// </summary>
    public void LoadPuzzle() {

        ReadFilenamesFromPuzzleFolder( GetPuzzleFolderPath() );

        dialogScript.ShowFileLoadDialog( filenames, 
            delegate(){ LoadSelectedFile(); }
        );

	}
    
    internal string GetPuzzleFolderPath() {
        return Shared.GlobalValues.SavedPuzzlesFolder;
	}

    internal void ReadFilenamesFromPuzzleFolder( string folderPath ) {


        var info = new DirectoryInfo( UnityTools.FixFilePath(folderPath) );
print("EXISTS?="+info.Exists);
print("DIR="+info.FullName );

        FileInfo[] files = info.GetFiles( );

        List<string> filenames = new List<string>();
        foreach ( var file in files ) {
print("FILEINFO = "+file.FullName );
            if (file.FullName.EndsWith( GlobalValues.PuzzleFileExtension ) ) {
                int clip = GlobalValues.PuzzleFileExtension.Length;
                var name = file.Name.Substring( 0,  file.Name.Length - clip );
                filenames.Add(name);
            }
		}
        
        this.filenames = filenames;
	}

    internal void LoadSelectedFile() {

        var fileIx = dialogScript.GetSelectedFileIndex();
        var filename = filenames[ fileIx ];

        var filePath = GetPuzzleFolderPath() + "/" + filename + GlobalValues.PuzzleFileExtension;
        var content = File.ReadAllText( UnityTools.FixFilePath( filePath ) );
print("Loaded File = "+content);

        var levelMap = RealmManager.ParseLevelMap( content );
        ArenaManagerScript.mapLoadEvent.Invoke( levelMap );
	}

 //======================================================================================================================

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
