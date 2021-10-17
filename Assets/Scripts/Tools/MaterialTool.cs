using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Tools {

	public class MaterialTool {

		
		/// <summary>
		/// Default shader when creating materials.
		/// </summary>
		static public Shader GetDefaultShader() {
			return Shader.Find("Universal Render Pipeline/Lit");
		}   

        /// <summary>
        ///     Material built from sprite texture.
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns>Material</returns>
        static public Material MaterialFromSprite( Sprite sprite ) {

            Material material = new Material(GetDefaultShader());
            material.mainTexture = TextureFromSprite( sprite );                 // sets "_MainTex"
            //material.SetTexture( "_BumpMap", TextureFromSprite( normal ) );     // sets "_BumpMap"

		    return material;
	    }

		   
        /// <summary>
        ///     Material built from sprite texture.
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns>Material</returns>
        static public Material MaterialFromSpriteAndNormal( Sprite sprite, Sprite normal ) {

            Material material = new Material(GetDefaultShader());
            material.mainTexture = TextureFromSprite( sprite );                 // sets "_MainTex"
            material.SetTexture( "_BumpMap", TextureFromSprite( normal ) );     // sets "_BumpMap"

		    return material;
	    }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        static public Texture2D TextureFromSprite(Sprite sprite) {

            if (sprite.rect.width == sprite.texture.width) return sprite.texture;

            // else
            Texture2D newText = new Texture2D((int)sprite.rect.width,(int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels(
                (int)sprite.textureRect.x, (int)sprite.textureRect.y,
                (int)sprite.textureRect.width, (int)sprite.textureRect.height );

            newText.SetPixels(newColors);
            newText.Apply();

            return newText;
        }
	}
}
