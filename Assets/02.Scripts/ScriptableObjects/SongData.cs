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

    public List<NoteData> notes = new List<NoteData>();
    public AudioClip clip;
    public Sprite img;
}