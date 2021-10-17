//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Tools {

    using System.Collections;
    using System.Collections.Generic;

	using TMPro;

	using UnityEngine;

    public class ShowAppVersion : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            this.gameObject.GetComponent<TMP_Text>().text = "Dragon Court Tactics\n"+Application.version;
        }

    }

}