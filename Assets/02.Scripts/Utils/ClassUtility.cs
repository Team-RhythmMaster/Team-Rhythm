using System;
using UnityEngine;
using Utils.EnumType;

namespace Utils.ClassUtility
{
    // Note 정보
    [Serializable]
    public class NoteData
    {
        public bool IsLong => endTime > time; // longNote 여부 확인
        public int lane;      // 레인 위치
        public float speed;   // 이동 속도
        public float time;    // 판정선 도착시간
        public float endTime; // longNote 끝시간
    }

    // Score 정보
    [Serializable]
    public class ScoreData
    {
        public string[] judgeText;
        public Color[] judgeColor;
        public JudgeType judge;

        public int combo;
        public int score;

        public int perfect;
        public int great;
        public int good;
        public int bad;
        public int miss;
    }
}