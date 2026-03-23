using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.ClassUtility;
using Utils.GameDefinitions;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance {  get { return instance; } }

    public GameObject[] keyEffects = new GameObject[4];
    private Conductor conductor;
    private Judgement judgement;
    private Sync sync;

    public LayerMask noteLayer;
    public Vector2 mousePos;

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

    private void Start()
    {
        //foreach (var effect in keyEffects)
        //{
        //    effect.gameObject.SetActive(false);
        //}

        conductor = FindFirstObjectByType<Conductor>();
        judgement = FindFirstObjectByType<Judgement>();
        sync = FindFirstObjectByType<Sync>();
    }

    private void Update()
    {
        //if (GameManager.Instance.state == GameState.Edit)
        //    mousePos = Mouse.current.position.ReadValue();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HitNote();
        }
    }

    void HitNote()
    {
        NoteObject[] notes = FindObjectsByType<NoteObject>(FindObjectsSortMode.None);

        if (notes.Length == 0) return;

        // 가장 가까운 노트 찾기
        NoteObject closest = notes
            .OrderBy(n => Mathf.Abs(n.noteTime - conductor.songTime))
            .First();

        closest.TryHit();
    }

    public void OnNoteLine0(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            judgement.Judge(0);
            keyEffects[0].gameObject.SetActive(true);
        }
        else if (context.canceled)
        {
            judgement.CheckLongNote(0);
            keyEffects[0].gameObject.SetActive(false);
        }
    }
    public void OnNoteLine1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            judgement.Judge(1);
            keyEffects[1].gameObject.SetActive(true);
        }
        else if (context.canceled)
        {
            judgement.CheckLongNote(1);
            keyEffects[1].gameObject.SetActive(false);
        }
    }
    public void OnNoteLine2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            judgement.Judge(2);
            keyEffects[2].gameObject.SetActive(true);
        }
        else if (context.canceled)
        {
            judgement.CheckLongNote(2);
            keyEffects[2].gameObject.SetActive(false);
        }
    }
    public void OnNoteLine3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            judgement.Judge(3);
            keyEffects[3].gameObject.SetActive(true);
        }
        else if (context.canceled)
        {
            judgement.CheckLongNote(3);
            keyEffects[3].gameObject.SetActive(false);
        }
    }
}