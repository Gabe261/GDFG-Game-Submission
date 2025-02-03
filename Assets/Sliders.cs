using UnityEngine;
using UnityEngine.UIElements;

public class Sliders : MonoBehaviour
{

    private VisualElement root;
    
    private VisualElement leftWingSlider;
    private VisualElement rightWingSlider;
    
    private const float minSliderValue = -2f;  // Minimum value for input (corresponding to top -70)
    private const float maxSliderValue = 2f;   // Maximum value for input (corresponding to top 70)
    private const float minTopValue = -70f;    // Corresponding minimum top value in styling
    private const float maxTopValue = 70f;  
    
    private float leftWingInput = 0;
    private float rightWingInput = 0;
    
    private void Awake()
    {
        // Get the root of the UXML document
        root = GetComponent<UIDocument>().rootVisualElement;

        // Find the sliders by their names
        leftWingSlider = root.Q<VisualElement>("LeftWing");
        rightWingSlider = root.Q<VisualElement>("RightWing");
    }

    void Update()
    {
        leftWingSlider.style.top = CalculateSliderPosition(leftWingInput);
        rightWingSlider.style.top = CalculateSliderPosition(rightWingInput);
    }

    public void UpdateSliders(float leftInput, float rightInput)
    {
        leftWingInput = leftInput;
        rightWingInput = rightInput;
    }
    
    private float CalculateSliderPosition(float input)
    {
        // Map the input range (-2, 2) to top value range (-70, 70)
        float clampedInput = Mathf.Clamp(input, minSliderValue, maxSliderValue);
        float mappedPosition = Mathf.Lerp(minTopValue, maxTopValue, (clampedInput - minSliderValue) / (maxSliderValue - minSliderValue));

        return mappedPosition;
    }
}
