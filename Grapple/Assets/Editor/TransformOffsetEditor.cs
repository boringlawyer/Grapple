using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransformOffset)), CanEditMultipleObjects]
public class TransformOffsetEditor : Editor
{
    // keeps track of two versions to detect changes
    Vector3 widgetPos = Vector3.zero;
    Vector3 oldWidgetPos = Vector3.zero;
    protected virtual void OnSceneGUI()
    {
		oldWidgetPos = widgetPos;
        TransformOffset example = (TransformOffset)target;
        EditorGUI.BeginChangeCheck();
    	widgetPos = Handles.PositionHandle(example.Offset, Quaternion.identity);
        Vector3 newScale = Handles.ScaleHandle(example.transform.localScale, widgetPos, Quaternion.identity, 1);
		if (EditorGUI.EndChangeCheck())
        {
    		example.Offset = widgetPos;
 			Vector3 deltaScale = new Vector3(newScale.x / example.transform.localScale.x, newScale.y / example.transform.localScale.y, newScale.z / example.transform.localScale.z);
            example.ScaleOffset(deltaScale);
        }
    }

    // public override void OnInspectorGUI()
	// {
	// 	TransformOffset myTarget = (TransformOffset)target;
    //     // tracks two offset values to compare them for changes
    //     Vector3 oldOffset = widgetPos;
    //     bool oldUseLocal = myTarget.UseLocalCoords;
    //     myTarget.UseLocalCoords = EditorGUILayout.Toggle("UseLocalCoords", myTarget.UseLocalCoords);        
	// 	Vector3 displayOffset = EditorGUILayout.Vector3Field("Offset", oldOffset);
    //     // if (myTarget.UseLocalCoords)
    //     // {
    //     //     widgetPos = displayOffset + myTarget.transform.position;
    //     // }
    //     // else
    //     // {
    //     //     widgetPos = displayOffset;
    //     // }
    //     if (oldOffset != displayOffset)
    //     {
    //         if (myTarget.UseLocalCoords)
    //         {
    //             // myTarget.Offset = displayOffset - myTarget.transform.position;
    //             widgetPos = displayOffset + myTarget.transform.position;
    //         }
    //         else
    //         {
    //             // myTarget.Offset = displayOffset;
    //             widgetPos = displayOffset;
    //         }
    //         myTarget.Offset = displayOffset;
    //     }        
	// }

    public override void OnInspectorGUI()
    {
        TransformOffset myTarget = (TransformOffset)target;
        Vector3 displayOffset = myTarget.Offset;
        myTarget.useLocalCoords = EditorGUILayout.Toggle("UseLocalCoords", myTarget.useLocalCoords);
        if (myTarget.useLocalCoords)
        {
            displayOffset = myTarget.Offset - myTarget.transform.position;
        }        
        // Vector3 offsetInput = EditorGUILayout.Vector3Field("Offset", myTarget.Offset);
        displayOffset = EditorGUILayout.Vector3Field("Offset", displayOffset);
        if (myTarget.useLocalCoords)
        {
            //offsetInput = myTarget.Offset - myTarget.transform.position;
            myTarget.Offset = myTarget.transform.position + displayOffset;
        }
        // if (myTarget.useLocalCoords) 
        // {
        //     myTarget.Offset += myTarget.transform.position;
        // }
        else
        {
            myTarget.Offset = displayOffset;
        }
    }
}
