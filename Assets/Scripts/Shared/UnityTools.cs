//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Shared {
	using System;
	using System.IO;

	using UnityEngine;
	using UnityEngine.EventSystems;
	using UnityEngine.SceneManagement;
	using static UnityEngine.MonoBehaviour;

	public class UnityTools {

		/// <summary>
		/// Common action, assign gameobject to a parent
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="child"></param>
		static public void UseParent( GameObject parent, GameObject child ) {
			child.transform.parent = parent.transform;
		}

		/// <summary>
		/// Default shader when creating materials.
		/// </summary>
		static public Shader GetDefaultShader() {
			return Shader.Find("Universal Render Pipeline/Lit");
		}

		/// <summary>
		/// Switching around scenes.
		/// </summary>
		/// <param name="sceneName"></param>
		static public void ChangeScene( string sceneName ) {
			SceneManager.LoadScene( sceneName );
		}

		/// <summary>
		/// Currently selected game object.
		/// </summary>
		/// <param name="who"></param>
		static public void SetSelected( GameObject who ) { 
			EventSystem.current.SetSelectedGameObject(who);
		}

		static public string FixFilePath( string path ) {
			return path.Replace( '/', Path.DirectorySeparatorChar );
		}

		/// <summary>
		/// Debug Utility for examining component tree.
		/// </summary>
		/// <param name="obj"></param>
		static public void _InspectComponents( GameObject obj ) {

			print("INSPECT >>>>>>> "+obj);
			 Component[] comps = obj.GetComponents<Component>(); 

			print("COUNT = "+comps.Length );
			foreach ( Component cmp in comps ) {
				print("COMPONENT="+cmp);
				print("TYPE="+cmp.GetType());
			}
		}
		
//======================================================================================================================

		/// <summary>
		/// Is a Screen Click touching some component?
		/// </summary>
		/// <param name="what"></param>
		/// <param name="where"></param>
		/// <param name="view"></param>
		/// <returns></returns>
		static public bool IsScreenClickInObject( GameObject what, Vector2 where, Camera view ) {
			var rect = what.GetComponent<RectTransform>();
			return RectTransformUtility.RectangleContainsScreenPoint( rect, where, view );
		}

		static public Vector2 ScreenPointToLocal( GameObject what, Vector2 where, Camera view ) {
			var rect = what.GetComponent<RectTransform>();
			Vector2 localPt;
			bool result = RectTransformUtility.ScreenPointToLocalPointInRectangle( rect, where, view, out localPt );
print("SCREEN PT TO LOCAL BOOL = "+result);
			return localPt;
		}

//======================================================================================================================

		/// <summary>
		/// In development, quit out for certain failures.
		/// This forces the developer to pay attention.
		/// If calls here make it into release, then do NOT die.
		/// </summary>
		/// <param name="ex"></param>
		static public void ComplainAndDie( Exception ex ) {
			print( ">>>>>>>>>>>>>>>>>>> Fatal Exception !!!\n"+ex );
			#if DEVELOPMENT_BUILD // this is a UNITY define
			CloseGame();
			#endif
		}


		static public void CloseGame() {

			print("Trying to Close Game");

			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#elif UNITY_WEBPLAYER
			Application.OpenURL(webplayerQuitURL);
			#else
			Application.Quit();
			#endif
		}
	}
}
