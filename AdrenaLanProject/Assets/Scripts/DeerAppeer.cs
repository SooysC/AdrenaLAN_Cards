using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAppeer : MonoBehaviour {

	private GameObject target;
	private GameObject beast;
	public GameObject board;
	public Vector3 prevpos;
	public bool attack = false;
	public string piece = "";
	public Queue<GameObject> toOrder;
	public Queue<GameObject> toAttack;
	public Queue<Vector3> prevPos;

	void Start () {    
		toAttack = new Queue<GameObject>();    
		toOrder = new Queue<GameObject>();
		prevPos = new Queue<Vector3>();
    }
	
	// Update is called once per frame
	void Update () {
        if (attack) {
            Attack();
        }
    }

	public GameObject callBeast (string name) {
		// possibly animate the beast onto the scene
		Debug.Log("ROR");
		GameObject res = Resources.Load<GameObject>("Animal/" + name);
		Instantiate(res, board.transform);
		return GameObject.Find(name + "(Clone)");
	}

	public void Attack () {
		beast = toOrder.Peek();
		target = toAttack.Peek();
		Vector3 path = target.transform.position - beast.transform.position;
        beast.GetComponent<Rigidbody>().velocity = Vector3.Lerp(beast.GetComponent<Rigidbody>().velocity, path, 0.1f);
        Quaternion targetRotation = Quaternion.LookRotation(path);
        beast.transform.rotation = Quaternion.Lerp(beast.transform.rotation, targetRotation, 0.2f);

		if (path.magnitude <= 0.1){
			beast.transform.position = prevPos.Dequeue();
			toOrder.Dequeue();
			toAttack.Dequeue();
			attack = toAttack.Count != 0;
			Debug.Log("Too close for comfort");
		}
	}

	public void setupAttack (GameObject beast_, GameObject target_) {
		toOrder.Enqueue(beast_);
		toAttack.Enqueue(target_);
		prevPos.Enqueue(beast_.transform.position);
		attack = true;
	}
}
