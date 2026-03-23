using UnityEngine;
using Utils.ClassUtility;

// Note 데이터를 기반으로 렌더링 및 노트의 위치 계산
public abstract class NoteObject : MonoBehaviour
{
    private Conductor conductor;

    public float noteTime;
    public float speed = 3f;

    private bool isHit = false;

    // 판정 범위
    public float perfect = 0.05f;
    public float good = 0.1f;
    public float miss = 0.2f;

    void Start()
    {
        conductor = FindFirstObjectByType<Conductor>();
    }

    void Update()
    {
        float currentTime = conductor.songTime;
        float x = (noteTime - currentTime) * speed;

        transform.position = new Vector3(x, transform.position.y, 0);
        CheckMiss(currentTime);
    }

    // 자동 Miss 처리
    void CheckMiss(float currentTime)
    {
        if (!isHit && currentTime - noteTime > miss)
        {
            Debug.Log("Miss");
            Destroy(gameObject);
        }
    }

    // 입력 판정
    public void TryHit()
    {
        float currentTime = conductor.songTime;

        float diff = Mathf.Abs(noteTime - currentTime);

        if (diff <= perfect)
        {
            Debug.Log("Perfect");
            Hit();
        }
        else if (diff <= good)
        {
            Debug.Log("Good");
            Hit();
        }
        else
        {
            Debug.Log("Miss");
        }
    }

    void Hit()
    {
        isHit = true;
        Destroy(gameObject);
    }
}