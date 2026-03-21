using UnityEngine;

// Note 생성 및 오디오 분석을 담당하는 스크립트
public class NoteGenerator : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject notePrefab;
    public Transform spawnPoint;

    public enum Difficulty { Easy, Normal, Hard }
    public Difficulty difficulty = Difficulty.Normal;

    private float[] spectrum = new float[512];
    private float lastEnergy = 0f;

    private float spawnCooldown = 0f;

    void Update()
    {
        AnalyzeAudio();
    }

    void AnalyzeAudio()
    {
        // GetSpectrumData를 사용하여 오디오의 주파수 스펙트럼을 분석
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);

        float energy = 0f;

        // 저음~중음 영역 (비트 감지)
        for (int i = 0; i < 50; i++)
        {
            energy += spectrum[i];
        }

        float threshold = GetThreshold();

        // 피크 감지 (비트 순간)
        if (energy > lastEnergy * threshold && spawnCooldown <= 0f)
        {
            SpawnNote();
            spawnCooldown = GetCooldown();
        }

        lastEnergy = energy;
        spawnCooldown -= Time.deltaTime;
    }

    void SpawnNote()
    {
        Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
    }

    float GetThreshold()
    {
        switch (difficulty)
        {
            case Difficulty.Easy: return 1.5f;
            case Difficulty.Normal: return 1.3f;
            case Difficulty.Hard: return 1.1f;
        }
        return 1.3f;
    }

    float GetCooldown()
    {
        switch (difficulty)
        {
            case Difficulty.Easy: return 0.5f;
            case Difficulty.Normal: return 0.3f;
            case Difficulty.Hard: return 0.15f;
        }
        return 0.3f;
    }
}
