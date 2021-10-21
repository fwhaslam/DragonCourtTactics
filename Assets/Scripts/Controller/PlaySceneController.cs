//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Controller { 

    using Shared;

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using static Shared.UnityTools;

    public class PlaySceneController : MonoBehaviour {

        public DialogBoxScript dialogScript;

//======================================================================================================================

        /// <summary>
        /// Reset puzzle to the start.
        /// </summary>
        public void RestartLevel() {
            print("Restart Level");
	    }

        /// <summary>
        /// Show a confirm dialog box, and if true then switch the _mainScene.
        /// </summary>
        public void ConfirmAndExit() {

            dialogScript.ShowConfirmDialog( "Exit Play Mode", null, "Are you done playing?", 
                delegate() { ChangeScene( GlobalValues.playSceneReturn ); }
            );

	    }
    }
}