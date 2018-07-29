using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	// Use this for initialization

	public string card = "";

	public GameObject ActionManager;
	public GameObject CardQueue;

	private List<GameObject> played_cards;
	private List<int> health;
	private GameObject buidling;

	private int cards_used = 0;

	void Awake () {
		played_cards = new List<GameObject>();
		health = new List<int>();	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public bool isPlayerDead () {
		if (cards_used >= 4 && health[0] == 0 && health[1] == 0) {
			return true;
		}
		return false;
	}

	private void playCard () {
		if (Resources.Load<GameObject>("Animal/" + card) != null) {
			Debug.Log("play");
			
			played_cards.Add(ActionManager.GetComponent<DeerAppeer>().callBeast(card));
			health.Add(5);
			
			CardQueue.GetComponent<Queue>().history += " " + card;
			cards_used++;
			card = "";
		}
	}

	IEnumerator Charge (GameObject beast) {
		yield return new WaitUntil(() => !ActionManager.GetComponent<DeerAppeer>().attack);
		string name = beast.name.Remove(beast.name.Length - 7);
		
		List<GameObject> list = findValidTarget(name);
		Debug.Log(list);
		if (list.Count != 0)
			ActionManager.GetComponent<DeerAppeer>().setupAttack(beast, list[0]);
	}

	public IEnumerator checkForCards () {
		string old_name = "";
		while (played_cards.Count < 2) {
			yield return new WaitUntil(() => card != "");
			// do some checking for the card
			if (old_name != card)
				playCard();
			Debug.Log("played " + card);
			old_name = card;
		}
		yield return StartCoroutine(Charge(played_cards[0]));
		yield return StartCoroutine(Charge(played_cards[1]));
	}

	private List<GameObject> findValidTarget (string name) {
		string[] list_to_attack = GameGlobals.Attack_Map[name];
		List<GameObject> to_return = new List<GameObject>();
		foreach (string s in list_to_attack) {
			if (GameObject.Find(s + "(Clone)") != null) {
				to_return.Add(GameObject.Find(s + "(Clone)"));
				continue;
			} else if (GameObject.Find(s) != null) {
				to_return.Add(GameObject.Find(s));
				continue;
			}
		} 
		return to_return;
	}
}
