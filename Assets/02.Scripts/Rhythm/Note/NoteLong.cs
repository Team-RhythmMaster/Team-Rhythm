using UnityEngine;

public class NoteLong : NoteObject
{
    //public bool isHolding = false;

    //protected override void SetPosition(float x)
    //{
    //    transform.position = new Vector3(x, transform.position.y, 0);
    //}

    //protected override void Update()
    //{
    //    base.Update();

    //    // 유지 판정
    //    if (isHolding)
    //    {
    //        if (conductor.songPosition >= data.tail)
    //        {
    //            Debug.Log("Long Success");
    //            pool.Return(gameObject);
    //        }
    //    }
    //}

    //public override void TryHit()
    //{
    //    float diff = Mathf.Abs(data.time - conductor.songPosition);

    //    if (diff <= good)
    //    {
    //        isHolding = true;
    //        Debug.Log("Long Start");
    //    }
    //}

    //public void Release()
    //{
    //    isHolding = false;
    //    Debug.Log("Long Failed");
    //    pool.Return(gameObject);
    //}
}