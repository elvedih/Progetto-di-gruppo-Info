using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;


    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        float pixelsPerUnit = 32f;
        Vector3 pos = target.transform.position;
        pos.x = Mathf.Round(pos.x * pixelsPerUnit) / pixelsPerUnit;
        pos.y = Mathf.Round(pos.y * pixelsPerUnit) / pixelsPerUnit;
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
