using System;
using UnityEngine;
using Utils.ClassUtility;
using Utils.EnumType;

public class JudgeManager : MonoBehaviour
{
    private static JudgeManager instance;
    public static JudgeManager Instance { get { return instance; } }

    private UIText uiJudgement;
    private UIText uiCombo;
    private UIText uiScore;

    // 점수 데이터
    public ScoreData data;
    private int combo = 0;
    private int score = 0;

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

    // 초기화
    public void Init()
    {
        AniPreset.Instance.Join(uiJudgement.uiObject.Name);
        AniPreset.Instance.Join(uiCombo.uiObject.Name);
        AniPreset.Instance.Join(uiScore.uiObject.Name);
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

    public void Clear()
    {
        data = new ScoreData();
        data.judgeText = Enum.GetNames(typeof(JudgeType));
        data.judgeColor = new Color[3] { Color.blue, Color.yellow, Color.red };
        uiJudgement.SetText("");
        uiCombo.SetText("");
        uiScore.SetText("0");
    }

    public void SetScore()
    {
        uiJudgement.SetText(data.judgeText[(int)data.judge]);
        uiJudgement.SetColor(data.judgeColor[(int)data.judge]);
        uiCombo.SetText($"{data.combo}");
        uiScore.SetText($"{data.score}");

        AniPreset.Instance.PlayPop(uiJudgement.uiObject.Name, uiJudgement.uiObject.rect);
        AniPreset.Instance.PlayPop(uiCombo.uiObject.Name, uiCombo.uiObject.rect);
    }
}
