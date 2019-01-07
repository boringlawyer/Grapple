using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformOffsetScriptableObject : ScriptableObject 
{
	private static Dictionary<TransformOffset, TransformOffsetScriptableObject> instances;
	public static Dictionary<TransformOffset, TransformOffsetScriptableObject> Instances
	{
		get
		{
			return instances;
		}
	}
	public bool useLocalCoords;
    public bool uniformDisplacement;
    public Vector3 offset;
	static string path = "TransformOffsetData/";
	bool registered = false;
	public void RegisterInstance(TransformOffset newInstance)
	{
		if (!registered)
		{
			instances.Add(newInstance, this);
			registered = true;
		}
	}
}
