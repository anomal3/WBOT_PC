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

[CustomEditor(typeof(BCG_EnterExitPlayer))]
public class BCG_EnterExitPlayerEditor : Editor {

	BCG_EnterExitPlayer prop;

	public override void OnInspectorGUI (){

		serializedObject.Update();
		prop = (BCG_EnterExitPlayer)target;

		//DrawDefaultInspector ();

		EditorGUILayout.HelpBox ("Script must be attached to root of your FPS player.", MessageType.Info);

		EditorGUILayout.Space ();

		EditorGUILayout.PropertyField (serializedObject.FindProperty("playerStartsAsInVehicle"), new GUIContent("Player Starts As In Vehicle"), false);

		if (prop.playerStartsAsInVehicle) {
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("vehicle"), new GUIContent ("Vehicle"), false);
			EditorGUI.indentLevel--;
		}

		if (!GameObject.FindObjectOfType<BCG_EnterExitHandler> ()) {

			EditorGUILayout.HelpBox ("Your scene doesn't have BCG_EnterExitHandler. In order to use enter-exit system, your scene must have BCG_EnterExitHandler.", MessageType.Error);

			if (GUILayout.Button ("Create BCG_EnterExitHandler")) {

				GameObject newBCG_EnterExitHandler = new GameObject ();
				newBCG_EnterExitHandler.transform.name = "BCG_EnterExitHandler";
				newBCG_EnterExitHandler.transform.position = Vector3.zero;
				newBCG_EnterExitHandler.transform.rotation = Quaternion.identity;
				newBCG_EnterExitHandler.AddComponent<BCG_EnterExitHandler> ();

			}

		}

		serializedObject.ApplyModifiedProperties();

		if(GUI.changed)
			EditorUtility.SetDirty(prop);

	}

}
