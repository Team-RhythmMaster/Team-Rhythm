using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public float speed = 5f;
    public float hitLineY = -3.5f;
    public float hitThreshold = 0.5f;

    private bool isHit = false;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // 판정선 아래로 벗어나면 Miss
        if (transform.position.y < hitLineY - 1f && !isHit)
        {
            Miss();
            Destroy(gameObject);
        }
    }

    public void TryHit()
    {
        float distance = Mathf.Abs(transform.position.y - hitLineY);

        if (distance < hitThreshold)
        {
            isHit = true;
            Debug.Log("Perfect");
            Destroy(gameObject);
        }
    }

    void Miss()
    {
        Debug.Log("Miss");
    }
}