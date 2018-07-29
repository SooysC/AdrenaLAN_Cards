using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeline : MonoBehaviour {
    public bool gameStarted = false;
    public GameObject gameBoard;

	public GameObject important;

	public enum Game_State {game_setup, game_start, player_a_phase, player_b_phase, game_over};
	public Game_State current_state = Game_State.game_setup;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		GameGlobals.done;
	}

	public void nextState () {
		switch (current_state) {
			case Game_State.game_setup:
				current_state = Game_State.game_start;
				break;
			case Game_State.game_start:
				current_state = Game_State.player_a_phase;
				break;
			case Game_State.player_a_phase:
				current_state = Game_State.player_b_phase;
				break;
			case Game_State.player_b_phase:
				// For now we'll make the game one round only
				current_state = Game_State.game_over;
				break;
			case Game_State.game_over:
				current_state = Game_State.game_over;
				break;
			default:
				Debug.Log("Should not go here.");
				break;
		}
	}
}
