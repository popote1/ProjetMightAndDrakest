
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class flikeringLight : MonoBehaviour
{
    public Light Light;
    
    private float _newTime=0.5f;
    private float _timer=0;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _newTime)
        {
            Light.intensity = Random.Range(0.2f,0.5f);
            _newTime = Random.Range(0.2f, 0.8f);
            _timer = 0;
        }
        
    }
}
