
using Realm;
using Realm.Enums;

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
    internal TMP_Dropdown optionMenu, heightMenu, flagMenu, agentMenu ;
    internal TMP_Text mapSizeLabel,mapTitleLabel;

    // single tile change event
    internal static UnityEvent<GameObject> tileSelectEvent = new UnityEvent<GameObject>();


	// Start is called before the first frame update
	public void Start() {

        optionMenu = GameObject.Find("OptionPicker").GetComponent<TMP_Dropdown>();
		heightMenu = GameObject.Find("HeightPicker").GetComponent<TMP_Dropdown>();
        flagMenu = GameObject.Find("FlagPicker").GetComponent<TMP_Dropdown>();
        agentMenu = GameObject.Find("AgentPicker").GetComponent<TMP_Dropdown>();

        mapSizeLabel = GameObject.Find("MapSizeLabel").GetComponent<TMP_Text>();
        mapTitleLabel = GameObject.Find("MapTitleLabel").GetComponent<TMP_Text>();

		// fill in options on menus
		heightMenu.ClearOptions();
		heightMenu.AddOptions(new List<string>(Enum.GetNames(typeof(HeightEnum))));

		flagMenu.ClearOptions();
		flagMenu.AddOptions(new List<string>(Enum.GetNames(typeof(FlagEnum))));

		agentMenu.ClearOptions();
		agentMenu.AddOptions( AgentType.GetOptions() );

        // add listeners
        optionMenu.onValueChanged.AddListener(delegate {DoChangeOption();});
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
	    tileSelectEvent.AddListener( TileEventFunction );	 
        ManageArenaScript.mapRedrawEvent.AddListener( MapRedrawFunction );
	}

    /// <summary>
    /// Remove self as Event Listener
    /// </summary>
	public void OnDisable() {
        tileSelectEvent.RemoveListener( TileEventFunction );
        ManageArenaScript.mapRedrawEvent.RemoveListener( MapRedrawFunction );
	}

    /// <summary>
    /// Delegate for TileEvent.
    /// </summary>
    /// <param name="tile"></param>
    public void TileEventFunction( GameObject tile ) {
        DoUpdateWorkingTile( tile );
	}

    /// <summary>
    /// Delegate for map redraw events.
    /// </summary>
    public void MapRedrawFunction(LevelMap level) {
        print("Map Redraw Function ");

        mapTitleLabel.text = "Editing ("+level.Title+")";


	}

    public void TileDragFunction( Vector3 delta ) {
        //print("Tile Drag = "+delta);
	}
    
//======================================================================================================================
//      Options Menu


   
//======================================================================================================================
//      Size Menu

//======================================================================================================================
// Tile Select + Drag

    /// <summary>
    /// When a tile is clicked, invoke this method.
    /// </summary>
    /// <param name="who"></param>
    static public void SelectTile( GameObject who ) {
//print("SelectTile = "+who );
        tileSelectEvent.Invoke( who );
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
        Place place = map.Places[myRef.MLOC.X, myRef.MLOC.Y];
        heightMenu.value = (int)place.Height;
        flagMenu.value = (int)place.Flag;

		//agentMenu.value = place.Agent.Type.Index;
	}

    /// <summary>
    /// When a tile is dragged, call this method.
    /// </summary>
    /// <param name="delta"></param>
    static public void DragTile( Vector3 delta ) {

	}
    
//======================================================================================================================

    public void DoChangeOption() {
        var pick = optionMenu.options[optionMenu.value].text;
print("OPTION="+pick);
        switch (pick) { 
            case "Exit": 
                UnityTools.ChangeScene( GlobalValues.EntrySceneName );
                break;
            case "Save": /*load*/ break;
            case "Load": /*save*/ break;
            case "Reset": /*save*/ break;
            default:
                throw new UnityException("Unknown Option Menu Selection = ["+pick+"]");
		}
	}


    public void DoChangeHeight() {
print("NEW HIEGHT="+heightMenu.value);
        if (workingTile==null) return;
        workingTile.GetComponent<TileScript>().SetHeight( heightMenu.value );
	}

    public void DoChangeAgent() {
        print("NEW AGENT="+agentMenu.value);
        AgentType type = AgentType.Get( agentMenu.value );
        workingTile.GetComponent<TileScript>().AddAgent( type, DirEnum.North );
	}

    public void DoChangeFlag() {
        print("NEW FLAG="+flagMenu.value);
	}

}
