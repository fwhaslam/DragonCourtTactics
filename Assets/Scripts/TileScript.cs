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
print("REDRAW="+name);

        var flag = currentMap.FlagLayer[ X, Y ];

        // change 'cube' size
        var top = CalcZ( currentMap.HeightLayer[ X, Y ] );
        transform.localScale = new Vector3( 1, 1, top );
        transform.localPosition = new Vector3( DX, DY, 0 );

		int agentId = currentMap.AgentLayer[X, Y];
		//Agent agent = currentMap.Agents[agentId];

        if (agentId!=0) {
print(">>>>>>>>>>>>>>>> DRAWING AGENT ===== at"+this.name);

            token = Instantiate( Resources.Load("_prefabs/GoodToken") as GameObject );
            token.transform.localPosition = new Vector3( DX, DY, top );
            token.SetActive( true );

            // TODO: add decal for type
		}


	}

    internal float CalcZ( HeightEnum height ) {
        return 0.2f + (int)height * 0.8f;
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
    public void SetRef( int x, int y, float dx, float dy ) {
		this.X = x;
		this.Y = y;
        this.DX = dx;
        this.DY = dy;
	}

	public int X { get; internal set; }

	public int Y { get; internal set; }

	public float DX { get; internal set; }

	public float DY { get; internal set; }

}
