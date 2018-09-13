using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
       
        PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	void OnJoinLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("testRoom", new RoomOptions(), TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("RTCTank", Vector3.up * 5f, Quaternion.identity, 0);
    }




}
