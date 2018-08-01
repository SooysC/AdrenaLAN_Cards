using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeline : MonoBehaviour {
    public bool gameStarted = false;

	public GameObject Title;
	public GameObject player_a;
	public GameObject player_b;

	public enum Game_State {game_title, game_setup, game_start, player_a_phase, player_b_phase, game_over};
	public Game_State current_state = Game_State.game_title;

	// Use this for initialization
	void Awake () {
		GameGlobals.createGameActions();
		//Instantiate(Resources.Load<GameObject>("Queue"));
	}

	void Start () {
		StartCoroutine(nextState());
	}
	
	// Update is called once per frame
	void Update () {
        //nextState();
    }

    //IEnumerator game() {
    //}

	public IEnumerator nextState () {
		switch (current_state) {
            case Game_State.game_title:
                Debug.Log("state:game_title");
				yield return new WaitForSeconds(1f);
                if (GameGlobals.gameboardTriggered)
                {
                    Debug.Log("found");
                    if (GameObject.Find("Title") != null)
                        Destroy(GameObject.Find("Title"));
                    else
                        Destroy(GameObject.Find("Title(Clone)"));
                    current_state = Game_State.game_setup;
                }
                //Debug.Log(GameObject.Find("Title(Clone)"));
                break;
			case Game_State.game_setup:
                Debug.Log("state:game_setup");
				yield return new WaitForSeconds(1f);
                if (!GameGlobals.gameboardTriggered)
                {
                    Debug.Log("not found");
                    Title = Resources.Load<GameObject>("Title");
                    if (GameObject.Find("Title(Clone)") == null)
                        Instantiate(Title, GameObject.Find("Canvas").transform);
                    current_state = Game_State.game_title;
                }
                else
                {
                    current_state = Game_State.game_start;
                }
				break;
			case Game_State.game_start:
				yield return new WaitForSeconds(1f);
				current_state = Game_State.player_a_phase;
				break;
			case Game_State.player_a_phase:
                if (!player_a.GetComponent<PlayerBehaviour>().isPlayerDead()) {
					yield return StartCoroutine(player_a.GetComponent<PlayerBehaviour>().checkForCards());
					current_state = Game_State.player_b_phase;
					Debug.Log("Player 1");
				} else
					current_state = Game_State.game_over;
				break;
			case Game_State.player_b_phase:
                if (!player_b.GetComponent<PlayerBehaviour>().isPlayerDead()) {
					yield return StartCoroutine(player_b.GetComponent<PlayerBehaviour>().checkForCards());
					current_state = Game_State.player_a_phase;
					Debug.Log("Player 2");
				} else
					current_state = Game_State.game_over;
				break;
                current_state = Game_State.game_over;
				break;
            //case Game_state.player_end_game:
            //  display end of game animation
            //  display start screen to start game again
			default:
				Debug.Log("Should not go here.");
				break;
		}
		Debug.Log(current_state);
		StartCoroutine(nextState());
	}
}
