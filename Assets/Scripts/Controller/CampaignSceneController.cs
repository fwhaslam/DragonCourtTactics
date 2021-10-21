//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Controller {

    using Shared;

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using static Shared.UnityTools;

    public class CampaignSceneController : MonoBehaviour {

        public void ExitToMain() {
            ChangeScene( GlobalValues.EntrySceneName );
	    }
    }

}
