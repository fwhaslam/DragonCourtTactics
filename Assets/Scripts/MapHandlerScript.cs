
using Realm;

using Shared;

using System;

using UnityEngine;

/// <summary>
/// Construct tiles, layout map.
/// </summary>
public class MapHandlerScript : MonoBehaviour {

    internal GameObject tileParent,levelParent,playParent;
    internal Sprite[] sprites;
    internal GameObject[] tiles;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start() {

        playParent = GameObject.Find("Play");

        BuildTiles();
        BuildLevel();
		BuildMenus();

	}

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update() {

	}

//=======================================================================================================================

    /// <summary>
    /// Build Tile templates from sprites.
    /// </summary>
    internal void BuildTiles() {

        print("BuildTiles");
                
        tileParent = new GameObject("Tiles");
        tileParent.transform.parent = playParent.transform;

        sprites  = Resources.LoadAll<Sprite>("TileStone2");
        tiles = new GameObject[sprites.Length];

        for (int ix=0;ix<sprites.Length;ix++) { 
            Sprite sprite = sprites[ix];

            GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);

            tile.transform.position = new Vector3( GlobalValues.TILE_X_LOC, GlobalValues.TILE_Y_LOC, 0);
            tile.transform.parent = tileParent.transform;
            tile.GetComponent<MeshRenderer>().material = MaterialFromSprite( sprite );
            tiles[ix] = tile;
        }

        print("BuildTiles - done");
        Console.Out.Flush();
	}
    
    /// <summary>
    ///     Material built from sprite texture.
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns>Material</returns>
    Material MaterialFromSprite( Sprite sprite ) {

        Material material = new Material(Shader.Find("Standard"));
        material.mainTexture = TextureFromSprite( sprite );

		return material;
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns></returns>
    Texture2D TextureFromSprite(Sprite sprite) {

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
    
//=======================================================================================================================
   
    /// <summary>
    /// Use Map from Realm to build a local model using Tile Templates ( eg. cubes )
    /// </summary>
    internal void BuildLevel() {

print("Build Level");

        LevelMap level = GlobalValues.currentMap;
        if (level==null) {

print("Build Level");

            level = RealmFactory.SimpleTerrain( 15, 15 );
            GlobalValues.currentMap = level;
        }
print("DRAW LEVEL="+level);

        levelParent = new GameObject("Level");
        levelParent.transform.parent = playParent.transform;

        // center
        Vector2 c = new Vector2( level.Wide/2f, level.Tall/2f );

        for (int ix=0;ix<level.Wide;ix++) {
            for (int iy=0;iy<level.Tall;iy++) {

                int tileId = (7*ix+5*iy) % tiles.Length;

                GameObject tile = Instantiate( tiles[tileId] );
                tile.name = "Cube("+ix+","+iy+")";
                tile.transform.parent = levelParent.transform;


                // add tile script with  reference information
				TileScript info = tile.AddComponent<TileScript>();
				info.SetRef( ix, iy, c.x-ix, c.y-iy );

                // cleanup
                info.RedrawTile();

			}
		}


	}

//=======================================================================================================================

    /// <summary>
    /// Create images from models.  Use images in a flat panel menu.
    /// </summary>
    internal void BuildMenus() {

        BuildHeightMenu();
        //Sprite pic = TakePicture( "High Poly Chess Pack/Prefabs/Amber Pawn" );
        //Image box = GameObject.Find("ShowHere").GetComponent<Image>();
        //box.sprite = pic;
    }

    internal void BuildHeightMenu() {

	}

//=======================================================================================================================

    Sprite TakePicture( string prefabPath ) {

        //GameObject target = Resources.Load<GameObject>( prefabPath );
        GameObject target = GameObject.Find("Amber Pawn");
print("Target="+target);
//print("Bounds="+target.GetComponent<Renderer>().bounds);
        //Instantiate(target, new Vector3(0, 5, 0), Quaternion.identity);

		Camera Cam = GameObject.Find("Second Camera").GetComponent<Camera>();
		//Camera Cam = GameObject.FindWithTag("SecondCamera").GetComponent<Camera>();
print("CAM="+Cam);

        // point camera at pawn
        var point = target.GetComponent<Renderer>().bounds.center;
		Cam.transform.position = new Vector3(point.x, point.y, point.z - 1.5f);
		//Cam.transform.position = new Vector3( start.x, start.y, start.z );
print("Texture="+Cam.targetTexture);

        // take picture
        int wide=256, tall=256;
        Rect rect = new Rect(0,0,wide,tall);
        Cam.targetTexture = new RenderTexture(wide,tall,16);
        Cam.Render();

        // copy from active texture to image
        Texture2D Image = new Texture2D(wide,tall);
        RenderTexture.active = Cam.targetTexture;
        Image.ReadPixels(rect, 0, 0);
        Image.Apply();
 
        // return as sprite
        return Sprite.Create( Image, rect, new Vector2(0,0) );
    }

}
