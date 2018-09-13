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
/// Stored all general shared Enter-Exit settings here.
/// </summary>
[System.Serializable]
public class BCG_EnterExitSettings : ScriptableObject {
	
	#region singleton
	public static BCG_EnterExitSettings instance;
	public static BCG_EnterExitSettings Instance{	get{if(instance == null) instance = Resources.Load("BCG_EnterExitSettings") as BCG_EnterExitSettings; return instance;}}
	#endregion

	// Unity Inputs
	public KeyCode enterExitVehicleKB = KeyCode.E;

	#if RTC_REWIRED
	// ReWired Inputs
	public string RW_enterExitVehicleKB = "EnterExitVehicle";
	#endif

	public bool keepEnginesAlive = true;

}
