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

        public Material pit,wall,hidden;
        public Color[] factionColor;

        internal GameObject tileParent,levelParent,tokenParent;
        internal GameObject floor;
        internal Sprite[] sprites;
        internal Material[] materials;

        //internal LevelMap currentMap;
    
        // map needs to redraw event
        internal static UnityEvent<LevelMap> mapRedrawEvent = new UnityEvent<LevelMap>();

        static public ArenaManagerScript instance;

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start() {

		    if (instance != null) throw new ApplicationException("Cannot instantiate ArenaHandlerScript twice.");
		    instance = this;

		    mapRedrawEvent.AddListener( MapRedrawFunction );

            currentMap = PrepareLevel();
            BuildMaterials();

            //BuildLevel( currentMap );
            mapRedrawEvent.Invoke( currentMap );
	    }

        /// <summary>
        /// Add self as Event Listener
        /// </summary>
	    public void OnEnable() {
            mapRedrawEvent.AddListener( MapRedrawFunction );
	    }

        /// <summary>
        /// Remove self as Event Listener
        /// </summary>
	    public void OnDisable() {
            mapRedrawEvent.RemoveListener( MapRedrawFunction );
	    }

    //=======================================================================================================================

        /// <summary>
        /// Build Tile templates from sprites.
        /// </summary>
        internal void BuildMaterials() {

            print("BuildMaterials");
                
            tileParent = new GameObject("Tiles");
            UseParent( gameObject, tileParent );

            sprites  = Resources.LoadAll<Sprite>("Unpaid/TileStone2");
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
        /// Proxy to Global values.
        /// </summary>
        public LevelMap currentMap {
            get => GlobalValues.currentMap;
            set { GlobalValues.currentMap = value; }
        }

        internal LevelMap PrepareLevel() {


            if (currentMap==null) {
    print("Creating New Map");
                currentMap = RealmFactory.SimpleTerrain( 15, 15 );
            }

            return currentMap;
	    }

        /// <summary>
        /// Delegate for global map redraw events.
        /// </summary>
        public void MapRedrawFunction(LevelMap level) {

            print("BAD FRED!  Come back here and reuse these things!");
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

                    //GameObject tile = Instantiate( tiles[tileId] );
                    GameObject tile =  GameObject.CreatePrimitive(PrimitiveType.Cube);
                    tile.name = "Cube("+ix+","+iy+")";
                    UseParent( levelParent, tile );
                    tile.GetComponent<MeshRenderer>().material = materials[tileId];

                    // add tile script with  reference information
				    TileScript info = tile.AddComponent<TileScript>();
				    info.SetRef( this, currentMap.Places[ix,iy], c.x-ix, c.y-iy, materials[tileId] );

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