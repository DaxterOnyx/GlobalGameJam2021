using UnityEngine;
using UnityEngine.UIElements;

public class UITimer : MonoBehaviour
{
    private static UITimer instance;

    public static UITimer Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<UITimer>();
            if (instance == null)
                Debug.LogError("Do not have UITimer in scene");
            return instance;
        }
    }

    private Label label;

    // Start is called before the first frame update
    void Start()
    {
        label = GetComponent<UIDocument>().rootVisualElement.Q<Label>("TimeLeft");
        HideTime();
    }

    public void ShowTime()
    {
        label.style.display = DisplayStyle.Flex;
    }

    public void SetTime(int minute, int seconds)
    {
        label.text = minute.ToString("00") + ":" + seconds.ToString("00");
    }

    public void HideTime()
    {
        label.style.display = DisplayStyle.None;
    }
}