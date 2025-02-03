using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ContinueDelivery : MonoBehaviour
{
    private VisualElement root;
    private Label title, subtitle;
    private Button playButton, exitButton;

    public UnityEvent OnNextClicked;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        
        OnNextClicked ??= new UnityEvent();
        
        title = root.Q<Label>("Title");
        subtitle = root.Q<Label>("SubTitle");
        playButton = root.Q<Button>("PlayBtn");
        
        playButton.clicked += () =>
        {
            OnNextClicked?.Invoke();
            root.style.display = DisplayStyle.None;
        };
        
        root.style.display = DisplayStyle.None;
    }

    public void NextScreen(bool didLose)
    {
        root.style.display = DisplayStyle.Flex;
        if (didLose)
        {
            title.text = "You crashed!";
            subtitle.text = "You missed your target, try again with your next delivery";
            playButton.text = "Try Again";
        }
        else
        {
            title.text = "Delivered!";
            subtitle.text = "You made a successful delivery!";
            playButton.text = "Next Delivery";
        }
    }
}
