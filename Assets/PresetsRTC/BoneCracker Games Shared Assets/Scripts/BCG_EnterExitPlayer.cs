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

/// <summary>
/// Enter Exit for FPS Player.
/// </summary>
[AddComponentMenu("BoneCracker Games/Shared Assets/Enter-Exit/Enter Exit Script For FPS Player")]
public class BCG_EnterExitPlayer : MonoBehaviour {

	public bool canControl = true;
	public bool showGui = false;

	public bool playerStartsAsInVehicle = false;
	public BCG_EnterExitVehicle vehicle;

	public Camera FPSCamera;

	public delegate void onBCGPlayerSpawned(BCG_EnterExitPlayer player);
	public static event onBCGPlayerSpawned OnBCGPlayerSpawned;

	public delegate void onBCGPlayerEnteredAVehicle(BCG_EnterExitPlayer player, BCG_EnterExitVehicle vehicle);
	public static event onBCGPlayerEnteredAVehicle OnBCGPlayerEnteredAVehicle;

	public delegate void onBCGPlayerExitedFromAVehicle(BCG_EnterExitPlayer player, BCG_EnterExitVehicle vehicle);
	public static event onBCGPlayerExitedFromAVehicle OnBCGPlayerExitedFromAVehicle;

	void Start () {

		if (!GameObject.FindObjectOfType<BCG_EnterExitHandler> ())
			enabled = false;

		FPSCamera = GetComponentInChildren<Camera> ();

		if (OnBCGPlayerSpawned != null)
			OnBCGPlayerSpawned (this);

	}

	void OnEnable () {

		if (OnBCGPlayerSpawned != null)
			OnBCGPlayerSpawned (this);

		if (playerStartsAsInVehicle)
			StartCoroutine (StartInVehicle());
	
	}

	IEnumerator StartInVehicle(){

		yield return new WaitForFixedUpdate ();

		GetIn (vehicle);

	}

	public void GetIn(BCG_EnterExitVehicle vehicle){
		
		if(OnBCGPlayerEnteredAVehicle != null)
			OnBCGPlayerEnteredAVehicle (this, vehicle);

	}

	public void GetOut(BCG_EnterExitVehicle vehicle){

		if(OnBCGPlayerExitedFromAVehicle != null)
			OnBCGPlayerExitedFromAVehicle (this, vehicle);

	}

	void OnGUI (){
		 
		if(showGui){
			GUI.Label(new Rect(Screen.width - (Screen.width/1.7f),Screen.height - (Screen.height/1.2f),800,100),"Press ''" + BCG_EnterExitSettings.Instance.enterExitVehicleKB.ToString() + "'' key to Get In");
		}

	}

}
