using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeComponent : MonoBehaviour
{
    public float ShakeTime;
    public float Intencity;
    public float Frequency;

    private float _shakeTime;
    private float _frequency;
    private Vector3 _originalPosition;
    

    // Update is called once per frame
    void Update()
    {
        if (_shakeTime > 0)
        {
            _shakeTime -=Time.deltaTime;
            if (_frequency <= 0)
            {
                transform.localPosition = _originalPosition+new Vector3(Random.Range(-Intencity, Intencity),Random.Range(-Intencity, Intencity),Random.Range(-Intencity, Intencity));
                _frequency = Frequency;
            }

            _frequency -= Time.deltaTime;
        }
        if (_shakeTime < 0)
        {
            transform.localPosition = _originalPosition;
        }
    }


[ContextMenu("test")]
    public void StartShake()
    {
        _shakeTime = ShakeTime;
        _frequency = 0;
        _originalPosition = transform.localPosition;
    }
    
    
}
