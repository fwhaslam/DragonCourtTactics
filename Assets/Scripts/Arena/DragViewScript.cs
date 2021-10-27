using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Arena {

	/// <summary>
    /// Catch clicks around edge of world.
    /// </summary>
	public class DragViewScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

       
        // when a tile is dragged, move the camera :: function( V3 start, V3 delta )
        static internal readonly UnityEvent<Vector3,Vector3> tileDragEvent = new UnityEvent<Vector3,Vector3>();

        internal Vector3 startDrag = Vector3.zero;
        internal Vector3 lastDrag = Vector3.zero;

	    public void OnBeginDrag(PointerEventData eventData) {
		    //print("Begin Drag = "+eventData );
            startDrag = eventData.position;
            lastDrag = eventData.position;
	    }

	    public void OnDrag(PointerEventData eventData) {

		    //print("On Drag = "+eventData );
            Vector3 next = eventData.position;
            Vector3 delta = next - lastDrag;
            lastDrag = next;

            // fire off event
           tileDragEvent.Invoke( startDrag, delta );
	    }

	    public void OnEndDrag(PointerEventData eventData) {
		    //print("End Drag = "+eventData );
	    }

	}

}