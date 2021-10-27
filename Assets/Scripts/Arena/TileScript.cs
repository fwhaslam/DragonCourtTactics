//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Arena { 

    using Realm;
    using Realm.Enums;
    using Realm.Puzzle;

    using UnityEngine;
	using UnityEngine.Events;
	using UnityEngine.EventSystems;

    using static Shared.GlobalValues;
    using static Shared.UnityTools;

    /// <summary>
    /// Mange appearance and actions on a single Tile.
    /// </summary>
    public class TileScript : MonoBehaviour, IPointerDownHandler {

        internal GameObject token = null;
        internal GameObject flag = null;
 
        // Start is called before the first frame update
        void Start() {
        }

//======================================================================================================================


	    public void OnPointerDown(PointerEventData eventData) {
    //print("CLICKED=="+transform.name);
		    EditToolsMenuScript.TileSelect( this );
	    }

        public void TakeCursor( GameObject cursor ) {

            cursor.SetActive(true);
            Vector3 where = transform.position;

            // approximate the 'top' of the object ( z coord )
            //var top = transform.lossyScale.z; 
            var top = CalcZ( Place.Height );

            cursor.transform.position = new Vector3( where.x, where.y, top + 0.2f );

	    }

        public void RedrawSelectedTile( GameObject cursor ) {
            RedrawTile();
            TakeCursor( cursor );
		}
        
        public void RedrawTile() {
//print("REDRAW="+name);

            // change 'cube' size
            var top = CalcZ( Place.Height );
            FixCube( Place.Height, top );
            FixAgent( Place.Agent, top );
            FixFlag( Place.Flag, top );
        }

        void FixAgent( Agent agent, float top ) {

            // null agent, hide token.
            if (agent==null) {
                if (token!=null && token.activeSelf) token.SetActive(false);
                return;
			}

            // create or find agent
            else {
                
print(">>>>>>>>>>>>>>>> BUILDING AGENT ===== at"+this.name);
                if (token==null) {
                    token  = Instantiate( Resources.Load("_prefabs/GoodToken") as GameObject );
print(">>>>>>>>>>>>>>>> NEW TOKEN");
				}

				token.transform.localPosition = new Vector3( VLOC.x-0.5f, VLOC.y-0.5f, top );
                token.transform.eulerAngles = new Vector3( 0f, 0f, ((int)agent.Face) * 45f );

                // token image with faction + agent appearance
                Material material = Instantiate( Resources.Load("Materials/ImageToken") as Material );

                int faction = agent.Faction;
                material.SetColor( "Background", Owner.factionColor[ faction ]  );

                string resourcePath = "Unpaid/Agents/"+agent.Type.Name;
                Texture orcFigure = (Texture)Resources.Load(resourcePath,typeof(Texture));
                material.SetTexture("Figure", orcFigure );

                token.GetComponent<Renderer>().material = material;
                token.SetActive( true );

                UseParent( Owner.tokenParent, token );
		    }

	    }

        internal void FixCube( HeightEnum height, float top ) {

            transform.localScale = new Vector3( 1, 1, top );
            transform.localPosition = new Vector3( VLOC.x-0.5f, VLOC.y-0.5f, top/2 );

            GetComponent<MeshRenderer>().material =  PickMaterial( height );
	    }

        /// <summary>
        /// Create flag as a billboard hovering over the tiles.
        /// </summary>
        /// <param name="flagType"></param>
        /// <param name="top"></param>
        internal void FixFlag( FlagEnum flagKey, float top ) {

print("FIXING FLAG = "+flagKey);
            if (flag==null) {

                flag = new GameObject( "Flag"+Key() );
                flag.transform.localEulerAngles = new Vector3( 90f, 0f, 0f );

				flag.AddComponent<SpriteRenderer>();
				flag.AddComponent<BillboardSprite>();

				UseParent( gameObject, flag );
            }

            flag.transform.localPosition  = new Vector3( 0, 0, 0.6f + top );  // relative to cube
            flag.transform.localScale = Owner.flagScale;    //new Vector3(0.3f,0.3f,0.3f);

            var asSprite = flag.GetComponent<SpriteRenderer>();
            asSprite.sprite = FlagSymbolsScript.GetFlagSprite( flagKey );
            asSprite.color = Owner.flagShade;   // new Color( 1f,1f,1f, 0.25f );
		}

        /// <summary>
        /// Where is the 'top' of this tile?
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        internal float CalcZ( HeightEnum height ) {
            if (height==HeightEnum.Pit) return 0.1f;
            //if (height==HeightEnum.Wall) return 1.8f;
            return (int)height * 0.3f;
	    }

        /// <summary>
        /// What material should we use for the tile?
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        internal Material PickMaterial( HeightEnum height ) {
            if (height==HeightEnum.Pit) return Owner.hidden;
            if (height==HeightEnum.Wall) return Owner.wall;
            return Floor;
	    }

    
    //======================================================================================================================

     //   /// <summary>
     //   /// Invoked by Dropdown Menu.
     //   /// </summary>
     //   public void SetHeight( int height ) {
     //       Place.Height = (HeightEnum)height;
     //       RedrawTile();
	    //}

        public void AddAgent( AgentType type, DirEnum face ) {
            GetCurrentMap().AddAgent( type, Place.Where, face );
            RedrawTile();
	    }
    
    //======================================================================================================================

        /// <summary>
        /// X/Y reference into the map.  DX/DY draw into world.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="floor">default material when this cube is not pit nor wall.</param>
        public void SetRef( ArenaManagerScript owner, Place place, float dx, float dy, Material floor ) {
            this.Owner = owner;
            this.Place = place;
            this.VLOC = new Vector2( dx, dy );
            this.Floor = floor;
	    }

        public ArenaManagerScript Owner { get; internal set; }

        // location in map
        public Place Place { get; internal set; }

        // center of tile in view
	    public Vector2 VLOC { get; internal set; }

        // material for 'pit' level tiles
        public Material Floor {  get; internal set; }

        /// <summary>
        /// label to identify objects.
        /// </summary>
        /// <returns></returns>
        internal string Key() {  return "("+Place.Where.X+","+Place.Where.Y+")";}

    }
    
}
