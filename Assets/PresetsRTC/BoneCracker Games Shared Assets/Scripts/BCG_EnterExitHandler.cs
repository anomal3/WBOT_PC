//----------------------------------------------
//            Realistic Tank Controller
//
// Copyright © 2014 - 2017 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if RTC_REWIRED
using Rewired;
#endif

/// <summary>
/// Main Enter Exit Handler for Scene.
/// </summary>
[AddComponentMenu("BoneCracker Games/Shared Assets/Enter-Exit/Main Enter Exit Handler")]
public class BCG_EnterExitHandler : MonoBehaviour {

	// Getting an Instance of Main Shared RTC Settings.
	#region RTC Settings Instance

	private RTC_Settings RTCSettingsInstance;
	private RTC_Settings RTCSettings {
		get {
			if (RTCSettingsInstance == null) {
				RTCSettingsInstance = RTC_Settings.Instance;
			}
			return RTCSettingsInstance;
		}
	}

	#endregion

	public List <GameObject> cachedMainCameras = new List<GameObject> ();
	public List<BCG_EnterExitPlayer> cachedPlayers = new List<BCG_EnterExitPlayer>();
	public List<BCG_EnterExitVehicle> cachedVehicles = new List<BCG_EnterExitVehicle>();

	private float waitTime = 5;

	[System.Serializable]
	public class BCG_FPSPlayer{

		public BCG_EnterExitPlayer FPSPlayer;
		public BCG_EnterExitVehicle currentlyInThisVehicle;

	}
		
	public BCG_FPSPlayer BCGFPSPlayer;

	#if RTC_REWIRED
	private static Player player;
	#endif

	void Start(){

		for (int i = 0; i < cachedMainCameras.Count; i++) {
			cachedMainCameras[i].SetActive (false);
		}

		#if RTC_REWIRED
		player = Rewired.ReInput.players.GetPlayer(0);
		#endif
			
	}

	void Update(){

		waitTime += Time.deltaTime;

		if (BCGFPSPlayer.currentlyInThisVehicle == null) {

			RaycastHit hit;
			Vector3 rayPosition;

			if (BCGFPSPlayer.FPSPlayer.FPSCamera)
				rayPosition = BCGFPSPlayer.FPSPlayer.FPSCamera.transform.position;
			else
				rayPosition = BCGFPSPlayer.FPSPlayer.transform.position;

			if(Physics.Raycast(rayPosition, BCGFPSPlayer.FPSPlayer.transform.forward, out hit, 3f)){

				if(hit.transform.GetComponentInParent<BCG_EnterExitVehicle>()){

					BCGFPSPlayer.FPSPlayer.showGui = true;

					switch (RTCSettings.controllerType) {

					case RTC_Settings.ControllerType.Keyboard:

						if (Input.GetKeyDown (BCG_EnterExitSettings.Instance.enterExitVehicleKB)) {

							BCG_Player_OnBCGPlayerEnteredAVehicle (BCGFPSPlayer.FPSPlayer, hit.transform.GetComponentInParent<BCG_EnterExitVehicle>());

						}

						break;

					case RTC_Settings.ControllerType.Custom:

						#if RTC_REWIRED
						if (player.GetButtonDown (BCG_EnterExitSettings.Instance.RW_enterExitVehicleKB)) {

							BCG_Player_OnBCGPlayerEnteredAVehicle (BCGFPSPlayer.FPSPlayer, hit.transform.GetComponentInParent<BCG_EnterExitVehicle>());

						}
						#endif

						break;

					}
						
				}else{

					BCGFPSPlayer.FPSPlayer.showGui = false;

				}

			}else{

				BCGFPSPlayer.FPSPlayer.showGui = false;

			}

		} else {

			switch (RTCSettings.controllerType) {

			case RTC_Settings.ControllerType.Keyboard:

				if (Input.GetKeyDown (BCG_EnterExitSettings.Instance.enterExitVehicleKB)) {

					BCG_Player_OnBCGPlayerExitedFromAVehicle (BCGFPSPlayer.FPSPlayer, BCGFPSPlayer.currentlyInThisVehicle);

				}

				break;

			case RTC_Settings.ControllerType.Custom:

				#if RTC_REWIRED
				if (player.GetButtonDown (BCG_EnterExitSettings.Instance.RW_enterExitVehicleKB)) {

					BCG_Player_OnBCGPlayerExitedFromAVehicle (BCGFPSPlayer.FPSPlayer, BCGFPSPlayer.currentlyInThisVehicle);

				}
				#endif

				break;

			}

		}

	}

