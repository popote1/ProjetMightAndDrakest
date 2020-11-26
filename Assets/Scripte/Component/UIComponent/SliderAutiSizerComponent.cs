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
        Debug.Log("reload le panel avec "+transform.childCount+" panel dedant");
        _rectTransform.sizeDelta = new Vector2(10+transform.childCount*SinglePanlSize,_rectTransform.sizeDelta.y);
        Invoke("postResize",0.1f);
    }

    private void postResize()
    {
        _rectTransform.sizeDelta = new Vector2(10+transform.childCount*SinglePanlSize,_rectTransform.sizeDelta.y);
    }
}
