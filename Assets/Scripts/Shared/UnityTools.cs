//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Shared {
	
	using UnityEngine;
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

		static public void ChangeScene( string sceneName ) {
			SceneManager.LoadScene( sceneName );
		}

		
		/// <summary>
		/// Debug Utility for examining component tree.
		/// </summary>
		/// <param name="obj"></param>
		static public void inspectComponents( GameObject obj ) {

			print("INSPECT >>>>>>> "+obj);
			 Component[] comps = obj.GetComponents<Component>(); 

			print("COUNT = "+comps.Length );
			foreach ( Component cmp in comps ) {
				print("COMPONENT="+cmp);
				print("TYPE="+cmp.GetType());
			}
		}
	}
}
