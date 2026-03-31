using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils.EnumType;

public class HitEffect : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private RectTransform rect;
    private Image image;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rect = GetComponent<RectTransform>();
    }

    public void Play(Vector2 pos, JudgeType type)
    {
        rect.anchoredPosition = pos;

        transform.DOKill();
        transform.localScale = Vector3.one;

        image.color = GetColor(type);

        Sequence seq = DOTween.Sequence();

        if (type == JudgeType.Perfect)
        {
            // 강한 타격
            seq.Append(transform.DOScale(1.8f, 0.1f));
            seq.Join(image.DOFade(0f, 0.3f));
        }
        else if (type == JudgeType.Good)
        {
            // 중간 타격
            seq.Append(transform.DOScale(1.3f, 0.1f));
            seq.Join(image.DOFade(0f, 0.25f));
        }
        else
        {
            // Miss
            seq.Append(transform.DOScale(0.8f, 0.1f));
            seq.Join(image.DOFade(0f, 0.2f));
        }

        seq.OnComplete(() => gameObject.SetActive(false));
    }

    private Color GetColor(JudgeType _type)
    {
        switch (_type)
        {
            case JudgeType.Perfect:
                return Color.yellow;
            case JudgeType.Great:
                return Color.green;
            case JudgeType.Good:
                return Color.blue;
            case JudgeType.Bad:
                return Color.gray;
            default:
                return Color.red;
        }
    }
}
