using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EditUI { 

    using TMPro;

    /// <summary>
    /// Clicking on the title opens an input space for the name.
    /// </summary>
    public class EditTitleScript : MonoBehaviour, IPointerClickHandler {

        public TMP_InputField titleInput;
        
		// Start is called before the first frame update
		void Start() {
        
        }

		public void OnPointerClick( PointerEventData eventData ) {


			//throw new System.NotImplementedException();
		}


    }
}