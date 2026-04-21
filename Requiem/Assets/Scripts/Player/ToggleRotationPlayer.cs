using UnityEngine;

public class ToggleRotationFreeze : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool frozen = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            frozen = !frozen;
            rb.freezeRotation = frozen;

            Debug.Log("Freeze Rotation: " + frozen);
        }
    }
}