using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MonoBehaviour))]
public class HandlesTest : Editor 
{
	protected virtual void OnSceneGUI()
	{
		// Handles.BeginGUI();
		Handles.DrawDottedLine(Vector3.zero, new Vector3(4, 4, 0), .5f);
		// Handles.EndGUI();
	}
}
