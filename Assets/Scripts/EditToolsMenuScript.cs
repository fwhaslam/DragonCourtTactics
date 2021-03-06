//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

using Arena;

    using Realm;
    using Realm.Enums;
    using Realm.Puzzle;

using System;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This handles tile editing and menu updates for Edit Scene.
/// </summary>

public class EditToolsMenuScript : MonoBehaviour {

    public GameObject cursor;

	internal TileScript workingTile;
    internal TMP_Dropdown 
        unitTypeDropdown, unitFaceDropdown, unitGroupDropdown, unitStateDropdown,
        tileFlagDropdown;
    internal TMP_Text mapTitleLabel,
        mapSizeLabel,tileTypeLabel;

    internal List<string> 
        unitTypeOptions,unitFaceOptions,unitGroupOptions,unitStateOptions,
        tileFlagOptions;

    // single tile change event
    internal static UnityEvent<TileScript> tileSelectEvent = new UnityEvent<TileScript>();

	// Awake is called before OnEnable ( which is before Start )
	public void Awake() {

        mapTitleLabel = GameObject.Find("EditTitleTXT").GetComponent<TMP_Text>();
        
        mapSizeLabel = GameObject.Find("MapSizeLabel").GetComponent<TMP_Text>();
        tileTypeLabel = GameObject.Find("TileTypeLabel").GetComponent<TMP_Text>();

        unitTypeDropdown = GameObject.Find("UnitTypeDD").GetComponent<TMP_Dropdown>();
        unitFaceDropdown = GameObject.Find("UnitFaceDD").GetComponent<TMP_Dropdown>();
        unitGroupDropdown = GameObject.Find("UnitGroupDD").GetComponent<TMP_Dropdown>();
        unitStateDropdown = GameObject.Find("UnitStateDD").GetComponent<TMP_Dropdown>();
        tileFlagDropdown = GameObject.Find("TileFlagDD").GetComponent<TMP_Dropdown>();

        FixUnitTypeOptions();
        FixUnitFaceOptions();
        FixUnitGroupOptions();
        FixUnitStateOptions();
        FixTileFlagOptions();
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

		// fill in options on menus
        unitTypeDropdown.gameObject.SetActive(false);
        unitTypeDropdown.ClearOptions();
        unitTypeDropdown.AddOptions( unitTypeOptions );

        unitFaceDropdown.gameObject.SetActive(false);
        unitFaceDropdown.ClearOptions();
        unitFaceDropdown.AddOptions( unitFaceOptions );

        unitGroupDropdown.gameObject.SetActive(false);
        unitGroupDropdown.ClearOptions();
        unitGroupDropdown.AddOptions( unitGroupOptions );

        unitStateDropdown.gameObject.SetActive(false);
        unitStateDropdown.ClearOptions();
        unitStateDropdown.AddOptions( unitStateOptions );

        //tileFlagDropdown.gameObject.SetActive(true);
        tileFlagDropdown.ClearOptions();
        tileFlagDropdown.AddOptions( tileFlagOptions );

        // add listeners
        unitTypeDropdown.onValueChanged.AddListener(delegate {DoChangeUnitType();});
        unitFaceDropdown.onValueChanged.AddListener(delegate {DoChangeUnitFace();});
        unitGroupDropdown.onValueChanged.AddListener(delegate {DoChangeUnitGroup();});
        tileFlagDropdown.onValueChanged.AddListener(delegate {DoChangeTileFlag();});
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
    public void MapRedrawFunction(PuzzleMap level) {
        print("  ### ### ### ### EditToolsMenu => Map Redraw Function ");

        mapTitleLabel.text = "["+level.Title+"]";
        mapSizeLabel.text = "Size: "+level.Wide+" x "+level.Tall;

	}

    public void TileDragFunction( Vector3 delta ) {
        //print("Tile Drag = "+delta);
	}
    
//======================================================================================================================
//      Menu Options

    internal void FixUnitTypeOptions() {
        List<string> options = new List<string>();
        options.AddRange( AgentType.Keys() );
        unitTypeOptions = options;
	}

    internal void FixUnitFaceOptions() {
        List<string> options = new List<string>();
        foreach ( DirEnum value in Enum.GetValues(typeof(DirEnum))) {
            options.Add( value.ToString() );
		}
        unitFaceOptions = options;
	}

    internal void FixUnitGroupOptions() {
        List<string> options = new List<string>();
        options.Add ("White(hero)" );
        options.Add( "Red" );
        options.Add( "Green" );
        options.Add( "Blue" );
        unitGroupOptions = options;
	}

    internal void FixUnitStateOptions() {
        List<string> options = new List<string>();
        foreach ( StatusEnum value in Enum.GetValues(typeof(StatusEnum))) {
            options.Add( value.ToString() );
		}
        unitStateOptions = options;
	}

    internal void FixTileFlagOptions() {
        List<string> options = new List<string>();
        foreach ( FlagEnum value in Enum.GetValues(typeof(FlagEnum))) {
            options.Add( value.ToString() );
		}
        tileFlagOptions = options;
	}
   
//======================================================================================================================
//      Menu Actions

    public void DoChangeUnitType() {
        var agent = (workingTile==null ? null : workingTile.Place.Agent );
        if (agent!=null) {
            agent.Type = AgentType.Get( unitTypeDropdown.value );
            workingTile.RedrawTile();
        }
	}
   
    public void DoChangeUnitFace() {
        var agent = (workingTile==null ? null : workingTile.Place.Agent );
        if (agent!=null) {
            agent.Face = (DirEnum)unitFaceDropdown.value;
            workingTile.RedrawTile();
        }
	}
   
    public void DoChangeUnitGroup() {
        var agent = (workingTile==null ? null : workingTile.Place.Agent );
        if (agent!=null) {
            agent.Faction = unitGroupDropdown.value;
             workingTile.RedrawTile();
       }
	}
  
    public void DoChangeUnitState() {
        var agent = (workingTile==null ? null : workingTile.Place.Agent );
        if (agent!=null) {
            agent.Status = (StatusEnum)unitStateDropdown.value;
            workingTile.RedrawTile();
       }
	}
  
    public void DoChangeTileFlag() {
        if (workingTile!=null) {
            workingTile.Place.Flag = (FlagEnum)tileFlagDropdown.value;
            workingTile.RedrawTile();
        }
	}

//======================================================================================================================
//      Menus Affect Map

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

        public void AddAgent() {
 print("ADD AGENT");
            if (workingTile==null) return;          // nothing selected

            if (workingTile.Place.Agent==null) {
                Place place = workingTile.Place;

                var newAgent = new Agent( place.Where );
                newAgent.Type = AgentType.Get( unitTypeDropdown.value );
                newAgent.Face = (DirEnum) unitFaceDropdown.value;
                newAgent.Faction = unitGroupDropdown.value;
                newAgent.Status = (StatusEnum)unitStateDropdown.value;

                place.Agent = new Agent( place.Where );
                workingTile.RedrawTile();
                tileSelectEvent.Invoke(workingTile);
		    }
	    }

        public void DropAgent() {

print("Remove AGENT!");
            if (workingTile==null) return;          // nothing selected
            if (workingTile.Place.Agent!=null) {
                workingTile.Place.Agent = null;
                workingTile.RedrawTile();
               tileSelectEvent.Invoke(workingTile);
		    }
        }

//======================================================================================================================
// Tile Select + Drag

    /// <summary>
    /// When a tile is clicked, invoke this method.
    /// </summary>
    /// <param name="who"></param>
    static public void TileSelect( TileScript who ) {
print("SelectTile = " + who?.Place?.Where);
		tileSelectEvent.Invoke( who );
    }

    /// <summary>
    /// When player clicks on a tile, select that tile and update menus.
    /// </summary>
    /// <param name="nextTile"></param>
    void DoUpdateWorkingTile( TileScript nextTile ) {

print("DoUpdateTile=" + nextTile?.Place?.Where);
		// no work!
		//if ( workingTile==nextTile ) return;

        // disable graphics and UI for old tile
        if ( workingTile!=null && nextTile==null ) {
            cursor.SetActive(false);
            workingTile = null;
            UpdateTileLabels();
            return;
		}

        // update menu & mark with cursor
        workingTile = nextTile;
        if (workingTile!=null) {

            // update cursor for new tile
            workingTile.TakeCursor( cursor );
        }

         UpdateTileLabels();
	}
    
    internal void UpdateTileLabels() {
print("UpdateTileLabels");

        if (workingTile==null) {
            tileTypeLabel.text = "Tile: - - -";
		}
        else {
print("WorkingTile at "+workingTile.Place.Where);
            Place place = workingTile.Place;
            tileTypeLabel.text = "Tile: "+place.Height.ToString();
            tileFlagDropdown.value = (int)workingTile.Place.Flag;
        }
 
        // agent dropdowns
        var agent = (workingTile==null ? null : workingTile.Place.Agent);
        if (agent==null) {
            unitTypeDropdown.gameObject.SetActive( false );
            unitFaceDropdown.gameObject.SetActive( false );
            unitGroupDropdown.gameObject.SetActive( false );
            unitStateDropdown.gameObject.SetActive( false );
		}
        else {

            unitTypeDropdown.value = unitTypeOptions.IndexOf( agent.Type.Name );
            unitFaceDropdown.value = (int)agent.Face;
            unitGroupDropdown.value = agent.Faction;
            unitStateDropdown.value = (int)agent.Status;
                       
            unitTypeDropdown.gameObject.SetActive( true );
            unitFaceDropdown.gameObject.SetActive( true );
            unitGroupDropdown.gameObject.SetActive( true );
            unitStateDropdown.gameObject.SetActive( true );
		}
	}
    
//======================================================================================================================

    internal void HandleEditKeyboard() {
        
        if (Input.GetKeyDown(KeyCode.Greater) ||
            Input.GetKeyDown(KeyCode.Period) ) RaiseTileHeight();

        if (Input.GetKeyDown(KeyCode.Less) || 
            Input.GetKeyDown(KeyCode.Comma)) LowerTileHeight();
	}

}
