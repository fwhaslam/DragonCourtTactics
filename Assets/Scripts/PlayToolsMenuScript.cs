using Arena;

using Realm.Puzzle;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class PlayToolsMenuScript : MonoBehaviour {

    internal TMP_Text mapTitleLabel;

    // Start is called before the first frame update
    void Awake()
    {
        mapTitleLabel = GameObject.Find("PlayTitleTXT").GetComponent<TMP_Text>();
print("PLAY TITLE = "+mapTitleLabel);
    }

	public void OnEnable() { 
        ArenaManagerScript.mapRedrawEvent.AddListener( MapRedrawFunction );
	}

	public void OnDisable() { 
        ArenaManagerScript.mapRedrawEvent.RemoveListener( MapRedrawFunction );
	}

	// Update is called once per frame
	void Update()
    {
        
    }

    void MapRedrawFunction( PuzzleMap level ) {
print("LEVEL="+level);
print("TITLE="+level.Title);
print("PLAY TITLE = "+mapTitleLabel);

        mapTitleLabel.text = level.Title;
	}

}
