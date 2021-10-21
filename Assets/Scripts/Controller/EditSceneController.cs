//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Controller {
    using Arena;

    using Realm;
    using Realm.Enums;
    using Realm.Puzzle;

    using Shared;

    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class EditSceneController : MonoBehaviour {

        // Access the DialogBox for save/load dialogs.
        public GameObject dialogBox;

        // local folder with puzzle saves
        public string puzzlesFolder = Shared.GlobalValues.SavedPuzzlesFolder;

        internal DialogBoxScript dialogScript;

	    public void Awake() {
		    dialogScript = dialogBox.GetComponent<DialogBoxScript>();
	    }

     //======================================================================================================================

        /// <summary>
        /// Show a confirm dialog box, and if true then switch to _mainScene.
        /// </summary>
        public void ConfirmAndExit() {

            dialogScript.ShowConfirmDialog( "Exit Edit Mode", null, "Are you done with edit mode?", 
                delegate() {
                     ChangeScene( Shared.GlobalValues.EntrySceneName); 
                }
            );

	    }

        /// <summary>
        /// Show a confirm dialog box, and if true then switch the _mainScene.
        /// </summary>
        public void LoadPuzzle() {

            List<string> filenames = ReadFilenamesFromPuzzleFolder( GetPuzzleFolderPath() );

            //dialogScript.ShowFileLoadDialog( filenames, delegate(){ LoadSelectedFile(); } );
            dialogScript.ShowFileLoadDialog( filenames, LoadSelectedFile );

	    }

        /// <summary>
        /// Show a confirm dialog box, and if true then switch the _mainScene.
        /// </summary>
        public void SavePuzzle() {

            List<string> filenames = ReadFilenamesFromPuzzleFolder( GetPuzzleFolderPath() );

            //dialogScript.ShowFileLDialog( filenames, delegate(){ LoadSelectedFile(); } );
            dialogScript.ShowFileSaveDialog( filenames, SaveSelectedFile );

	    }

        /// <summary>
        /// Open the play scene, and register edit-scene for return;
        /// </summary>
        public void PlayPuzzle() {

            //GlobalValues.playingMap = GlobalValues.editingMap;
            GlobalValues.playSceneReturn = SceneManager.GetActiveScene().name;
    print(">> Setting RETURN SCENE to "+GlobalValues.playSceneReturn);

            UnityTools.ChangeScene( GlobalValues.PlaySceneName );
	    }

        /// <summary>
        /// Show a confirm dialog box, and if true then switch the _mainScene.
        /// </summary>
        public void ResetPuzzle() {

            dialogScript.ShowConfirmDialog( "Rest Puzzle?", null, 
                "Are you sure you want to discard the current puzzle and reset to default ?",  
                DoResetPuzzle );
	    }

    //======================================================================================================================

        internal string GetPuzzleFolderPath() {
            return Shared.GlobalValues.SavedPuzzlesFolder;
	    }

        internal List<string> ReadFilenamesFromPuzzleFolder( string folderPath ) {


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
        
            return filenames;
	    }

        internal void LoadSelectedFile() {

            var filename = dialogScript.GetSelectedFilename();
            var filePath = GetPuzzleFolderPath() + "/" + filename + GlobalValues.PuzzleFileExtension;

            var content = File.ReadAllText( UnityTools.FixFilePath( filePath ) );
            var levelMap = RealmManager.ParseLevelMap( content );

            ArenaManagerScript.mapLoadEvent.Invoke( levelMap );
	    }

        internal void SaveSelectedFile() {

            var filename = dialogScript.GetSelectedFilename();
            var filePath = GetPuzzleFolderPath() + "/" + filename + GlobalValues.PuzzleFileExtension;

            var content = RealmManager.DumpLevelMap( GlobalValues.GetCurrentMap() );
            File.WriteAllText( filePath, content );

           dialogScript.ShowAcknowledgeDialog( null, null, "Save Complete ["+filename+"]" );
	    }

        internal void DoResetPuzzle() {

            // tell arena handler to 'reset'
            ArenaManagerScript.mapLoadEvent.Invoke( null );

	    }

    //======================================================================================================================

        /// <summary>
        /// Change to another scene.
        /// </summary>
        /// <param name="next"></param>
        public void ChangeScene( string next ) {
            UnityTools.ChangeScene( next );
	    }


        public void ExitGame() {
            UnityTools.CloseGame();
	    }
    }
}
