
using Realm;

using Shared;

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using static Shared.GlobalValues;

/// <summary>
/// This handles tile editing and menu updates for Edit Scene.
/// </summary>
public class EditToolsMenuScript : MonoBehaviour {

    public GameObject cursor;

	internal GameObject workingTile;
    internal TMP_Dropdown heightMenu, flagMenu, agentMenu ;

    internal static UnityEvent<GameObject> tileEvent = new UnityEvent<GameObject>();


	// Start is called before the first frame update
	public void Start() {

		heightMenu = GameObject.Find("HeightPicker").GetComponent<TMP_Dropdown>();
        flagMenu = GameObject.Find("FlagPicker").GetComponent<TMP_Dropdown>();
        agentMenu = GameObject.Find("AgentPicker").GetComponent<TMP_Dropdown>();

        // fill in options on menus
        heightMenu.ClearOptions();
        heightMenu.AddOptions( new List<string>(Enum.GetNames(typeof(HeightEnum))) );

		flagMenu.ClearOptions();
		flagMenu.AddOptions(new List<string>(Enum.GetNames(typeof(FlagEnum))));

		agentMenu.ClearOptions();
		agentMenu.AddOptions( AgentType.GetOptions() );

        // add listeners
        heightMenu.onValueChanged.AddListener(delegate {DoChangeHeight();});
        flagMenu.onValueChanged.AddListener(delegate {DoChangeFlag();});
        agentMenu.onValueChanged.AddListener(delegate {DoChangeAgent();});
	}

	// Update is called once per frame
	public void Update() {

    }

//======================================================================================================================

    /// <summary>
    /// Add self as Event Listener
    /// </summary>
	public void OnEnable() {
	    tileEvent.AddListener( TileEventFunction );	 
	}

    /// <summary>
    /// Remove self as Event Listener
    /// </summary>
	public void OnDisable() {
        tileEvent.RemoveListener( TileEventFunction );
	}

    /// <summary>
    /// Delegate for TileEvent.
    /// </summary>
    /// <param name="tile"></param>
    public void TileEventFunction( GameObject tile ) {
        DoUpdateWorkingTile( tile );
	}

//======================================================================================================================

	/// <summary>
	/// Utility for examining component tree.
	/// </summary>
	/// <param name="obj"></param>
	void inspectComponents( GameObject obj ) {
print("INSPECT >>>>>>> "+obj);
         Component[] comps = obj.GetComponents<Component>(); 
print("COUNT = "+comps.Length );
        foreach ( Component cmp in comps ) {
            print("COMPONENT="+cmp);
            print("TYPE="+cmp.GetType());
		}
	}

    /// <summary>
    /// Select of unselect ( who==null) current tile.
    /// </summary>
    /// <param name="who"></param>
    static public void SelectTile( GameObject who ) {
//print("SelectTile = "+who );
        tileEvent.Invoke( who );
    }

    /// <summary>
    /// When player clicks on a tile, select that tile and update menus.
    /// </summary>
    /// <param name="nextTile"></param>
    void DoUpdateWorkingTile( GameObject nextTile ) {

//print("DoUpdateTile="+nextTile);
        // no work!
        if ( workingTile==nextTile ) return;

        // disable graphics and UI for old tile
        if ( workingTile!=null && nextTile==null ) {
            cursor.SetActive(false);
            workingTile = null;
            return;
		}

        // update menu & mark with cursor
        workingTile = nextTile;
        
        TileScript tileScript = workingTile.GetComponent<TileScript>();
        
        // update cursor for new tile
        tileScript.TakeCursor( cursor );

        // update UI for tile info
        TileScript myRef = workingTile.GetComponent<TileScript>();
        LevelMap map = currentMap;

        // setting menu values
        heightMenu.value = (int)map.HeightLayer[myRef.X, myRef.Y];
        flagMenu.value = (int)map.FlagLayer[myRef.X, myRef.Y];

		//int agentId = map.AgentLayer[myRef.X, myRef.Y];
        //Agent agent = map.Agents[agentId];
	}
    
//======================================================================================================================

    public void DoChangeHeight() {
print("NEW HIEGHT="+heightMenu.value);
        if (workingTile==null) return;
        workingTile.GetComponent<TileScript>().SetHeight( heightMenu.value );
	}

    public void DoChangeAgent() {
        print("NEW AGENT="+agentMenu.value);
	}

    public void DoChangeFlag() {
        print("NEW FLAG="+flagMenu.value);
	}

}
