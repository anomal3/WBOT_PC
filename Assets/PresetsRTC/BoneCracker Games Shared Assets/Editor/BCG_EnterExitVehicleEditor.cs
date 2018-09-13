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

[CustomEditor(typeof(BCG_EnterExitVehicle))]
public class BCG_EnterExitVehicleEditor : Editor {

	BCG_EnterExitVehicle prop;

	public override void OnInspectorGUI (){

		serializedObject.Update();
		prop = (BCG_EnterExitVehicle)target;

		//DrawDefaultInspector ();

		if (!GameObject.FindObjectOfType<BCG_EnterExitHandler> ()) {

			EditorGUILayout.HelpBox ("Your scene doesn't have BCG_EnterExitHandler. In order to use enter-exit system, your scene must have BCG_EnterExitHandler.", MessageType.Error);

			if (GUILayout.Button ("Create BCG_EnterExitHandler")) {

				GameObject newBCG_EnterExitHandler = new GameObject ();
				newBCG_EnterExitHandler.transform.name = "BCG_EnterExitHandler";
				newBCG_EnterExitHandler.transform.position = Vector3.zero;
				newBCG_EnterExitHandler.transform.rotation = Quaternion.identity;
				newBCG_EnterExitHandler.AddComponent<BCG_EnterExitHandler> ();

			}

		} else {

			prop.correspondingCamera = (GameObject)EditorGUILayout.ObjectField ("Corresponding Camera", prop.correspondingCamera, typeof(GameObject), true);
			EditorGUILayout.PropertyField (serializedObject.FindProperty("getOutPosition"), new GUIContent("Get Out Position"), false);

		}
			
		serializedObject.ApplyModifiedProperties();

		if(GUI.changed)
			EditorUtility.SetDirty(prop);

	}

}
