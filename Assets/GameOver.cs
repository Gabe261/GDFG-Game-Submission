using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOver : MonoBehaviour
{
    private VisualElement root;
    private Label title, subtitle;
    private Button playButton, exitButton;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        
        
        title = root.Q<Label>("Title");
        subtitle = root.Q<Label>("SubTitle");
        playButton = root.Q<Button>("PlayBtn");
        
        playButton.clicked += () =>
        {
            root.style.display = DisplayStyle.None;
            SceneManager.LoadScene("Main Scene");
        };
        
        root.style.display = DisplayStyle.None;
    }
    
    public void UpdateScreen(string titleText, string subtitleText)
    {
        root.style.display = DisplayStyle.Flex;

        title.text = "Game Over";
        subtitle.text = subtitleText;
        playButton.text = "Play Again";
        
    }
}
