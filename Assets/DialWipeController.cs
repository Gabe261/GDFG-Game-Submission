using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialWipeController : MonoBehaviour
{
    bool isRunning = false;

    public float timerTime = 5.0f;

    public RectTransform dialSlice;
    public GameObject firstSlice, secondSlice, thirdsSlice, fourthSlice;

    public Sprite breezeSpriteL, gustSpriteL, stormSpriteL;
    public Color breezeColor, gustColor, stormColor;
    public List<Sprite> sprites;
    public Image[] image = new Image[6];

    public int siblingIndex;

    public UnityEvent<int> OnChangeWindType; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnChangeWindType ??= new UnityEvent<int>();
        siblingIndex = transform.GetSiblingIndex();
        StartCoroutine(DoDialWipe());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoDialWipe()
    {
        float t = 0.0f;
        dialSlice.GameObject().transform.SetSiblingIndex(1);
        isRunning = true;

        while (t < timerTime)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(360.0f, 0.0f, t / timerTime) % 360.0f;
            dialSlice.localEulerAngles = new Vector3(0.0f, 0.0f, zRotation);
            
            if (zRotation <= 0.0f)
            {
                fourthSlice.SetActive(true);

            }
            if (zRotation <= 90.0f)
            {
                thirdsSlice.SetActive(true);
            }
            if (zRotation <= 180.0f)
            {
                dialSlice.GameObject().transform.SetSiblingIndex(2);
                secondSlice.SetActive(true);
            }
            if (zRotation <= 270.0f)
            {
                firstSlice.SetActive(true);
            }
            
            yield return null;
        }
        isRunning = false;
        SpriteUpdates();
        TurnOffSections();
    }

    public void SpriteUpdates()
    {
        Sprite holdOneSprite = sprites[0];
        Sprite holdTwoSprite = sprites[1];
        
        sprites.RemoveAt(0);
        sprites.RemoveAt(0);

        sprites.Add(holdOneSprite);
        sprites.Add(holdTwoSprite);
        
        image[0].sprite = sprites[0];
        image[1].sprite = sprites[1];
        image[2].sprite = sprites[2];
        image[3].sprite = sprites[3];
        image[4].sprite = sprites[4];
        image[5].sprite = sprites[5];

        for (int i = 0; i < 6; i++)
        {
            if (image[i].sprite == breezeSpriteL)
            {
                image[i].color = breezeColor;
                image[i+1].color = breezeColor;
                OnChangeWindType.Invoke(1);
            }
            else if (image[i].sprite == gustSpriteL)
            {
                image[i].color = gustColor;
                image[i+1].color = gustColor;
                OnChangeWindType.Invoke(2);
            }
            else if (image[i].sprite == stormSpriteL)
            {
                image[i].color = stormColor;
                image[i+1].color = stormColor;
                OnChangeWindType.Invoke(0);
            }
            
            i++; //we only check every second slot, so we prematurely increment i before the loop does it itself
        }
    }

    public void TurnOffSections()
    {
        firstSlice.SetActive(false);
        secondSlice.SetActive(false);
        thirdsSlice.SetActive(false);
        fourthSlice.SetActive(false);
        
        StartCoroutine(DoDialWipe());
    }
}
