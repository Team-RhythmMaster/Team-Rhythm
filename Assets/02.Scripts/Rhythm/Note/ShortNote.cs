using UnityEngine;
using Utils.EnumType;

public class ShortNote : NoteObject
{
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

            Remove();
        }
    }
}