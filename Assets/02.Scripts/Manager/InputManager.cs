using UnityEngine;

public class InputManager : MonoBehaviour
{
    public LayerMask noteLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DetectHit();
        }
    }

    void DetectHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(Vector2.zero, Vector2.up, 10f, noteLayer);

        if (hit.collider != null)
        {
            hit.collider.GetComponent<NoteObject>().TryHit();
        }
    }
}