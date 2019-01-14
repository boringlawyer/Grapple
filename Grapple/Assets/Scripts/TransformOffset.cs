using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class TransformOffset : MonoBehaviour, ICustomDuplicate
{
    public TransformOffsetScriptableObject offsetSO;
    public bool UseLocalCoords
    {
        get
        {
            return offsetSO.useLocalCoords;
        }
        set
        {
            offsetSO.useLocalCoords = value;
        }
    }
    public bool UniformDisplacement
    {
        get
        {
            return offsetSO.uniformDisplacement;
        }
        set
        {
            offsetSO.uniformDisplacement = value;
        }
    }
    public Vector3 Offset
    {
        get
        {
            return offsetSO.offset;
        }
        set
        {
            offsetSO.offset = value;
        }

    }
    void OnEnable()
    {
        CreateScriptableObject();
    }
    void Start()
    {
        if (offsetSO == null)
        {
            offsetSO = Resources.Load("TransformOffsetData/" + gameObject.name) as TransformOffsetScriptableObject;
        }
    }
    public void ScaleOffset(Vector3 scaleFactor)
    {
        // example.Offset = newOffset;
        Vector3 firstPosition = transform.position;
        transform.position = Offset;
        // Vector3 deltaScale = new Vector3(scaleFactor.x / transform.localScale.x, scaleFactor.y / transform.localScale.y, scaleFactor.z / transform.localScale.z);
        transform.localScale = Vector3.Scale(scaleFactor, transform.localScale);
        Vector3 offsetToOrigin = firstPosition - Offset;
        //Vector3 scaledVec = Vector3.Scale(offsetToOrigin, scaleFactor);
        Vector3 scaledVec = Vector3.zero;
        if (UniformDisplacement)
        {
            if (scaleFactor.x == 1)
            {
                //scaledVec.x = scaledVec.y;
                //scaledVec = new Vector3(scaledVec.y, scaledVec.y, scaledVec.z);
                scaledVec = offsetToOrigin * scaleFactor.y;
            }
            else if (scaleFactor.y == 1)
            {
                //scaledVec.y = scaledVec.x;
                //scaledVec = new Vector3(scaledVec.x, scaledVec.x, scaledVec.z);
                scaledVec = offsetToOrigin * scaleFactor.x;
            }
        }
        else
        {
            scaledVec = Vector3.Scale(offsetToOrigin, scaleFactor);
        }
        transform.position = Offset + scaledVec;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ScaleOffset(new Vector3(1.1f, 1f, 1));
        }
    }

    public void OnDuplicate()
    {
        offsetSO = null;
        CreateScriptableObject();
    }

    public void CreateScriptableObject()
    {
        if (offsetSO == null && Resources.Load("Assets/Resources/TransformOffsetData/" + gameObject.name) == null)
        {
            offsetSO = ScriptableObject.CreateInstance<TransformOffsetScriptableObject>();
            AssetDatabase.CreateAsset(offsetSO, "Assets/Resources/TransformOffsetData/" + gameObject.name + ".asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
        }
    }

    void OnDisable()
    {
        string path = "Assets/Resources/TransformOffsetData/" + gameObject.name + ".asset";
        bool success = FileUtil.DeleteFileOrDirectory(path);
        if (success)
        {
            print("Success");
        }
        else
        {
            print("Fail");
        }
    }

}
