using UnityEngine;
using System.Collections.Generic;

// Note 재사용 시스템
public class NotePool : MonoBehaviour
{
    public GameObject notePrefab;
    public int poolSize = 100;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(notePrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        // 부족하면 추가 생성
        return Instantiate(notePrefab);
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}