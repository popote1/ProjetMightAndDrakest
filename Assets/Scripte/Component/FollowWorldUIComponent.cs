using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWorldUIComponent : MonoBehaviour
{
    public Transform lookAt;
    public Vector3 OffSet;
    
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void Update()
    {
        Vector3 pos = _camera.WorldToScreenPoint(lookAt.position + OffSet);
        if (transform.position != pos)
            transform.position = pos;
    }
}
