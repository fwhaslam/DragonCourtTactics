//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Arena { 

    using Realm;
    using Realm.Enums;
    using UnityEngine;
	using UnityEngine.Events;
	using UnityEngine.EventSystems;

    using static Shared.GlobalValues;
    using static Shared.UnityTools;

    /// <summary>
    /// Mange appearance and actions on a single Tile.
    /// </summary>
    public class TileScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {

        internal GameObject token = null;
        
        // when a tile is dragged, move the camera :: function( V3 start, V3 delta )
        static internal readonly UnityEvent<Vector3,Vector3> tileDragEvent = new UnityEvent<Vector3,Vector3>();

        // Start is called before the first frame update
        void Start() {
        }

//======================================================================================================================


	    public void OnPointerDown(PointerEventData eventData) {
    //print("CLICKED=="+transform.name);
		    EditToolsMenuScript.SelectTile( this );
	    }

        public void TakeCursor( GameObject cursor ) {

            cursor.SetActive(true);
            Vector3 where = transform.position;

            // approximate the 'top' of the object ( z coord )
            //var top = transform.lossyScale.z; 
            var top = CalcZ( Place.Height );

            cursor.transform.position = new Vector3( where.x, where.y, top );

	    }

        public void RedrawSelectedTile( GameObject cursor ) {
            RedrawTile();
            TakeCursor( cursor );
		}
        
        public void RedrawTile() {
    //print("REDRAW="+name);

            var flag = Place.Flag;

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

        internal void FixFlag( FlagEnum flag, float top ) {
            // TODO:
		}

        internal float CalcZ( HeightEnum height ) {
            if (height==HeightEnum.Pit) return 0.2f;
            if (height==HeightEnum.Wall) return 3f;
            return 0.2f + (int)height * 0.4f;
	    }

        internal Material PickMaterial( HeightEnum height ) {
            if (height==HeightEnum.Pit) return ArenaManagerScript.instance.hidden;
            if (height==HeightEnum.Wall) return ArenaManagerScript.instance.wall;
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
            currentMap.AddAgent( type, Place.Where, face );
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

    //======================================================================================================================

        internal Vector3 startDrag = Vector3.zero;
        internal Vector3 lastDrag = Vector3.zero;

	    public void OnBeginDrag(PointerEventData eventData) {
		    //print("Begin Drag = "+eventData );
            startDrag = eventData.position;
            lastDrag = eventData.position;
	    }

	    public void OnDrag(PointerEventData eventData) {

		    //print("On Drag = "+eventData );
            Vector3 next = eventData.position;
            Vector3 delta = next - lastDrag;
            lastDrag = next;

            // fire off event
           tileDragEvent.Invoke( startDrag, delta );
	    }

	    public void OnEndDrag(PointerEventData eventData) {
		    //print("End Drag = "+eventData );
	    }
    }
    
}
