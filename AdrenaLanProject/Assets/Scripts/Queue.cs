using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Queue : NetworkBehaviour {
    [SyncVar]
    public string history = "";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RpcSyncVarWithClients(history);
        Debug.Log(history);
	}
    [ClientRpc]
    void RpcSyncVarWithClients(string varToSync)
    {
        if (!history.Contains(varToSync))
            history = history + " " + varToSync;
    }
}