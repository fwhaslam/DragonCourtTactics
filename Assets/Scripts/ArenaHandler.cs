//
//  Handler form Edit Arena
//

// as of Unity 2021.1.9f1 there is a bug where Input.mousePosition does not update
#define USE_WORKAROUND_FOR_MOUSE_POSITION

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared;

/// <summary>
/// NOTE: this is currently disabled, not used.
/// This USED to have control a hovering cursor.  That cursoor functions moved to EditToolsScript.
/// </summary>
public class ArenaHandler : MonoBehaviour {

    public GameObject cursor;
    public float cursorHover;

    internal readonly int HOVER = 1;

#if USE_WORKAROUND_FOR_MOUSE_POSITION
    internal Vector3 lastMouse = Vector3.zero;
#endif

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //HighlightTileWithCursor();
    }

#if USE_WORKAROUND_FOR_MOUSE_POSITION
	public void OnGUI() {
		
        if (Event.current.type==EventType.Repaint) { 
            var mouse = Event.current.mousePosition;   
            lastMouse = new Vector3( mouse.x, Screen.height - mouse.y,  Camera.main.nearClipPlane );
        }
	}
#endif

	internal void HighlightTileWithCursor() {

#if USE_WORKAROUND_FOR_MOUSE_POSITION
		Vector3 mouse = lastMouse;
#else
		Vector3 mouse = Input.mousePosition;
#endif

        Ray ray = Camera.main.ScreenPointToRay(mouse);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)) {
            var where = hit.transform.position;
            var height = hit.transform.lossyScale.z;

            cursor.SetActive(true);
            cursor.transform.position = new Vector3( where.x, where.y, height + cursorHover );    //where.z - HOVER );

            //Vector3 clickPosition = hit.point;
            //Debug.Log( "***** Hit="+hit.transform.name );
        }
        else {
            cursor.SetActive(false);
		}

 	}
}
