using UnityEngine;
using Utils.ClassUtility;

// Note 생성 시스템
public class NoteGenerator : MonoBehaviour
{
    public GameObject notePrefab;
    private Conductor conductor;
    public Transform spawnPos;

    private float[] spectrum = new float[512];
    private float energy = 0.0f;
    private float lastEnergy = 0f;

    public float threshold = 0.05f;    // 비트 감지 임계값 (이 정도 이상 변화해야 비트로 인정)
    private float cooldown = 0f;       // 지금 노트 생성 가능한 상태인지 체크하는 쿨다운 타이머
    public float spawnInterval = 0.2f; // 노트 생성 후 다음 노트까지 최소 간격 (초)

    public float speed = 3f;

    private void Start()
    {
        conductor = FindFirstObjectByType<Conductor>();
    }

    private void Update()
    {
        energy = 0f;
        cooldown -= Time.deltaTime;
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);

        // 저음 영역 (킥 느낌)
        for (int i = 0; i < 50; i++)
            energy += spectrum[i];

        // 갑자기 소리가 커지는 순간 확인
        float delta = energy - lastEnergy;

        // 비트 감지
        if (delta > threshold && cooldown <= 0f)
        {
            SpawnNote();
            cooldown = spawnInterval;
        }

        lastEnergy = energy;
    }

    private void SpawnNote()
    {
        GameObject obj = Instantiate(notePrefab, spawnPos.position, Quaternion.identity);
        NoteObject note = obj.GetComponent<NoteObject>();

        float currentTime = conductor.songTime;
        float spawnAheadTime = 2f; // 2초 뒤 도착
        note.noteTime = currentTime + spawnAheadTime;
    }
}