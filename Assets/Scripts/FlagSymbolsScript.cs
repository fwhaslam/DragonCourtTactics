using Realm.Enums;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NOTE: The values in this script MUST match the FlagEnum from the RealmModel.
/// </summary>
public class FlagSymbolsScript : MonoBehaviour {

	//public Sprite None;	// should always be null

	public Sprite Entry;
	public Sprite Door;
	public Sprite Hostage;
	public Sprite Chest;
	public Sprite Sack;

	[Space]
	public Sprite Lever;
	public Sprite Switch;
	public Sprite Gears;
	public Sprite Pitfall;
	public Sprite Masher;

	[Space]
	public Sprite Lower;
	public Sprite Raise;
	public Sprite Spikes;
	public Sprite Arrows;
	public Sprite Boulder;

//======================================================================================================================

	static FlagSymbolsScript instance;

	public FlagSymbolsScript() {
		instance = this;
	}

	static public Sprite GetFlagSprite( FlagEnum flagKey ) {

		switch (flagKey) {

			case FlagEnum.None: return null;

			case FlagEnum.Entry: return instance.Entry;
			case FlagEnum.Door: return instance.Door;
			case FlagEnum.Hostage: return instance.Hostage;
			case FlagEnum.Chest: return instance.Chest;
			case FlagEnum.Sack: return instance.Sack;

			case FlagEnum.Lever: return instance.Lever;
			case FlagEnum.Switch: return instance.Switch;
			case FlagEnum.Gears: return instance.Gears;
			case FlagEnum.Pitfall: return instance.Pitfall;
			case FlagEnum.Masher: return instance.Masher;

			case FlagEnum.Lower: return instance.Lower;
			case FlagEnum.Raise: return instance.Raise;
			case FlagEnum.Spikes: return instance.Spikes;
			case FlagEnum.Arrows: return instance.Arrows;
			case FlagEnum.Boulder: return instance.Boulder;

			default:
				throw new UnityException("Invalid value for FlagEnum = ["+flagKey+"]");
		}

	}

}
