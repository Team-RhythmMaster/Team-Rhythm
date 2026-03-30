using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    private Text text;
    public UIObject uiObject;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void SetText(string _text)
    {
        text.text = _text;
    }

    public void SetColor(Color color)
    {
        text.color = color;
    }

    public void ChangeText()
    {
        UIManager.Instance.find.Invoke(uiObject.Name);
    }
}
