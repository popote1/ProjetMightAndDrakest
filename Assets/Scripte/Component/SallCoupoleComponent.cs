using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SallCoupoleComponent : MonoBehaviour
{
    public PuzzelComponent PuzzelComponent;
    public GameObject Porte;
    public Vector3 ClosePose;

    public AudioClip CloseSound;
    [Range(0, 1)] public float Volume;

    private bool _doorClose = false;


    public void ItemPiked()
    {
        if (!PuzzelComponent.PuzzelComplet)
        {
            Porte.transform.localPosition = ClosePose;
            SoundManager.PlaySound(CloseSound, Volume);
            _doorClose = true;
        }
    }

    public void PuzzelComplet()
    {
        if (_doorClose)
        {
            Porte.GetComponent<SecretDorComponent>().OpenDoor();
        }
    }
}