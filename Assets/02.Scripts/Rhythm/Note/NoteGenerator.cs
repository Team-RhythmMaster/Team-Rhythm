using UnityEngine;
using Utils.ClassUtility;
using System.Collections.Generic;

// 노트 생성 및 풀링 요청
public class NoteGenerator : MonoBehaviour
{
    // 생성할 노트 데이터 리스트 (시간 순서로 정렬)
    public List<NoteData> notes = new List<NoteData>();

    public float speed { get { return noteSpeed; } }
    private float noteSpeed = 2.5f;

    private float spawnAheadTime; // 노트가 판정선까지 이동하는 데 걸리는 시간
    private int spawnIndex = 0;   // 다음 생성할 노트 인덱스
    private float rightEdge;      // 화면 오른쪽 끝 위치
    private float spawnX;

    public void Initialize()
    {
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        spawnX = rightEdge + 2f;

        spawnAheadTime = ((spawnX - NoteManager.hitLineX) / speed) + 1.0f;
        float currentTime = AudioManager.Instance.songTime;

        while (spawnIndex < notes.Count)
        {
            if (notes[spawnIndex].time <= currentTime + spawnAheadTime)
            {
                Spawn(notes[spawnIndex]);
                spawnIndex++;
            }
            else 
                break;
        }
    }

    private void Update()
    {
        float currentTime = AudioManager.Instance.songTime;

        while (spawnIndex < notes.Count)
        {
            if (notes[spawnIndex].time <= currentTime + spawnAheadTime)
            {
                Debug.Log($"spawn 시 timeDiff: {notes[spawnIndex].time - currentTime}");
                Spawn(notes[spawnIndex]);
                spawnIndex++;
            }
            else 
                break;
        }
    }

    // 노트 리스트에 추가 (자동 정렬)
    public void AddNote(NoteData _note)
    {
        notes.Add(_note);
        notes.Sort((a, b) => a.time.CompareTo(b.time));
    }

    // 노트 생성
    private void Spawn(NoteData _data)
    {
        float y = NoteManager.Instance.GetLaneY(_data.lane);
        var note = NoteManager.Instance.Get(_data);

        note.transform.position = new Vector3(spawnX, y, 0);
        note.Init(_data, speed, this);
    }
}