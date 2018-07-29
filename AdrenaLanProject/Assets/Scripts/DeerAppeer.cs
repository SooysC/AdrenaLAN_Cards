using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAppeer : MonoBehaviour {

	public GameObject[] Homes;
    public int Target_Destination;
	public GameObject Deer;
	public GameObject board;
    public bool run = false;
	private bool set = false;

	void Start () {        
    	StartCoroutine(generateDeer());
    }
	
	// Update is called once per frame
	void Update () {
        if (set) {
            GoIntoHouse();
        }
    }

	public void GoIntoHouse () {
		GameObject d = GameObject.Find("Deer(Clone)");
		Vector3 path = Homes[Target_Destination].transform.position - d.transform.position;
        d.GetComponent<Rigidbody>().velocity = Vector3.Lerp(d.GetComponent<Rigidbody>().velocity, path, 0.1f);
        Quaternion targetRotation = Quaternion.LookRotation(path);
        d.transform.rotation = Quaternion.Lerp(d.transform.rotation, targetRotation, 0.2f);

		if (path.magnitude <= 0.001){
			//set = false;
			//Destroy(GameObject.Find("Deer"));
		}
	}

    public IEnumerator generateDeer() {
        yield return new WaitUntil(() => run);
		Instantiate(Deer, board.transform);
		yield return new WaitForSeconds(3f);
		set = true;
    }
}
