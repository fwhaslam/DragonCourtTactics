//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Controller {

    using Shared;

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using static Shared.UnityTools;

    public class EntrySceneController : MonoBehaviour {

        public void ExitGame() {
            CloseGame();
	    }

        public void EditPuzzle() {
            ChangeScene( GlobalValues.EditSceneName );
		}

        public void PlayCampaign() {
            ChangeScene( GlobalValues.CamnpaignSceneName );
		}

        public void ShowCredits() {

		}
    }

}
