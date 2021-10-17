//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Arena { 

    using Realm;
    using Realm.Enums;

    using Shared;
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    using static Shared.UnityTools;

    /// <summary>
    /// Construct tiles, layout map.
    /// </summary>
    public class ArenaManagerScript : MonoBehaviour {

        // inspector
        public Material pit,wall,hidden;
        public Color[] factionColor;
        public Color flagShade;
        public Vector3 flagScale;

        // private values
        internal GameObject tileParent,levelParent,tokenParent;
        internal GameObject floor;
        internal Sprite[] sprites,normals;
        internal Material[] materials;

        //internal LevelMap currentMap;
    
        // map needs to redraw
        internal static UnityEvent<LevelMap> mapRedrawEvent = new UnityEvent<LevelMap>();

        // load new map ( or null for reset )
        internal static UnityEvent<LevelMap> mapLoadEvent = new UnityEvent<LevelMap>();

        static public ArenaManagerScript instance;

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start() {

		    if (instance != null) throw new ApplicationException("Cannot instantiate ArenaHandlerScript twice.");
		    instance = this;

		    //mapRedrawEvent.AddListener( MapRedrawFunction );
            //currentMap = PrepareLevel();

            BuildMaterials();

            mapLoadEvent.Invoke( null );      // load default map

            //BuildLevel( currentMap );
            //mapRedrawEvent.Invoke( currentMap );
	    }

        /// <summary>
        /// Add self as Event Listener
        /// </summary>
	    public void OnEnable() {
            mapRedrawEvent.AddListener( MapRedrawFunction );
            mapLoadEvent.AddListener( LoadMapFunction );
	    }

        /// <summary>
        /// Remove self as Event Listener
        /// </summary>
	    public void OnDisable() {
            mapRedrawEvent.RemoveListener( MapRedrawFunction );
           mapLoadEvent.RemoveListener( LoadMapFunction );
	    }

//=======================================================================================================================

        /// <summary>
        /// Build Tile templates from sprites.
        /// </summary>
        internal void BuildMaterials() {

            print("BuildMaterials");
                
            tileParent = new GameObject("Tiles");
            UseParent( gameObject, tileParent );

            //sprites  = Resources.LoadAll<Sprite>("Unpaid/TileStone2");
            //sprites  = Resources.LoadAll<Sprite>("Usable/stone_tiles");
            sprites  = Resources.LoadAll<Sprite>("Usable/pixel_tiles");
            normals  = Resources.LoadAll<Sprite>("Usable/pixel_tiles_n");

            materials = new Material[ sprites.Length ];
            //tiles = new GameObject[sprites.Length];

            for (int ix=0;ix<sprites.Length;ix++) { 

                Sprite sprite = sprites[ix];
                Sprite normal = normals[ix];
                materials[ix] = MaterialFromSpriteAndNormal( sprite, normal );

            }

	    }
    
        /// <summary>
        ///     Material built from sprite texture.
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns>Material</returns>
        Material MaterialFromSpriteAndNormal( Sprite sprite, Sprite normal ) {

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
        /// Proxy to Global values.
        /// </summary>
        public LevelMap currentMap {
            get => GlobalValues.currentMap;
            set { GlobalValues.currentMap = value; }
        }

 //=======================================================================================================================

        /// <summary>
        /// Delegate for loading a new map.
        /// </summary>
        public void LoadMapFunction(LevelMap newMap ) {

            // reset ?
            if (newMap==null) newMap = RealmFactory.SimpleTerrain( 12, 12 );

            EditToolsMenuScript.tileSelectEvent.Invoke( null );

            currentMap = newMap;
            mapRedrawEvent.Invoke( newMap );
		}

        /// <summary>
        /// Delegate for global map redraw events.
        /// </summary>
        public void MapRedrawFunction(LevelMap level) {

            if (levelParent!=null) { 
                Destroy( levelParent );
                Destroy( tokenParent );
                Destroy( tileParent );
                Destroy( floor );
            }

            // and away we go!
            BuildLevel( level );

	    }    
    
    
        /// <summary>
        /// Use Map from Realm to build a local model using Tile Templates ( eg. cubes )
        /// </summary>
        internal void BuildLevel( LevelMap level ) {

    print("DRAW LEVEL="+level);
                
            BuildFloor( level.Wide, level.Tall );

            levelParent = new GameObject("Level");
            UseParent( gameObject, levelParent );
            tokenParent = new GameObject("Tokens");
            UseParent( gameObject, tokenParent );

            RedrawMap( level );
        }

        internal void RedrawMap( LevelMap level ) {

            // center
            Vector2 c = new Vector2( level.Wide/2f, level.Tall/2f );

            for (int ix=0;ix<level.Wide;ix++) {
                for (int iy=0;iy<level.Tall;iy++) {

                    // material selection
                    int tileId = (7*ix+5*iy) % materials.Length;
                   //int tileId = (7*ix+5*iy) % tiles.Length;
                    var material = materials[tileId];

                    // tile info
                    var place = level.Places[ix,iy];
 
                    //GameObject tile = Instantiate( tiles[tileId] );
                    GameObject tile =  GameObject.CreatePrimitive(PrimitiveType.Cube);
                    tile.name = "Cube("+ix+","+iy+")";
                    UseParent( levelParent, tile );
                    tile.GetComponent<MeshRenderer>().material = materials[tileId];

                    // add tile script with  reference information
				    TileScript info = tile.AddComponent<TileScript>();
				    info.SetRef( this, place, c.x-ix, c.y-iy, material );

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

            floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.name = "Floor";
		    UseParent( gameObject, floor );

            floor.transform.localScale = new Vector3( w-SLIGHTLY_SMALLER, t-SLIGHTLY_SMALLER, 0.1f );
            floor.transform.localPosition = new Vector3( 0, 0, 0.05f );
            floor.GetComponent<MeshRenderer>().material = pit;

	    }

    //=======================================================================================================================

        static readonly int DIR_OFFSET = 4;

        internal DirEnum FindCameraDirection() {

            float angle = Camera.main.transform.eulerAngles.z;
    print("CAMERA="+angle);

            int pick = (int)( MathTools.Modulus( angle, 360 ) / 90 );
            DirEnum dir = (DirEnum) (( 2 * pick + DIR_OFFSET ) % 8);
		    print("DIR=" + dir.ToString());

		    return dir;
	    }

        public void AddRowToMap() {
            print(">>>>>>>>>>>>>>>>>>>>>> Add Row To Map");

            DirEnum dir = FindCameraDirection();

            currentMap.AddRow( dir );
            mapRedrawEvent.Invoke( currentMap );
	    }

        public void CutRowFromMap() {
            print(">>>>>>>>>>>>>>>>>>>>>> Cut Row From Map");

            DirEnum dir = FindCameraDirection();

            currentMap.DropRow( dir );
            mapRedrawEvent.Invoke( currentMap );
        }

    }

}