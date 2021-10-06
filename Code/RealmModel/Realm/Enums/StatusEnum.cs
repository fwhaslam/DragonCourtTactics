//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Enums {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// Status for agents.  
	/// This is affected by special abilities in the world.
	/// The status defines how active an agent is.
	/// </summary>
	public enum StatusEnum {

		Active,		// will act against all visible enemies
		Alert,		// unit is waiting, but will become active if it spots nearby foes
		Stunned,	// unit is unable to move for one turn, then becomes Alert
		Sleep		// unit is waiting, will become active if struck or foe moves adjacent
	}
}
