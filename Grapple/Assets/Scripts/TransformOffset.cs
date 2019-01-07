using UnityEngine;

[ExecuteInEditMode]
public class TransformOffset : MonoBehaviour
{
    public bool useLocalCoords;
    public bool uniformDisplacement;
    public Vector3 offset = new Vector3(1f, 0f, 2f);
    public void ScaleOffset(Vector3 scaleFactor)
    {
        // example.Offset = newOffset;
        Vector3 firstPosition = transform.position;
        transform.position = offset;
        // Vector3 deltaScale = new Vector3(scaleFactor.x / transform.localScale.x, scaleFactor.y / transform.localScale.y, scaleFactor.z / transform.localScale.z);
        transform.localScale = Vector3.Scale(scaleFactor, transform.localScale);
        Vector3 offsetToOrigin = firstPosition - offset;
        Vector3 scaledVec = Vector3.Scale(offsetToOrigin, scaleFactor);
        if (uniformDisplacement)
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
        transform.position = offset + scaledVec;
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
