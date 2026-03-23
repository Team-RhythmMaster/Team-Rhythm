using UnityEngine;

// 음악 시간 관리 (DSP 기반)
public class Conductor : MonoBehaviour
{
    // 현재 음악의 시간
    public float songTime;

    void Start()
    {
        AudioManager.Instance.Play();
    }

    void Update()
    {
        songTime = AudioManager.Instance.audioSource.time;
    }
}