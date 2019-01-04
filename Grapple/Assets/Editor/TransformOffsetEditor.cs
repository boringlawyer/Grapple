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
        // Vector3 newTargetPosition = Handles.PositionHandle(example.targetPosition, Quaternion.identity);
        if (example.UseLocalCoords)
        {
    		widgetPos = Handles.PositionHandle(example.LocalOffset, Quaternion.identity);
        }
        else
        {
    		widgetPos = Handles.PositionHandle(example.Offset, Quaternion.identity);
        }
        //newOffset = Handles.PositionHandle(example.Offset, Quaternion.identity);
        Vector3 newScale = Handles.ScaleHandle(example.transform.localScale, widgetPos, Quaternion.identity, 1);
		//Quaternion newRot = Handles.RotationHandle(example.transform.rotation, newOffset);
		if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(example, "Changed origin");
    		example.Offset = widgetPos;
			Vector3 firstPosition = example.transform.position;
			example.transform.position = example.Offset;
 			Vector3 deltaScale = new Vector3(newScale.x / example.transform.localScale.x, newScale.y / example.transform.localScale.y, newScale.z / example.transform.localScale.z);
			example.transform.localScale = newScale;
			Vector3 offsetToOrigin = firstPosition - widgetPos;
			example.transform.position = widgetPos + Vector3.Scale(offsetToOrigin, deltaScale);
            //example.Offset = Vector3.Scale(deltaScale, -offsetToOrigin) + example.Offset;
            // example.targetPosition = newTargetPosition;
            // example.Update();
        }
    }

    public override void OnInspectorGUI()
	{
		TransformOffset myTarget = (TransformOffset)target;
        // tracks two offset values to compare them for changes
        Vector3 oldOffset = widgetPos;
        bool oldUseLocal = myTarget.UseLocalCoords;
        myTarget.UseLocalCoords = EditorGUILayout.Toggle("UseLocalCoords", myTarget.UseLocalCoords);        
		Vector3 displayOffset = EditorGUILayout.Vector3Field("Offset", oldOffset);
        // if (myTarget.UseLocalCoords)
        // {
        //     widgetPos = displayOffset + myTarget.transform.position;
        // }
        // else
        // {
        //     widgetPos = displayOffset;
        // }
        if (oldOffset != displayOffset)
        {
            if (myTarget.UseLocalCoords)
            {
                // myTarget.Offset = displayOffset - myTarget.transform.position;
                widgetPos = displayOffset + myTarget.transform.position;
            }
            else
            {
                // myTarget.Offset = displayOffset;
                widgetPos = displayOffset;
            }
            myTarget.Offset = displayOffset;
        }        
	}
}
