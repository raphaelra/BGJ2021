using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorShiftText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI NpcText;
    [SerializeField] [Range(0f,3f)] float lerpTime = 1f;
    [SerializeField] Color[] myColors;
    int colorIndex = 0;
    float t = 0f;
    int len = 0;


    void Start()
    {
        len = myColors.Length;
    }

    
    void Update()
    {
        NpcText.color = Color.Lerp (NpcText.color, myColors[colorIndex], lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if(t>.9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0 : colorIndex;
        }
    }
}
