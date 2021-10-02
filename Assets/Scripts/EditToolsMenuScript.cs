//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

using Arena;

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

	internal TileScript workingTile;
    internal TMP_Dropdown optionMenu, flagMenu ;
    internal TMP_Text mapTitleLabel,
        mapSizeLabel,tileTypeLabel,tileFlagLabel,
        unitTypeLabel,unitFaceLabel,unitGroupLabel;

    // single tile change event
    internal static UnityEvent<TileScript> tileSelectEvent = new UnityEvent<TileScript>();

	// Awake is called before OnEnable ( which is before Start )
	public void Awake() {

        mapTitleLabel = GameObject.Find("MapTitleLabel").GetComponent<TMP_Text>();
        
        mapSizeLabel = GameObject.Find("MapSizeLabel").GetComponent<TMP_Text>();
        tileTypeLabel = GameObject.Find("TileTypeLabel").GetComponent<TMP_Text>();
        tileFlagLabel = GameObject.Find("TileFlagLabel").GetComponent<TMP_Text>();

        unitTypeLabel = GameObject.Find("UnitTypeLabel").GetComponent<TMP_Text>();
        unitFaceLabel = GameObject.Find("UnitFaceLabel").GetComponent<TMP_Text>();
        unitGroupLabel = GameObject.Find("UnitGroupLabel").GetComponent<TMP_Text>();
    }
    
    /// <summary>
    /// Add self as Event Listener
    /// </summary>
	public void OnEnable() {
	    tileSelectEvent.AddListener( TileEventFunction );	 
        ArenaManagerScript.mapRedrawEvent.AddListener( MapRedrawFunction );
	}

    /// <summary>
    /// Remove self as Event Listener
    /// </summary>
	public void OnDisable() {
        tileSelectEvent.RemoveListener( TileEventFunction );
        ArenaManagerScript.mapRedrawEvent.RemoveListener( MapRedrawFunction );
	}

	// Start is called after Awake/Enable, but before any Update
	public void Start() {

	    optionMenu = GameObject.Find("OptionPicker").GetComponent<TMP_Dropdown>();
        flagMenu = GameObject.Find("FlagPicker").GetComponent<TMP_Dropdown>();

		// fill in options on menus
		flagMenu.ClearOptions();
		flagMenu.AddOptions(new List<string>(Enum.GetNames(typeof(FlagEnum))));

        // add listeners
        optionMenu.onValueChanged.AddListener(delegate {DoChangeOption();});
        flagMenu.onValueChanged.AddListener(delegate {DoChangeFlag();});
	}


    /// <summary>
    /// Called once/frame.  Can handle keystrokes.
    /// </summary>
	public void Update() {
		HandleEditKeyboard();
	}

	//======================================================================================================================

	/// <summary>
	/// Delegate for TileEvent.
	/// </summary>
	/// <param name="tile"></param>
	public void TileEventFunction( TileScript tile ) {
        DoUpdateWorkingTile( tile );
	}

    /// <summary>
    /// Delegate for map redraw events.
    /// </summary>
    public void MapRedrawFunction(LevelMap level) {
        print("  ### ### ### ### EditToolsMenu => Map Redraw Function ");

        mapTitleLabel.text = "Editing ("+level.Title+")";
        mapSizeLabel.text = "Size: "+level.Wide+" x "+level.Tall;

	}

    public void TileDragFunction( Vector3 delta ) {
        //print("Tile Drag = "+delta);
	}
    
//======================================================================================================================
//      Options Menu


   
//======================================================================================================================
//      Height Menu

        public void RaiseTileHeight() {
print("RAISE TILE HEIGHT >>>>>>>");

            if (workingTile==null) return;          // nothing selected

            HeightEnum height = workingTile.Place.Height;
            if (height==HeightEnum.Wall) return;        // at limit

            workingTile.Place.Height = (HeightEnum)((int)height + 1 );
            workingTile.RedrawSelectedTile( cursor );
		}

        public void LowerTileHeight() {
  print("LOWER TILE HEIGHT >>>>>>>");
      
            if (workingTile==null) return;          // nothing selected

            HeightEnum height = workingTile.Place.Height;
            if (height==HeightEnum.Pit) return;        // at limit

            workingTile.Place.Height = (HeightEnum)((int)height - 1 );
            workingTile.RedrawSelectedTile( cursor );
		}

//======================================================================================================================
// Tile Select + Drag

    /// <summary>
    /// When a tile is clicked, invoke this method.
    /// </summary>
    /// <param name="who"></param>
    static public void SelectTile( TileScript who ) {
print("SelectTile = " + who.Place.Where);
		tileSelectEvent.Invoke( who );
    }

    /// <summary>
    /// When player clicks on a tile, select that tile and update menus.
    /// </summary>
    /// <param name="nextTile"></param>
    void DoUpdateWorkingTile( TileScript nextTile ) {

print("DoUpdateTile=" + nextTile.Place.Where);
		// no work!
		if ( workingTile==nextTile ) return;

        // disable graphics and UI for old tile
        if ( workingTile!=null && nextTile==null ) {
            cursor.SetActive(false);
            workingTile = null;
            UpdateTileLabels();
            return;
		}

        // update menu & mark with cursor
        workingTile = nextTile;
        Place place = workingTile.Place;

        // update cursor for new tile
        workingTile.TakeCursor( cursor );

        // setting menu values
        flagMenu.value = (int)place.Flag;
		//agentMenu.value = place.Agent.Type.Index;

         UpdateTileLabels();
	}

    /// <summary>
    /// When a tile is dragged, call this method.
    /// </summary>
    /// <param name="delta"></param>
 //   static public void DragTile( Vector3 delta ) {

	//}
    
    internal void UpdateTileLabels() {
print("UpdateTileLabels");

        if (workingTile==null) {
            tileTypeLabel.text = "Tile: - - -";
            tileFlagLabel.text = "Flag: - - -";
		}
        else {
print("WorkingTile at "+workingTile.Place.Where);
            Place place = workingTile.Place;
            tileTypeLabel.text = "Tile: "+place.Height.ToString();
            tileFlagLabel.text = "Flag: "+place.Flag.ToString();
        }

        var agent = workingTile?.Place.Agent;
        if (agent==null) {
            unitTypeLabel.text = "Type: - - -";
            unitFaceLabel.text = "Face: - - -";
            unitGroupLabel.text = "Group: - - -";
		}
        else {
            unitTypeLabel.text = "Type: "+agent.Type.Name;
            unitFaceLabel.text = "Face: "+agent.Face.ToString();
            unitGroupLabel.text = "Group: "+agent.Faction;
		}
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

    public void DoChangeFlag() {
        print("NEW FLAG="+flagMenu.value);
	}
    
//======================================================================================================================

    internal void HandleEditKeyboard() {
        
        if (Input.GetKeyDown(KeyCode.Greater) ||
            Input.GetKeyDown(KeyCode.Period) ) RaiseTileHeight();

        if (Input.GetKeyDown(KeyCode.Less) || 
            Input.GetKeyDown(KeyCode.Comma)) LowerTileHeight();
	}

}