	void OnEnable () {

		BCG_EnterExitPlayer.OnBCGPlayerSpawned += BCG_Player_OnBCGPlayerSpawned;
		BCG_EnterExitVehicle.OnBCGVehicleSpawned += BCG_Player_OnBCGVehicleSpawned;
		BCG_EnterExitPlayer.OnBCGPlayerEnteredAVehicle += BCG_Player_OnBCGPlayerEnteredAVehicle;
		BCG_EnterExitPlayer.OnBCGPlayerExitedFromAVehicle += BCG_Player_OnBCGPlayerExitedFromAVehicle;

	}

	void BCG_Player_OnBCGPlayerSpawned (BCG_EnterExitPlayer player){

		if(!cachedPlayers.Contains(player))
			cachedPlayers.Add (player);

		BCG_FPSPlayer newPlayer = new BCG_FPSPlayer ();

		newPlayer.FPSPlayer = player;
		newPlayer.currentlyInThisVehicle = null;

		BCGFPSPlayer = newPlayer;
		
	}

	void BCG_Player_OnBCGVehicleSpawned (BCG_EnterExitVehicle player){

		if(!cachedVehicles.Contains(player))
			cachedVehicles.Add (player);

		if (!cachedMainCameras.Contains (player.correspondingCamera)) {
			cachedMainCameras.Add (player.correspondingCamera);
			player.correspondingCamera.SetActive (false);
		}

	}

	void BCG_Player_OnBCGPlayerEnteredAVehicle (BCG_EnterExitPlayer player, BCG_EnterExitVehicle vehicle){
		
		if (waitTime < 1)
			return;

		print ("Player Named " + player.name + " has entered a vehicle named " + vehicle.name);

		player.gameObject.SetActive (false);
		player.transform.SetParent (vehicle.transform);
		player.transform.localPosition = Vector3.zero;
		player.transform.localRotation = Quaternion.identity;
		player.transform.rotation = vehicle.transform.rotation;
		BCGFPSPlayer.currentlyInThisVehicle = vehicle;

		for (int i = 0; i < cachedMainCameras.Count; i++) {

			if (cachedMainCameras [i] != vehicle.correspondingCamera) {
				cachedMainCameras [i].SetActive (false);
			} else {
				cachedMainCameras [i].SetActive (true);
				cachedMainCameras [i].SendMessage ("SetTarget", vehicle.gameObject, SendMessageOptions.DontRequireReceiver);
			}

		}

		vehicle.gameObject.SendMessage ("SetCanControl", true, SendMessageOptions.DontRequireReceiver);
		vehicle.gameObject.SendMessage ("SetEngine", true, SendMessageOptions.DontRequireReceiver);

		waitTime = 0f;

	}

	void BCG_Player_OnBCGPlayerExitedFromAVehicle (BCG_EnterExitPlayer player, BCG_EnterExitVehicle vehicle){

		if (waitTime < 1)
			return;

		print ("Player Named " + player.name + " has exited from a vehicle named " + vehicle.name);
		player.transform.SetParent (null);
		BCGFPSPlayer.currentlyInThisVehicle = null;

		if (vehicle.getOutPosition) {
			player.transform.position = vehicle.getOutPosition.position;
		} else {
			player.transform.position = vehicle.transform.position;
			player.transform.position += player.transform.right * 2f;
		}

		player.transform.rotation = vehicle.transform.rotation;
		player.gameObject.SetActive (true);

		for (int i = 0; i < cachedMainCameras.Count; i++) {
			cachedMainCameras[i].SetActive (false);
		}

		vehicle.gameObject.SendMessage ("SetCanControl", false, SendMessageOptions.DontRequireReceiver);
		vehicle.gameObject.SendMessage ("SetEngine", BCG_EnterExitSettings.Instance.keepEnginesAlive, SendMessageOptions.DontRequireReceiver);

		waitTime = 0f;

	}

	void OnDisable () {

		BCG_EnterExitPlayer.OnBCGPlayerSpawned -= BCG_Player_OnBCGPlayerSpawned;
		BCG_EnterExitVehicle.OnBCGVehicleSpawned -= BCG_Player_OnBCGVehicleSpawned;
		BCG_EnterExitPlayer.OnBCGPlayerEnteredAVehicle -= BCG_Player_OnBCGPlayerEnteredAVehicle;
		BCG_EnterExitPlayer.OnBCGPlayerExitedFromAVehicle -= BCG_Player_OnBCGPlayerExitedFromAVehicle;

	}

}
