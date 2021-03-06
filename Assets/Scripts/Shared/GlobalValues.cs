//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Shared {
	
	using Realm;
	using Realm.Game;
	using Realm.Puzzle;

	public class GlobalValues {

		// current map for editing or play
		static public PuzzleGame editingMap = new PuzzleGame();

		static public PuzzleMap GetCurrentMap() {
			return editingMap.GetState( 0 ).Puzzle;
		}
		static public void SetCurrentMap(PuzzleMap map) {
			editingMap.SetStartState( map );
		}

		// the play scene can be called from several other scenes
		static public string playSceneReturn;

		// Offscreen Location of preloaded tiles.
		static public readonly float TILE_X_LOC = 1000f;
		static public readonly float TILE_Y_LOC = 1000f;

		// scene names
		static public readonly string EntrySceneName = "_MainScene";
		static public readonly string EditSceneName = "EditScene";
		static public readonly string PlaySceneName = "PlayScene";
		static public readonly string CamnpaignSceneName = "CampaignScene";

		// placeholder for save puzzle folder location. ( relative to Project folder )
		static public readonly string SavedPuzzlesFolder = "Assets\\Puzzles";
		static public readonly string PuzzleFileExtension = "_dctpzl.yml";
	}
}

