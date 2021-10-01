//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Examples {
    
    using UnityEngine;

	class MouseExamples {

		
    /// <summary>
    /// Which object in screen got clicked?
    /// </summary>
    /// <returns></returns>
	internal GameObject FindClickTarget() {

        Vector3 mouse = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        // did we hit anything ?
        if (!Physics.Raycast(ray, out hit)) {
            //cursor.SetActive(false);
            return null;
        }
        
        return hit.transform.gameObject;
    }
	}
}
