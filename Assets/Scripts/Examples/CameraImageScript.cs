//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

// as of Unity 2021.1.9f1 there is a bug where Input.mousePosition does not Update without a click
#define USE_WORKAROUND_FOR_MOUSE_POSITION

namespace Examples {

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Shared;

	/// <summary>
	/// NOTE: this is currently disabled, not used.
	/// This provides an example of a hovering cursor.  
	/// The cursor functions have moved to EditToolsScript.
	/// </summary>
	public class CameraImageScript : MonoBehaviour {


		/// <summary>
		/// Used to transfer an image from a camera to an on-screen location.
		/// </summary>
		/// <param name="prefabPath"></param>
		/// <returns></returns>
		Sprite TakePicture(string prefabPath) {

			//GameObject target = Resources.Load<GameObject>( prefabPath );
			GameObject target = GameObject.Find("Amber Pawn");
			print("Target=" + target);
			//print("Bounds="+target.GetComponent<Renderer>().bounds);
			//Instantiate(target, new Vector3(0, 5, 0), Quaternion.identity);

			Camera Cam = GameObject.Find("Second Camera").GetComponent<Camera>();
			//Camera Cam = GameObject.FindWithTag("SecondCamera").GetComponent<Camera>();
			print("CAM=" + Cam);

			// point camera at pawn
			var point = target.GetComponent<Renderer>().bounds.center;
			Cam.transform.position = new Vector3(point.x, point.y, point.z - 1.5f);
			//Cam.transform.position = new Vector3( start.x, start.y, start.z );
			print("Texture=" + Cam.targetTexture);

			// take picture
			int wide = 256, tall = 256;
			Rect rect = new Rect(0, 0, wide, tall);
			Cam.targetTexture = new RenderTexture(wide, tall, 16);
			Cam.Render();

			// copy from active texture to image
			Texture2D Image = new Texture2D(wide, tall);
			RenderTexture.active = Cam.targetTexture;
			Image.ReadPixels(rect, 0, 0);
			Image.Apply();

			// return as sprite
			return Sprite.Create(Image, rect, new Vector2(0, 0));
		}

	}
}