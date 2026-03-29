using UnityEngine;
using Utils.EnumType;

public class JudgeManager : MonoBehaviour
{
    private static JudgeManager instance;
    public static JudgeManager Instance { get { return instance; } }

    // 판정 범위
    public const float perfect = 0.05f;
    public const float great = 0.1f;
    public const float good = 0.15f;
    public const float bad = 0.2f;
    public const float miss = 0.25f;

    // 판정별 점수
    public const int perfectScore = 1000;
    public const int greatScore = 500;
    public const int goodScore = 250;
    public const int badScore = 100;
    public const int missScore = 0;

    public int combo = 0;
    public int score = 0;

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

    // 판정 결과 처리
    public void Judge(JudgeType _result)
    {
        switch (_result)
        {
            case JudgeType.Perfect:
                combo++;
                score += perfectScore;
                break;
            case JudgeType.Great:
                combo++;
                score += greatScore;
                break;
            case JudgeType.Good:
                combo++;
                score += goodScore;
                break;
            case JudgeType.Bad:
                combo = 0;
                score += badScore;
                break;
            case JudgeType.Miss:
                combo = missScore;
                break;
        }

        UIManager.Instance.ShowJudge(_result);
    }
}
