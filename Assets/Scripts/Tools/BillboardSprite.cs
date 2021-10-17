using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Object will turn to face the camera.  Used to create flat, hoving sprites.  Icons over 3D objects.
/// </summary>
public class BillboardSprite : MonoBehaviour {

    internal Camera mainCam;
	internal MainCameraHandler handler;
    internal Vector3 up = Vector3.forward;	// Z is up

	private void Start() {
		mainCam = Camera.main;
		handler = mainCam.GetComponent<MainCameraHandler>();
	}
	void Update() {

		float angle = handler.GetCameraAngle();
		transform.localEulerAngles = new Vector3( -angle, 90f, 90f );

		//// faces cammera
		//transform.LookAt(mainCam.transform.position, up);

		//  align with camera angle?
		//transform.rotation = Quaternion.Inverse( theCam.transform.rotation );

		// turn towards camera?
		//transform.rotation = Quaternion.LookRotation(-mainCam.transform.forward);
		//transform.rotation = Quaternion.LookRotation(-mainCam.transform.up);
		//transform.rotation = Quaternion.LookRotation(-mainCam.transform.right);
		
		// stay level, but face same direction as camera for x/y
		//Vector3 camAngle = mainCam.transform.localEulerAngles;
		//transform.localEulerAngles = new Vector3( 0, camAngle.y, camAngle.z );

    }

}
