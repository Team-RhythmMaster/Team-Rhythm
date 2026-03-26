using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance {  get { return instance; } }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        HandleLane(0, KeyCode.A);
        HandleLane(1, KeyCode.D);
    }

    private void HandleLane(int _lane, KeyCode _key)
    {
        // 키 눌렀을 때 판정 시도
        if (Input.GetKeyDown(_key))
            NoteManager.Instance.TryHit(_lane);

        // 롱노트 유지 여부 전달
        NoteManager.Instance.Hold(_lane, Input.GetKey(_key));
    }
}