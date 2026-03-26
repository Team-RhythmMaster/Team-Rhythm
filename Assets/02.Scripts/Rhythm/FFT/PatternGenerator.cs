using UnityEngine;
using Utils.ClassUtility;
using System.Collections.Generic;

// 비트 정보를 기반으로 실제 노트 패턴 생성
public class PatternGenerator : MonoBehaviour
{
    private NoteGenerator noteGenerator;
    private BeatAnalyzer beatAnalyzer;

    private Dictionary<int, float> lastLaneTime = new Dictionary<int, float>();
    private float noteSpawnDelay = 6f;

    private void Awake()
    {
        noteGenerator = FindAnyObjectByType<NoteGenerator>();
        beatAnalyzer = FindAnyObjectByType<BeatAnalyzer>();
    }

    private void Start()
    {
        for (int i = 0; i < NoteManager.Instance.laneY.Length; i++)
            lastLaneTime[i] = -999f;
    }

    private void Update()
    {
        // Beat 기반 Note 자동 생성
        if (beatAnalyzer.IsOnset())
        {
            float time = AudioManager.Instance.songTime;
            Generate(time);
        }
    }

    // 비트 발생 시 호출 → 노트 생성
    public void Generate(float _rawTime)
    {
        float time = beatAnalyzer.Quantize(_rawTime);
        time += noteSpawnDelay;

        int lane = SelectLane();

        if (time - lastLaneTime[lane] < beatAnalyzer.GetBeatInterval() * 0.5f)
            return;

        NoteData note = new NoteData();
        note.time = time;
        note.lane = lane;

        if (Random.value < 0.2f)
        {
            float length = beatAnalyzer.GetBeatInterval() * Random.Range(1f, 2f);
            note.endTime = time + length;

            lastLaneTime[lane] = note.endTime;
        }
        else
        {
            note.endTime = time;
            lastLaneTime[lane] = time;
        }

        noteGenerator.AddNote(note);
    }

    // 자연스러운 레인 선택
    int SelectLane()
    {
        return Random.Range(0, NoteManager.Instance.laneY.Length);
    }
}
