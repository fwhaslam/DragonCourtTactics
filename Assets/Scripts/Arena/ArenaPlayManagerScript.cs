//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Arena {
	using Realm;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using UnityEngine;
	using UnityEngine.SceneManagement;

	using static Shared.GlobalValues;

	public class ArenaPlayManagerScript: ArenaManagerScript {
		
	    new public void OnEnable() {
 print("OnEnable for "+GetType().Name+" under ["+SceneManager.GetActiveScene().name+"]");
			base.OnEnable();
	    }

	    new public void OnDisable() {
 print("OnDisable for "+GetType().Name+" under ["+SceneManager.GetActiveScene().name+"]");
			base.OnDisable();
	    }

	    new void Awake() {
 print("Awake for "+GetType().Name+" under ["+SceneManager.GetActiveScene().name+"]");
			base.Awake();
	    }

	    void Start() {
 print("Start for "+GetType().Name+" under ["+SceneManager.GetActiveScene().name+"]");

			// play map SHOULD exist at start BUt won't exist then we just start from the scene
			var map = GetCurrentMap();
			if (map.Wide==0) map = RealmFactory.SimpleTerrain( 8, 8 );		// create new map

			base.Prepare( map );
	    }
	}

}
