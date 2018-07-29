using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	// Use this for initialization

	public string card = "Deer";

	public GameObject ActionManager;
	private List<GameObject> played_cards;
	private List<int> health;

	void Awake () {
		played_cards = new List<GameObject>();
		health = new List<int>();	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			StartCoroutine(Charge(played_cards[0]));
			StartCoroutine(Charge(played_cards[1]));
		}
	}

	private void playCard () {
		if (Resources.Load<GameObject>("Animal/" + card) != null) {
			Debug.Log("play");
			played_cards.Add(ActionManager.GetComponent<DeerAppeer>().callBeast(card));
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

	IEnumerator checkForCards () {
		while (true) {
			yield return new WaitForSeconds(1f);

			// do some checking for the card
			playCard();

			yield return new WaitUntil(() => played_cards.Count < 2);
		}
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
