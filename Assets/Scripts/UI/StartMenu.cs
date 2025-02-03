using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.Events;

public class StartMenu : MonoBehaviour
{
    private VisualElement root;
    private Label title;
    private Button playButton;

    [SerializeField] private string titleName;
    public UnityEvent OnPlayClicked;
    
    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        
        OnPlayClicked ??= new UnityEvent();
        
        title = root.Q<Label>("Title");
        playButton = root.Q<Button>("PlayBtn");
        
        title.text = titleName;
        playButton.clicked += () => { OnPlayClicked?.Invoke(); };
    }

    public void Show()
    {
        root.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        root.style.display = DisplayStyle.None;
    }
}
