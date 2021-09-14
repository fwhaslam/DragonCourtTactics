using Realm;

using Shared;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using static Shared.GlobalValues;

public class TileScript : MonoBehaviour, IPointerDownHandler {

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
		EditToolsMenuScript.SelectTile( transform.gameObject );
	}

    public void TakeCursor( GameObject cursor ) {

        cursor.SetActive(true);
        Vector3 where = transform.position;

        // approximate the 'top' of the object ( z coord )
        //var top = transform.lossyScale.z; 
        var top = CalcZ( currentMap.HeightLayer[ X, Y ] );

        cursor.transform.position = new Vector3( where.x, where.y, top );

	}

    public void RedrawTile() {
//print("REDRAW="+name);

        var flag = currentMap.FlagLayer[ X, Y ];

        // change 'cube' size
        var height = currentMap.HeightLayer[ X, Y ];
        var top = CalcZ( height );
        FixCube( height, top );

		int agentId = currentMap.AgentLayer[X, Y];
		//Agent agent = currentMap.Agents[agentId];

        FixAgent( agentId, top );
    }

    void FixAgent( int agentId, float top ) {

        // shift agent
        if (agentId!=0) {

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
				token.transform.localPosition = new Vector3( DX, DY, top );
                token.SetActive( true );

                // TODO: create an 'agent pool'
                UnityTools.UseParent( MapHandlerScript.instance.gameObject, token );

                // TODO: add decal for type
            }
		}

        // remove agent
        if (agentId==0) {

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
        transform.localPosition = new Vector3( DX, DY, 0 );

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


    /// <summary>
    /// Invoked by Dropdown Menu.
    /// </summary>
    public void SetHeight( int height ) {
        currentMap.HeightLayer[ X, Y ] = (HeightEnum)height;
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
		this.X = x;
		this.Y = y;
        this.DX = dx;
        this.DY = dy;
        this.Floor = floor;
	}

	public int X { get; internal set; }

	public int Y { get; internal set; }

	public float DX { get; internal set; }

	public float DY { get; internal set; }

    public Material Floor { get; internal set; }

}
