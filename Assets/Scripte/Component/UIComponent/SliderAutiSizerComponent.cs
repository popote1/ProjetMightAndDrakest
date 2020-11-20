using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderAutiSizerComponent : MonoBehaviour
{
    public float SinglePanlSize;

    private RectTransform _rectTransform;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void ReSizePanel()
    {
        _rectTransform.sizeDelta = new Vector2(10+transform.childCount*SinglePanlSize,_rectTransform.sizeDelta.y);
    }
}
