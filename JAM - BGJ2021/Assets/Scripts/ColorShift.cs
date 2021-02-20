using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorShift : MonoBehaviour
{
    public Image image;
    [SerializeField] [Range(0f,3f)] float lerpTime = 1f;
    [SerializeField] Color[] myColors;
    int colorIndex = 0;
    float t = 0f;
    int len = 0;


    void Start()
    {
        image = GetComponent<Image>();
        len = myColors.Length;
    }

    
    void Update()
    {
        image.color = Color.Lerp (image.color, myColors[colorIndex], lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if(t>.9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0 : colorIndex;
        }
    }
}
