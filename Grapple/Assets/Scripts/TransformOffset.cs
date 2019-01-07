using UnityEngine;

[ExecuteInEditMode]
public class TransformOffset : MonoBehaviour
{
    TransformOffsetScriptableObject offsetSO;
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
    public void ScaleOffset(Vector3 scaleFactor)
    {
        // example.Offset = newOffset;
        Vector3 firstPosition = transform.position;
        transform.position = Offset;
        // Vector3 deltaScale = new Vector3(scaleFactor.x / transform.localScale.x, scaleFactor.y / transform.localScale.y, scaleFactor.z / transform.localScale.z);
        transform.localScale = Vector3.Scale(scaleFactor, transform.localScale);
        Vector3 offsetToOrigin = firstPosition - Offset;
        Vector3 scaledVec = Vector3.Scale(offsetToOrigin, scaleFactor);
        if (UniformDisplacement)
        {
            if (scaleFactor.x == 1)
            {
                //scaledVec.x = scaledVec.y;
                scaledVec = new Vector3(scaledVec.y, scaledVec.y, scaledVec.z);
            }
            else if (scaleFactor.y == 1)
            {
                //scaledVec.y = scaledVec.x;
                scaledVec = new Vector3(scaledVec.x, scaledVec.x, scaledVec.z);
            }
        }
        transform.position = Offset + scaledVec;
        print(scaledVec);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ScaleOffset(new Vector3(1.1f, 1, 1));
        }
    }
}
