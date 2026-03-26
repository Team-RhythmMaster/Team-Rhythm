using UnityEngine;

// 일반 노트
public class ShortNote : NoteObject
{
    public override void TryHit()
    {
        float currentTime = AudioManager.Instance.songTime + offset;
        float diff = Mathf.Abs(GetTime() - currentTime);

        if (diff <= bad)
        {
            isHit = true;

            if (diff <= perfect) 
                JudgeManager.Instance.Judge("Perfect");
            else if (diff <= great) 
                JudgeManager.Instance.Judge("Great");
            else if (diff <= good) 
                JudgeManager.Instance.Judge("Good");
            else 
                JudgeManager.Instance.Judge("Bad");

            Remove();
        }
    }
}