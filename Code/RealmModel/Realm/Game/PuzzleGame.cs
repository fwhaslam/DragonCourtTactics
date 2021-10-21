//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Game {
	using Realm.Brain;
	using Realm.Puzzle;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;


	/// <summary>
	/// A game is a puzzle in progress.  The game states are organized into 'actions' and 'turns'.
	/// An action is a the advancement of a single agent with onboard consequences.  
	/// A turn is when all agents have acted and we progress to a new round of actions.
	/// Each action and turn creates a snapshot state of the puzzle, so there is complete playback.
	/// </summary>
	public class PuzzleGame {

		public PuzzleGame() : this(new PuzzleMap()) {}

		public PuzzleGame( PuzzleMap start ) {
			turnStates.Add( new PuzzleState( start ) );	// slot zero
		}

		// one brain per active faction, with PlayerBrain as zero
		public List<BrainIF> Mind {  get; set; }

		// snapshot of puzzle after individual agent action-chains
		internal List<PuzzleState> actionStates = new List<PuzzleState>();

		// snapshot of game just before player turn ( which is followed by faction turns )
		internal List<PuzzleState> turnStates = new List<PuzzleState>();

		/// <summary>
		/// Is the game state such the player must take actions ?
		/// </summary>
		/// <returns></returns>
		public bool IsPlayerTurn() {
			return true;
		}

		public bool IsGameOver() {
			return false;
		}

		/// <summary>
		/// True when it is NOT the playersw turn, and game is not over.
		/// </summary>
		/// <returns></returns>
		public bool HasNext() {
			return true;
		}

		/// <summary>
		/// What are all the enemy faction actions?
		/// </summary>
		/// <returns></returns>
		public List<ActionChain> Next() {
			return null;
		}

		public PuzzleState GetState( int turnId ) {
			return turnStates[turnId];
		}

		public void SetStartState( PuzzleMap map ) {
			turnStates[0] = new PuzzleState( null, map );
		}

		public void SetState( int turnId, ActionChain actions, PuzzleMap map ) {
			turnStates[turnId] = new PuzzleState( actions,map );
		}
	}

}
