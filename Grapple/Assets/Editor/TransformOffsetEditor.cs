using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransformOffset)), CanEditMultipleObjects]
public class TransformOffsetEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
		
        TransformOffset example = (TransformOffset)target;

        EditorGUI.BeginChangeCheck();
        // Vector3 newTargetPosition = Handles.PositionHandle(example.targetPosition, Quaternion.identity);
		Vector3 newOffset = Handles.PositionHandle(example.Offset, Quaternion.identity);
        Vector3 newScale = Handles.ScaleHandle(example.transform.localScale, newOffset, Quaternion.identity, 1);
		//Quaternion newRot = Handles.RotationHandle(example.transform.rotation, newOffset);
		if (EditorGUI.EndChangeCheck())
        {
             Undo.RecordObject(example, "Changed origin");
			 example.Offset = newOffset;
			 Vector3 firstPosition = example.transform.position;
			 example.transform.position = newOffset;
 			 Vector3 deltaScale = new Vector3(newScale.x / example.transform.localScale.x, newScale.y / example.transform.localScale.y, newScale.z / example.transform.localScale.z);
			 example.transform.localScale = newScale;
			 Vector3 offsetToOrigin = firstPosition - newOffset;
			 example.transform.position = newOffset + Vector3.Scale(offsetToOrigin, deltaScale);
            // example.targetPosition = newTargetPosition;
            // example.Update();
        }
    }
}
