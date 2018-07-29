using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeline : MonoBehaviour {
    public bool gameStarted = false;

	public GameObject Title;

	public enum Game_State {game_title, game_setup, game_start, player_a_phase, player_b_phase, game_over};
	public Game_State current_state = Game_State.game_title;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        nextState();
    }

    //IEnumerator game() {
    //}

	public void nextState () {
		switch (current_state) {
            case Game_State.game_title:
                Debug.Log("state:game_title");
                if (GameGlobals.gameboardTriggered)
                {
                    Debug.Log("found");
                    //if (GameObject.Find("Title") != null)
                        Destroy(GameObject.Find("Title"));
                    //else
                        Destroy(GameObject.Find("Title(Clone)"));
                    current_state = Game_State.game_setup;
                }
                Debug.Log(GameObject.Find("Title(Clone)"));
                break;
			case Game_State.game_setup:
                Debug.Log("state:game_setup");
                if (!GameGlobals.gameboardTriggered)
                {
                    Debug.Log("not found");
                    Title = Resources.Load<GameObject>("Title");
                    if (GameObject.Find("Title(Clone)") == null)
                        Instantiate(Title, GameObject.Find("Canvas").transform);
                    current_state = Game_State.game_title;
                }
                //else
                //{
                //    current_state = Game_State.game_start;
                //}
				break;
			case Game_State.game_start:
				//current_state = Game_State.player_a_phase;
				break;
			case Game_State.player_a_phase:
				current_state = Game_State.player_b_phase;
				break;
			case Game_State.player_b_phase:
				// For now we'll make the game one round only
				current_state = Game_State.game_over;
				break;

			default:
				Debug.Log("Should not go here.");
				break;
		}
	}
}
