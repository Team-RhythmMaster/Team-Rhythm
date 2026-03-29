using UnityEngine;
using Utils.EnumType;
using System.Collections;

public class ShortNote : NoteObject
{
    protected override void Start()
    {
        base.Start();
    }

    public override void TryHit()
    {
        diff = Mathf.Abs(data.time - currentTime);

        if (diff <= JudgeManager.bad)
        {
            isHit = true;

            if (diff <= JudgeManager.perfect) 
                JudgeManager.Instance.Judge(JudgeType.Perfect);
            else if (diff <= JudgeManager.great) 
                JudgeManager.Instance.Judge(JudgeType.Great);
            else if (diff <= JudgeManager.good) 
                JudgeManager.Instance.Judge(JudgeType.Good);
            else 
                JudgeManager.Instance.Judge(JudgeType.Bad);

            StartCoroutine(OnHit());
        }
    }

    private IEnumerator OnHit()
    {
        spriteRenderer.sprite = noteHitSprites[spriteIndex];
        yield return new WaitForSeconds(0.1f);
        Remove();
    }
}