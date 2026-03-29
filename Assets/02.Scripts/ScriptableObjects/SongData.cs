using UnityEngine;
using Utils.ClassUtility;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SongData", menuName = "Rhythm/SongData")]
public class SongData : ScriptableObject
{
    // Description
    public string title;
    public string artist;

    // Audio
    public int bpm;
    public int offset;
    public int[] signature;

    // Note
    public List<NoteData> notes = new List<NoteData>();
    public AudioClip clip;
    public Sprite img;

    public float BarPerSec => BarPerMilliSec * 0.001f;
    public float BeatPerSec => BarPerMilliSec / 64f;
    public int BarPerMilliSec => (int)(signature[0] / (bpm / 60f) * 1000);
    public int BeatPerMilliSec => BarPerMilliSec / 64;
}