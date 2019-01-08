using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransformOffset)), CanEditMultipleObjects]
public class TransformOffsetEditor : Editor
{
    TransformOffset myTarget;
    Vector3 widgetPos = Vector3.zero;
    protected virtual void OnSceneGUI()
    {
        myTarget = (TransformOffset)target;
        myTarget.CreateScriptableObject();
        EditorGUI.BeginChangeCheck();
    	widgetPos = Handles.PositionHandle(myTarget.Offset, Quaternion.identity);
        Vector3 newScale = Handles.ScaleHandle(myTarget.transform.localScale, widgetPos, Quaternion.identity, 1);
		if (EditorGUI.EndChangeCheck())
        {
    		myTarget.Offset = widgetPos;
 			Vector3 deltaScale = new Vector3(newScale.x / myTarget.transform.localScale.x, newScale.y / myTarget.transform.localScale.y, newScale.z / myTarget.transform.localScale.z);
            myTarget.ScaleOffset(deltaScale);
        }
    }
    public override void OnInspectorGUI()
    {
        myTarget = (TransformOffset)target;
        myTarget.CreateScriptableObject();
        Vector3 displayOffset = myTarget.Offset;
        myTarget.UseLocalCoords = EditorGUILayout.Toggle("UseLocalCoords", myTarget.UseLocalCoords);
        if (myTarget.UseLocalCoords)
        {
            displayOffset = myTarget.Offset - myTarget.transform.position;
        }        
        displayOffset = EditorGUILayout.Vector3Field("Offset", displayOffset);
        if (myTarget.UseLocalCoords)
        {
            myTarget.Offset = myTarget.transform.position + displayOffset;
        }
        else
        {
            myTarget.Offset = displayOffset;
        }
        //myTarget.UniformDisplacement = EditorGUILayout.Toggle("Uniform Displacement", myTarget.UniformDisplacement);
    }
}
