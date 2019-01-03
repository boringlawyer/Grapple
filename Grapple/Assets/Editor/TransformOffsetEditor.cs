using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransformOffset)), CanEditMultipleObjects]
public class TransformOffsetEditor : Editor
{
    Vector3 widgetPos;
    protected virtual void OnSceneGUI()
    {
		
        TransformOffset example = (TransformOffset)target;

        EditorGUI.BeginChangeCheck();
        // Vector3 newTargetPosition = Handles.PositionHandle(example.targetPosition, Quaternion.identity);
        Vector3 newOffset;
        if (example.UseLocalCoords)
        {
    		newOffset = Handles.PositionHandle(example.LocalOffset, Quaternion.identity);
        }
        else
        {
    		newOffset = Handles.PositionHandle(example.Offset, Quaternion.identity);
        }
        //newOffset = Handles.PositionHandle(example.Offset, Quaternion.identity);
        Vector3 newScale = Handles.ScaleHandle(example.transform.localScale, newOffset, Quaternion.identity, 1);
		//Quaternion newRot = Handles.RotationHandle(example.transform.rotation, newOffset);
		if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(example, "Changed origin");
    		example.Offset = newOffset;
			Vector3 firstPosition = example.transform.position;
			example.transform.position = example.Offset;
 			Vector3 deltaScale = new Vector3(newScale.x / example.transform.localScale.x, newScale.y / example.transform.localScale.y, newScale.z / example.transform.localScale.z);
			example.transform.localScale = newScale;
			Vector3 offsetToOrigin = firstPosition - newOffset;
			example.transform.position = newOffset + Vector3.Scale(offsetToOrigin, deltaScale);
            example.Offset = Vector3.Scale(deltaScale, -offsetToOrigin) + example.Offset;
            // example.targetPosition = newTargetPosition;
            // example.Update();
        }
    }

    public override void OnInspectorGUI()
	{
		TransformOffset myTarget = (TransformOffset)target;
        Vector3 oldDisplayOffset;
        // tracks two offset values to compare them for changes
        if (myTarget.UseLocalCoords)
        {
            oldDisplayOffset = myTarget.LocalOffset;
        }
        else
        {
            oldDisplayOffset = myTarget.Offset;
        }
		Vector3 displayOffset = EditorGUILayout.Vector3Field("Offset", oldDisplayOffset);
        if (oldDisplayOffset != displayOffset)
        {
            if (myTarget.UseLocalCoords)
            {
                myTarget.Offset = myTarget.transform.position + displayOffset;
            }
            else
            {
                myTarget.Offset = displayOffset;
            }
        }
        myTarget.UseLocalCoords = EditorGUILayout.Toggle(myTarget.UseLocalCoords);
	}
}
