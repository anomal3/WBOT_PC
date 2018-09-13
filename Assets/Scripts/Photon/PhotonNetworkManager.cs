using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonNetworkManager : Photon.MonoBehaviour {

    [SerializeField] private Text connectText;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject lobbyCamera;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private GameObject wall;

    // Use this for initialization
    void Start () {
        //connect ti photon server
        PhotonNetwork.ConnectUsingSettings("0.1");

	}
	
    public virtual void OnJoinedLobby()
    {
        Debug.Log("Вы подключены к Лобби");

        //Joined Room if it exist or create one
        PhotonNetwork.JoinOrCreateRoom("New", null, null);
    }


    public virtual void OnJoinedRoom()
    {
        //spawn in the player
        PhotonNetwork.Instantiate(player.name, spawnPoint.position, spawnPoint.rotation, 0);
        PhotonNetwork.Instantiate(wall.name, spawnPoint2.position, spawnPoint2.rotation, 0);
        PhotonNetwork.player.NickName = "KAKAШКА";
        //DeactivateLobby camera
        lobbyCamera.SetActive(false);
    }


	// Update is called once per frame
	void Update () {
        connectText.text = PhotonNetwork.connectionStateDetailed.ToString();
		
	}
}
