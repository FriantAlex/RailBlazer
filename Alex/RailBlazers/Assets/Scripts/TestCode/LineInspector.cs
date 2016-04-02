using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Line))]

public class LineInspector : Editor {

	//shows the lines in the inspector taking into account the transform scale and rotation
	// adds transform handels to the points for easy editing

	private void OnSceneGUI () {
		
		Line line = target as Line;
		Transform handleTransform = line.transform;
		Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation : Quaternion.identity;
		Vector3 p0 = handleTransform.transform.TransformPoint (line.p0);
		Vector3 p1 = handleTransform.transform.TransformPoint (line.p1);

		Handles.color = Color.green;
		Handles.DrawLine(line.p0, line.p1);

		//checks if the points have been moved and converts world points to local points to store rotation and position
		//adds the ability to undo changes
		EditorGUI.BeginChangeCheck();
		p0 = Handles.DoPositionHandle(p0, handleRotation);
		if (EditorGUI.EndChangeCheck()) {
			
			Undo.RecordObject (line, "Move Point");
			EditorUtility.SetDirty (line);
			line.p0 = handleTransform.InverseTransformPoint(p0);
		}
		EditorGUI.BeginChangeCheck();
		p1 = Handles.DoPositionHandle(p1, handleRotation);
		if (EditorGUI.EndChangeCheck()) {
			
			Undo.RecordObject (line, "Move Point");
			EditorUtility.SetDirty (line);
			line.p1 = handleTransform.InverseTransformPoint(p1);
		}

	}

}
