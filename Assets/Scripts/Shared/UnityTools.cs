
using UnityEngine;

namespace Shared {

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
	}
}
