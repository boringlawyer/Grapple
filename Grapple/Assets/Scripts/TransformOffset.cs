using UnityEngine;

[ExecuteInEditMode]
public class TransformOffset : MonoBehaviour
{
    public Vector3 Offset 
    { 
        get 
        {
            if (useLocalCoords)
            {
                return offset + transform.position;
            }
            else
            {
                return offset;
            }
        } 
        set 
        {
            if (useLocalCoords)
            {
                offset = value - transform.position;
            }
            else
            {
                offset = value;
            }
        }
    }
    public Vector3 LocalOffset
    {
        get
        {
            return offset + transform.position;
        }
    }
    [SerializeField]
    private Vector3 offset = new Vector3(1f, 0f, 2f);
    [SerializeField]
    bool useLocalCoords;
    public bool UseLocalCoords
    {
        get
        {
            return useLocalCoords;
        }
        set
        {
            useLocalCoords = value;
        }
    }
    void ScaleOffset(Vector3 scaleFactor)
    {
        // example.Offset = newOffset;
        Vector3 firstPosition = transform.position;
        transform.position = Offset;
        // Vector3 deltaScale = new Vector3(scaleFactor.x / transform.localScale.x, scaleFactor.y / transform.localScale.y, scaleFactor.z / transform.localScale.z);
        transform.localScale = Vector3.Scale(scaleFactor, transform.localScale);
        Vector3 offsetToOrigin = firstPosition - Offset;
        transform.position = Offset + Vector3.Scale(offsetToOrigin, scaleFactor);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ScaleOffset(new Vector3(1.1f, 1, 1));
        }
    }
}
