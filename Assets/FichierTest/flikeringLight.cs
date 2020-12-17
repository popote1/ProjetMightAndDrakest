
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class flikeringLight : MonoBehaviour
{
    public Light Light;
    public float MinIntencity=0.2f;
    public float MaxIntencity=0.5f;
    public float MinTime=0.2f;
    public float MaxTime=0.8f;

    private float _newIntencity;
    private float _oldIntecity;
    private float _newTime=0.5f;
    private float _timer=0;

    private void Start()
    {
        _newIntencity = Light.intensity;
    }

    private void Update()
    {
        Light.intensity = Mathf.Lerp(Light.intensity, _newIntencity, 0.01f);
        _timer += Time.deltaTime;
        if (_timer >= _newTime)
        {
            _oldIntecity = _newIntencity;
            _newIntencity= Random.Range(MinIntencity,MaxIntencity);
            _newTime = Random.Range(MinTime, MaxTime);
            _timer = 0;
        }
        
    }
}
