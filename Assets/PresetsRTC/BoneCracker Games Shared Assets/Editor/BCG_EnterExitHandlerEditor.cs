//----------------------------------------------
//            Realistic Tank Controller
//
// Copyright © 2014 - 2017 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(BCG_EnterExitHandler))]
public class BCG_EnterExitHandlerEditor : Editor {

	BCG_EnterExitHandler prop;

	[MenuItem("Tools/BoneCracker Games/Shared Assets/Enter-Exit/Edit Enter-Exit Settings", false, -100)]
	public static void OpenBCGEnterExitSettings(){

		Selection.activeObject = BCG_EnterExitSettings.Instance;

	}

	[MenuItem("Tools/BoneCracker Games/Shared Assets/Enter-Exit/Add Main Enter-Exit Handler To Scene", false)]
	public static void CreateEnterExitHandler(){

		if(GameObject.FindObjectOfType<BCG_EnterExitHandler>()){

			EditorUtility.DisplayDialog("Scene has BCG_EnterExitHandler already!", "Scene has BCG_EnterExitHandler already!", "Ok");

		}else{

			GameObject newBCG_EnterExitHandler = new GameObject ();
			newBCG_EnterExitHandler.transform.name = "BCG_EnterExitHandler";
			newBCG_EnterExitHandler.transform.position = Vector3.zero;
			newBCG_EnterExitHandler.transform.rotation = Quaternion.identity;
			newBCG_EnterExitHandler.AddComponent<BCG_EnterExitHandler> ();

			Selection.activeGameObject = newBCG_EnterExitHandler;

		}

	}

	[MenuItem("Tools/BoneCracker Games/Shared Assets/Enter-Exit/Add Enter-Exit To Vehicle", false)]
	public static void CreateEnterExitVehicle(){

		if (Selection.activeGameObject == null) {
			EditorUtility.DisplayDialog ("Select your vehicle on your scene, and then come back again!", "Select your vehicle on your scene, and then come back again!", "Ok");
			return;
		}

		if (Selection.activeGameObject.GetComponentInParent<RTC_TankController>() == null) {
			EditorUtility.DisplayDialog ("Selected vehicle doesn't have RTC_TankController!", "Selected vehicle doesn't have RTC_TankController! You must have a running vehicle before the Enter-Exit System...", "Ok");
			return;
		}

		if(Selection.activeGameObject.GetComponentInParent<BCG_EnterExitVehicle>()){

			EditorUtility.DisplayDialog("Selected vehicle has BCG_EnterExitVehicle already!", "Selected vehicle has BCG_EnterExitVehicle already!", "Ok");

		}else{

			Selection.activeGameObject.GetComponentInParent<RTC_TankController>().gameObject.AddComponent<BCG_EnterExitVehicle> ();

		}

	}

	[MenuItem("Tools/BoneCracker Games/Shared Assets/Enter-Exit/Add Enter-Exit To FPS Player", false)]
	public static void CreateEnterExitPlayer(){

		if (Selection.activeGameObject == null) {
			EditorUtility.DisplayDialog ("Select your FPS player on your scene, and then come back again!", "Select your FPS player on your scene, and then come back again!", "Ok");
			return;
		}

		if(Selection.activeGameObject.GetComponentInParent<BCG_EnterExitPlayer>()){

			EditorUtility.DisplayDialog("Selected FPS Player has BCG_EnterExitPlayer already!", "Selected FPS Player has BCG_EnterExitPlayer already!", "Ok");

		}else{
			
			Selection.activeGameObject.AddComponent<BCG_EnterExitPlayer> ();

		}

	}

	public override void OnInspectorGUI (){

		serializedObject.Update();
		prop = (BCG_EnterExitHandler)target;

		EditorGUILayout.HelpBox ("General event based enter exit system for all vehicles created by BCG.", MessageType.Info);

		//DrawDefaultInspector ();

		EditorGUILayout.PropertyField (serializedObject.FindProperty("BCGFPSPlayer"), new GUIContent("BCG FPS Player"), true);

		EditorGUILayout.LabelField ("Debug", EditorStyles.boldLabel);

		EditorGUI.BeginDisabledGroup (true);

		EditorGUILayout.PropertyField (serializedObject.FindProperty("cachedMainCameras"), new GUIContent("Cached Main Cameras"), true);
		EditorGUILayout.PropertyField (serializedObject.FindProperty("cachedPlayers"), new GUIContent("Cached Players"), true);
		EditorGUILayout.PropertyField (serializedObject.FindProperty("cachedVehicles"), new GUIContent("Cached Vehicles"), true);

		EditorGUI.EndDisabledGroup ();

		if(EditorApplication.isPlaying && prop.cachedMainCameras != null && prop.cachedMainCameras.Count == 0)
			EditorGUILayout.HelpBox ("One main camera needed at least.", MessageType.Error);

		serializedObject.ApplyModifiedProperties();

		if(GUI.changed)
			EditorUtility.SetDirty(prop);

	}

}
