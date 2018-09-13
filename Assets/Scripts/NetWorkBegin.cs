using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetWorkBegin : NetworkBehaviour {

	
	
	void Update () {
		if(!isLocalPlayer)
        { return; }
	}
}
