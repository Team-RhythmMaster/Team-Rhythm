using UnityEngine;
using Utils.ClassUtility;
using System.Collections.Generic;

// 전체 곡 데이터
[CreateAssetMenu(fileName = "Beatmap", menuName = "Rhythm/Beatmap")]
public class Beatmap : ScriptableObject
{
    public float bpm;
    public AudioClip music; // 연결된 음악
    public List<Note> notes = new List<Note>();
}