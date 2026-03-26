using UnityEngine;
using Utils.ClassUtility;
using System.Collections.Generic;

public class NoteManager : MonoBehaviour
{
    private static NoteManager instance;
    public static NoteManager Instance { get { return instance; } }

    private Dictionary<int, Queue<NoteObject>> lanes = new();
    private Dictionary<int, LongNote> activeLong = new();

    private Queue<ShortNote> shortPool = new Queue<ShortNote>();
    private Queue<LongNote> longPool = new Queue<LongNote>();

    public ShortNote shortPrefab;
    public LongNote longPrefab;

    public Transform poolParent;
    public int poolSize = 100;

    public float[] laneY = { 1f, -1f };
    public const float hitLineX = -6.5f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        // lane 초기화
        for (int i = 0; i < laneY.Length; i++)
            lanes[i] = new Queue<NoteObject>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (shortPrefab == null || longPrefab == null)
        {
            Debug.Log("NoteManager: Prefab이 할당되지 않았습니다.");
            return;
        }

        if (poolParent == null)
        {
            Debug.LogWarning("NoteManager: poolParent가 없어 자동 생성합니다.");
            GameObject parent = new GameObject("NotePool");
            poolParent = parent.transform;
        }

        for (int i = 0; i < poolSize; i++)
        {
            var s = Instantiate(shortPrefab, poolParent);
            s.gameObject.SetActive(false);
            shortPool.Enqueue(s);

            var l = Instantiate(longPrefab, poolParent);
            l.gameObject.SetActive(false);
            longPool.Enqueue(l);
        }
    }

    // 풀에서 가져오기
    public NoteObject Get(NoteData _data)
    {
        NoteObject note;

        if (_data.IsLong)
        {
            note = longPool.Count > 0 ? longPool.Dequeue() : Instantiate(longPrefab);
        }
        else
        {
            note = shortPool.Count > 0 ? shortPool.Dequeue() : Instantiate(shortPrefab);
        }

        note.gameObject.SetActive(true);
        return note;
    }

    // 반환
    public void Return(NoteObject _note)
    {
        _note.gameObject.SetActive(false);

        if (_note is LongNote l)
            longPool.Enqueue(l);
        else if (_note is ShortNote s)
            shortPool.Enqueue(s);
    }

    // Lane 관리
    public void Register(NoteObject _note)
    {
        lanes[_note.GetLane()].Enqueue(_note);
    }

    public void Unregister(NoteObject _note)
    {
        int lane = _note.GetLane();
        if (lanes[lane].Count > 0 && lanes[lane].Peek() == _note)
            lanes[lane].Dequeue();
    }

    // 입력 처리
    public void TryHit(int _lane)
    {
        if (lanes[_lane].Count == 0) return;
        lanes[_lane].Peek().TryHit();
    }

    public void Hold(int _lane, bool _holding)
    {
        if (activeLong.TryGetValue(_lane, out var ln))
            ln.SetHold(_holding);
    }

    public void SetActiveLongNote(int _lane, LongNote _note)
    {
        activeLong[_lane] = _note;
    }

    public void ClearActiveLongNote(int _lane)
    {
        activeLong.Remove(_lane);
    }

    public float GetLaneY(int _lane) => laneY[_lane];
}