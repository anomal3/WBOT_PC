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


/// <summary>
/// Enter Exit for BCG Vehicles.
/// </summary>
[AddComponentMenu("BoneCracker Games/Shared Assets/Enter-Exit/Enter Exit Script For Vehicle")]
public class BCG_EnterExitVehicle : MonoBehaviour {

	public GameObject correspondingCamera;

	public Transform getOutPosition;

	public delegate void onBCGVehicleSpawned(BCG_EnterExitVehicle player);
	public static event onBCGVehicleSpawned OnBCGVehicleSpawned;

	void Start () {

		if (!GameObject.FindObjectOfType<BCG_EnterExitHandler> ()) {
			enabled = false;
			return;
		}
			
		gameObject.SendMessage ("SetCanControl", false, SendMessageOptions.DontRequireReceiver);

		if (OnBCGVehicleSpawned != null)
			OnBCGVehicleSpawned (this);

	}

	void OnEnable(){

		if (OnBCGVehicleSpawned != null)
			OnBCGVehicleSpawned (this);

	}

}
