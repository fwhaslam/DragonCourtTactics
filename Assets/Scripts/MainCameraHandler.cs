//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

using Arena;

using Shared;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Camera handling.  Includes zoom, turns, dragging, and tile select.
/// NOTE that the light is a child of the camera, so stays at a constant angle to the camera position.
/// </summary>
public class MainCameraHandler : MonoBehaviour {

    public float rotateSpeed,rotateLerpSpeed,startAngle;
    public float minZoom,maxZoom,startZoom,wheelZoomSpeed,pinchZoomSpeed,keyDragSpeed;

	public Slider zoomSlider;

    // set by turn controls
    internal Vector3 focus = new Vector3(0,0,0);
    internal float currentTurning,angle,zoom;
    
    // drag constants
    internal readonly Vector3 cameraUp = Vector3.forward;

 
//======================================================================================================================

    /// <summary>
    /// Setup camera values.  Add listener to slider.
    /// </summary>
    void Start() {

        currentTurning = 0;
        angle = startAngle;
        zoom = startZoom;

        // starting camera location
        FixCameraLocation( focus, angle, zoom );

		// listen to the scale slider
		zoomSlider.onValueChanged.AddListener(delegate { ScaleSliderChange(); });
	}

	// Update is called once per frame
	void Update() {
//print("CAMERA UPDATE <<<<====");

        // keyboard moves camera
        HandleCameraKeyboard();

        // mouse wheel and screen pinch
        CheckZoom();

        // move camera
        FixCameraLocation( focus, angle, zoom );


	}

//======================================================================================================================

    /// <summary>
    /// Add self as Event Listener
    /// </summary>
	public void OnEnable() {
        TileScript.tileDragEvent.AddListener( TileDragFunction );
	}

    /// <summary>
    /// Remove self as Event Listener
    /// </summary>
	public void OnDisable() {
        TileScript.tileDragEvent.RemoveListener( TileDragFunction );
	}

	/// <summary>
	/// Implement camera drag, with location and facing limits.
	/// </summary>
	/// <param name="start"></param>
	/// <param name="delta"></param>
	public void TileDragFunction(Vector3 start, Vector3 delta) {
//print("Tile Drag = "+delta);
		if (IsRotate(start)) {
            float turning = delta.x;
			RotateCamera( turning );
		}
		else {
			DragCamera(delta);
		}
	}

    /// <summary>
    /// Keystrokes that move the camera around the world.
    /// </summary>
    internal void HandleCameraKeyboard() {
//print("Handle Camera Keyboard");

        if (Input.GetKey(KeyCode.S)) MoveCameraFocus( 0, +1f );
        if (Input.GetKey(KeyCode.W)) MoveCameraFocus( 0, -1f );

        if (Input.GetKey(KeyCode.A)) MoveCameraFocus( +1f, 0f );
        if (Input.GetKey(KeyCode.D)) MoveCameraFocus( -1f, 0f );

        if (Input.GetKey(KeyCode.Q)) RotateCamera( -1f );
        if (Input.GetKey(KeyCode.E)) RotateCamera( +1f );

	}
    
    /// <summary>
    /// Transform keyboard into camera move.
    /// </summary>
    /// <param name="horz"></param>
    /// <param name="vert"></param>
    internal void MoveCameraFocus( float horz, float vert ) {
        DragCamera( new Vector3( horz*keyDragSpeed, vert*keyDragSpeed, 0f ) );
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

    /// <summary>
    /// If the point is inside an ellipse in the center of the screen, then drag else rotate
    /// </summary>
    /// <param name="start"></param>
    /// <returns>true for use mouse drag to rotate</returns>
    internal bool IsRotate( Vector3 start ) {


        //float dx = 2f * ( start.x - Screen.width/2f ) / Screen.width;
        //if (dx<-0.8f || dx>0.8f) return true;
        float dy = 2f * ( start.y - Screen.height/2f ) / Screen.height;
//print("DY="+dy);
        if (dy<-0.5f) return true;
        return false;

        //float dx = 2f * ( start.x - Screen.width/2f ) / Screen.width;
        //float dy = 2f * ( start.y - Screen.height/2f ) / Screen.height;
        //float dist2 = dx*dx + dy*dy;
        //return ( dist2 > 0.50f );
	}

    /// <summary>
    /// Rotate camera some amount based on mouse drag or keyboard.
    /// </summary>
    /// <param name="turning"></param>
    internal void RotateCamera( float turning ) {

		currentTurning = Mathf.Lerp( currentTurning, turning, Time.deltaTime * rotateLerpSpeed );
        angle += currentTurning * rotateSpeed * Time.deltaTime;
        angle = MathTools.Modulus( angle, 360f );

	}

    /// <summary>
    /// Used to rotate the flag layer.
    /// </summary>
    /// <returns></returns>
    public float GetCameraAngle() {
        return angle;
	}


//======================================================================================================================

    /// <summary>
    /// Used with 'drag' logic.
    /// </summary>
    /// <param name="delta"></param>
    internal void DragCamera( Vector3 delta ) {

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
	/// Assume slider has continuous value from zero to one.
    /// This is called when player changes slider.
	/// </summary>
	public void ScaleSliderChange() {
		//zoom = startZoom + (mainSlider.value - 0.5f) * 10f;
        zoom =  Mathf.Clamp( minZoom + (maxZoom-minZoom) * zoomSlider.value, minZoom,  maxZoom);
	}

}
