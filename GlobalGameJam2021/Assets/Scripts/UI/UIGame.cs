using UnityEngine;
using UnityEngine.UIElements;

public class UIGame : MonoBehaviour
{
    private static UIGame instance;

    public static UIGame Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<UIGame>();
            if (instance == null)
                Debug.LogError("Do not have UITimer in scene");
            return instance;
        }
    }

    private Label timer;
    private Label soulCounter;
    private Label torchCounter;
    private VisualElement soul;
    private VisualElement torch;

    // Start is called before the first frame update
    void Start()
    {
        var visualElement = GetComponent<UIDocument>().rootVisualElement;
        timer = visualElement.Q<Label>("TimeLeft");
        soulCounter = visualElement.Q<Label>("SoulCounter");
        soul = visualElement.Q<VisualElement>("Soul");
        torch = visualElement.Q<VisualElement>("Torch");
        torchCounter = visualElement.Q<Label>("TorchCounter");
        HideTime();
    }

    public void ShowTime()
    {
        timer.style.display = DisplayStyle.Flex;
        torch.style.display = DisplayStyle.None;
        
    }

    public void SetTime(int minute, int seconds)
    {
        timer.text = minute.ToString("00") + ":" + seconds.ToString("00");
    }

    public void HideTime()
    {
        timer.style.display = DisplayStyle.None;
        torch.style.display = DisplayStyle.Flex;
    }

    public void SetTorchCount(int count)
    {
        torchCounter.text = count.ToString();
    }

    public void SetSoulCounter(int count)
    {
        soulCounter.text = count.ToString();
    }
}