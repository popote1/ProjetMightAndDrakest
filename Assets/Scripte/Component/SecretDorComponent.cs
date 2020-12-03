using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDorComponent : MonoBehaviour
{
    public void OpenDoor()
    {
        transform.position += transform.up * 2;
    }
}
