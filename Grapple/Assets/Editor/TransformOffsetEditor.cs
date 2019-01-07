using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransformOffset)), CanEditMultipleObjects]
public class TransformOffsetEditor : Editor
{
    // keeps track of two versions to detect changes
    Vector3 widgetPos = Vector3.zero;
    protected virtual void OnSceneGUI()
    {
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

    public override void OnInspectorGUI()
    {
        TransformOffset myTarget = (TransformOffset)target;
        Vector3 displayOffset = myTarget.Offset;
        myTarget.useLocalCoords = EditorGUILayout.Toggle("UseLocalCoords", myTarget.useLocalCoords);
        if (myTarget.useLocalCoords)
        {
            displayOffset = myTarget.Offset - myTarget.transform.position;
        }        
        displayOffset = EditorGUILayout.Vector3Field("Offset", displayOffset);
        if (myTarget.useLocalCoords)
        {
            myTarget.Offset = myTarget.transform.position + displayOffset;
        }
        else
        {
            myTarget.Offset = displayOffset;
        }
        myTarget.uniformDisplacement = EditorGUILayout.Toggle("Uniform Displacement", myTarget.uniformDisplacement);
    }
}
