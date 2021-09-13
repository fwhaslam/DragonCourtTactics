
using Shared;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Camera handling.  Includes zoom, turns, dragging, and tile select.
/// NOTE that the light is a child of the camera, so stays at a constant angle to the camera position.
/// </summary>
public class MainCameraHandler : MonoBehaviour {

    public float rotateSpeed,rotateLerpSpeed,startAngle;
    public float minZoom,maxZoom,startZoom,wheelZoomSpeed,pinchZoomSpeed;

	public Slider zoomSlider;

    // set by turn controls
    static int turning;
    internal Vector3 focus = new Vector3(0,0,0);
    internal float currentTurning,angle,zoom;
    
    internal bool isDragging;
    internal Vector3 mouseDragFrom;

    internal readonly Vector3 cameraUp = Vector3.forward;
 
//======================================================================================================================

    /// <summary>
    /// Setup camera values.  Add listener to slider.
    /// </summary>
    void Start() {

        currentTurning = turning = 0;
        angle = startAngle;
        zoom = startZoom;

        // starting camera location
        FixCameraLocation( focus, angle, zoom );
        //Vector2 where = zoom * MathTools.DegreesToPosition( angle );
        //transform.position = new Vector3( where.x, where.y, zoom );
        //transform.LookAt( focus, cameraUp );

		// listen to the scale slider
		zoomSlider.onValueChanged.AddListener(delegate { ScaleSliderChange(); });
	}

	// Update is called once per frame
	void Update() {

        // for hover, tap and drag
        MouseDrag();

        // mouse wheel and screen pinch
        CheckZoom();

		// change angle based on 'turn' controls
		currentTurning = Mathf.Lerp( currentTurning, turning, Time.deltaTime * rotateLerpSpeed );
        angle += currentTurning * rotateSpeed * Time.deltaTime;
        while (angle<0) angle += 360;
        while (angle>=360) angle -= 360;

        // move camera
        FixCameraLocation( focus, angle, zoom );

	}
    
 //======================================================================================================================

    /// <summary>
    /// Once we know our angle and distance and view target, place the camera.
    /// </summary>
    /// <param name="focus"></param>
    /// <param name="angle"></param>
    /// <param name="zoom"></param>
    internal void FixCameraLocation( Vector3 focus, float angle, float zoom) {
        Vector2 where = zoom * MathTools.DegreesToPosition(angle);
		transform.position = new Vector3(focus.x + where.x, focus.y + where.y, zoom);
		transform.LookAt(focus, cameraUp );
	}

    internal void MouseDrag() {

        if (Input.GetMouseButtonDown(0)) {

            // discard clicks on UI objects.
            if (!EventSystem.current.IsPointerOverGameObject()) {
                mouseDragFrom = Input.mousePosition;
                isDragging = true;
            }

		}
        if (Input.GetMouseButtonUp(0)) {
            isDragging = false;
		}

        if (isDragging && Input.GetMouseButton(0)) { 
            
            Vector3 newMouse = Input.mousePosition;
            Vector3 delta = newMouse - mouseDragFrom;
            mouseDragFrom = newMouse;

            Vector2 forward = delta.y * MathTools.DegreesToPosition( angle );
            Vector2 sideway = delta.x * MathTools.DegreesToPosition( angle+90 );

            // do not move past edge of map
            focus.x += ( forward.x + sideway.x ) * zoom / 100f / startZoom;
            float xlimit = GlobalValues.currentMap.Wide / 2f;
            if (focus.x<-xlimit) focus.x = -xlimit;
            else if (focus.x>xlimit) focus.x = xlimit;

            focus.y += ( forward.y + sideway.y ) * zoom / 100f / startZoom;
            float ylimit = GlobalValues.currentMap.Tall / 2f;
            if (focus.y<-ylimit) focus.y = -ylimit;
            if (focus.y>ylimit) focus.y = ylimit;

		}
	}

    internal void CheckZoom() {

        // mouse wheel
        if (!Input.touchSupported) {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Zoom(scroll, wheelZoomSpeed);
		}

        // screen pinch
        else  if (Input.touchCount == 2) {
			// get current touch positions
            Touch tZero = Input.GetTouch(0);
            Touch tOne = Input.GetTouch(1);
			// get touch position from the previous frame
			Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
			Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;
 
			float oldTouchDistance = Vector2.Distance (tZeroPrevious, tOnePrevious);
			float currentTouchDistance = Vector2.Distance (tZero.position, tOne.position);
 
			// get offset value
			float deltaDistance = oldTouchDistance - currentTouchDistance;
			Zoom (deltaDistance, pinchZoomSpeed);
        }

    }
 
    internal void Zoom(float deltaMagnitudeDiff, float speed) {
//print("ZOOM = "+zoom+""deltaMagnitudeDiff+" * "+speed);
        zoom =  Mathf.Clamp(zoom + deltaMagnitudeDiff * speed, minZoom,  maxZoom);

        // fix slider value
        zoomSlider.value = ( zoom - minZoom ) / ( maxZoom - minZoom );

    }

	/// <summary>
	/// Player is using 'turn' controls.
	/// </summary>
	/// <param name="value"></param>
	static public void AddTurning(int value) {
        turning += value;
	}

	/// <summary>
	/// Assume slider has continuous value from zero to one.
    /// This is called when player changes slider.
	/// </summary>
	public void ScaleSliderChange() {
		//zoom = startZoom + (mainSlider.value - 0.5f) * 10f;
        zoom =  Mathf.Clamp( minZoom + (maxZoom-minZoom) * zoomSlider.value, minZoom,  maxZoom);
	}

//======================================================================================================================

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
