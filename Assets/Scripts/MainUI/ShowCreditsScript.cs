//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace MainUI {
	using Shared;

	using System.Collections;
    using System.Collections.Generic;
	using System.Text;

	using TMPro;

    using UnityEngine;

    /// <summary>
    /// Load attribution, create credits.
    /// </summary>
    public class ShowCreditsScript : MonoBehaviour {

        AttributionList attribution;

		private void Awake() {
			attribution = AttributionList.ReadAttributeYml();
		}


		// Start is called before the first frame update
		void Start() {

            var credits = new StringBuilder();
            credits.Append("Credits:\n");
            
            credits.Append( AsLine( attribution.Attribution[0] ) );

            gameObject.GetComponent<TMP_Text>().text = credits.ToString();
        }


        internal string AsLine( Attribution item ) {

            var line = new StringBuilder();

            line.Append( "\t\""+item.Title+"\"" );
            line.Append( " by "+item.Creator );
            line.Append( " at "+item.Link );
            line.Append("\n\t\t"+item.License );
            line.Append(" from "+item.Source );

            return line.ToString();
		}
    }

}
