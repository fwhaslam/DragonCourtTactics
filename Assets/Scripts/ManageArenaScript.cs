
using Realm;
using Shared;
using System;
using UnityEngine;
using UnityEngine.Events;

using static Shared.UnityTools;

/// <summary>
/// Construct tiles, layout map.
/// </summary>
public class ManageArenaScript : MonoBehaviour {

    static public ManageArenaScript instance;

    public Material pit,wall,hidden;

    internal GameObject tileParent,levelParent;
    internal GameObject floor;
    internal Sprite[] sprites;
    internal Material[] materials;


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start() {

        if (instance!=null) throw new ApplicationException("Cannot instantiate ArenaHandlerScript twice.");
        instance = this;

        BuildMaterials();
        BuildLevel();

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
    internal void BuildMaterials() {

        print("BuildMaterials");
                
        tileParent = new GameObject("Tiles");
        UseParent( gameObject, tileParent );

        sprites  = Resources.LoadAll<Sprite>("TileStone2");
        materials = new Material[ sprites.Length ];
        //tiles = new GameObject[sprites.Length];

        for (int ix=0;ix<sprites.Length;ix++) { 

            Sprite sprite = sprites[ix];
            materials[ix] = MaterialFromSprite( sprite );

        }

	}
    
    /// <summary>
    ///     Material built from sprite texture.
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns>Material</returns>
    Material MaterialFromSprite( Sprite sprite ) {

        Material material = new Material(GetDefaultShader());
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
                
        BuildFloor( level.Wide, level.Tall );


        levelParent = new GameObject("Level");
        UseParent( gameObject, levelParent );

        // center
        Vector2 c = new Vector2( level.Wide/2f, level.Tall/2f );

        for (int ix=0;ix<level.Wide;ix++) {
            for (int iy=0;iy<level.Tall;iy++) {

                // material selection
                int tileId = (7*ix+5*iy) % materials.Length;
               //int tileId = (7*ix+5*iy) % tiles.Length;

                //GameObject tile = Instantiate( tiles[tileId] );
                GameObject tile =  GameObject.CreatePrimitive(PrimitiveType.Cube);
                tile.name = "Cube("+ix+","+iy+")";
                UseParent( levelParent, tile );
                tile.GetComponent<MeshRenderer>().material = materials[tileId];

                // add tile script with  reference information
				TileScript info = tile.AddComponent<TileScript>();
				info.SetRef( ix, iy, c.x-ix, c.y-iy, materials[tileId] );

                // cleanup
                info.RedrawTile();

			}
		}
	}

    static readonly float SLIGHTLY_SMALLER = 0.001f;

    /// <summary>
    /// The floor is all the 'pit' tiles as a single object.  This makes lava + water look nicer.
    /// </summary>
    /// <param name="w"></param>
    /// <param name="t"></param>
    internal void BuildFloor( int w, int t) {

        if (floor!=null) return;

        floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Floor";
		UseParent( gameObject, floor );

        floor.transform.localScale = new Vector3( w-SLIGHTLY_SMALLER, t-SLIGHTLY_SMALLER, 0.1f );
        floor.transform.localPosition = new Vector3( 0, 0, 0.05f );
        floor.GetComponent<MeshRenderer>().material = pit;

	}

//=======================================================================================================================

    public void AddRowToMap() {
        print(">>>>>>>>>>>>>>>>>>>>>> Add Row To Map");
	}

    public void CutRowFromMap() {
        print(">>>>>>>>>>>>>>>>>>>>>> Cut Row From Map");
	}

}
