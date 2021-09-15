using Realm;
using Realm.Enums;
using Realm.Tools;

using Shared;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using static Shared.GlobalValues;

public class TileScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {

    internal GameObject token;

    // Start is called before the first frame update
    void Start() {
        token = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void OnPointerDown(PointerEventData eventData) {
//print("CLICKED=="+transform.name);
		EditToolsMenuScript.SelectTile( gameObject );
	}

    public void TakeCursor( GameObject cursor ) {

        cursor.SetActive(true);
        Vector3 where = transform.position;

        // approximate the 'top' of the object ( z coord )
        //var top = transform.lossyScale.z; 
        var top = CalcZ( currentMap.Places[ MLOC.X, MLOC.Y ].Height );

        cursor.transform.position = new Vector3( where.x, where.y, top );

	}

    public void RedrawTile() {
//print("REDRAW="+name);

        Place place = currentMap.Places[MLOC.X,MLOC.Y];
        var flag = place.Flag;

        // change 'cube' size
        var height = place.Height;
        var top = CalcZ( height );
        FixCube( height, top );

		Agent agent = place.Agent;

        FixAgent( agent, top );
    }

    void FixAgent( Agent agent, float top ) {

        // Agent found
        if (agent!=null) { 

            // token exists
            if (token!=null) {
                // no work
            }
            // create agent
            else {
print(">>>>>>>>>>>>>>>> CREATING AGENT ===== at"+this.name);

				token = Instantiate(Resources.Load("_prefabs/GoodToken") as GameObject);
				//GameObject basis = GameObject.Find("GoodToken");
				//token = GameObject.Instantiate( basis );
				token.transform.localPosition = new Vector3( VLOC.x+0.5f, VLOC.y+0.5f, top+0.5f );
                token.SetActive( true );

                // TODO: create an 'agent pool'
                UnityTools.UseParent( MapHandlerScript.instance.gameObject, token );

                // TODO: add decal for type
            }
		}

        // Agent not found
        else {

            // no token
            if (token==null) {
                // no work
            }
            // hide token
            else {
                token.SetActive(false);
		    }
        }

	}

    internal void FixCube( HeightEnum height, float top ) {

        transform.localScale = new Vector3( 1, 1, top );
        transform.localPosition = new Vector3( VLOC.x-0.5f, VLOC.y-0.5f, top/2 );

        GetComponent<MeshRenderer>().material =  PickMaterial( height );
	}

    internal float CalcZ( HeightEnum height ) {
        if (height==HeightEnum.Pit) return 0.2f;
        if (height==HeightEnum.Wall) return 3f;
        return 0.2f + (int)height * 0.4f;
	}

    internal Material PickMaterial( HeightEnum height ) {
        if (height==HeightEnum.Pit) return MapHandlerScript.instance.hidden;
        if (height==HeightEnum.Wall) return MapHandlerScript.instance.wall;
        return Floor;
	}

    
//======================================================================================================================

    /// <summary>
    /// Invoked by Dropdown Menu.
    /// </summary>
    public void SetHeight( int height ) {
        currentMap.Places[ MLOC.X, MLOC.Y ].Height = (HeightEnum)height;
        RedrawTile();
	}

    public void AddAgent( AgentType type, DirEnum face ) {
        currentMap.AddAgent( type, MLOC, face );
        RedrawTile();
	}
    
//======================================================================================================================

    /// <summary>
    /// X/Y reference into the map.  DX/DY draw into world.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="dx"></param>
    /// <param name="dy"></param>
    /// <param name="floor">default material when this cube is not pit nor wall.</param>
    public void SetRef( int x, int y, float dx, float dy, Material floor ) {
        this.MLOC = new Where( x, y );
        this.VLOC = new Vector2( dx, dy );
        this.Floor = floor;
	}

    // location in map
    public Where MLOC { get; internal set; }

    // center of tile in view
	public Vector2 VLOC { get; internal set; }

    // material for 'pit' level tiles
    public Material Floor {  get; internal set; }

//======================================================================================================================

    internal Vector3 lastDrag = Vector3.zero;

	public void OnBeginDrag(PointerEventData eventData) {
		//print("Begin Drag = "+eventData );
        lastDrag = eventData.position;
	}

	public void OnDrag(PointerEventData eventData) {
		//print("On Drag = "+eventData );
        Vector3 next = eventData.position;
        Vector3 delta = next - lastDrag;
        lastDrag = next;
        MainCameraHandler.TileDragged( delta );
	}

	public void OnEndDrag(PointerEventData eventData) {
		//print("End Drag = "+eventData );
	}
}
