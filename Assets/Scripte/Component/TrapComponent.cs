using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapComponent : MonoBehaviour
{
    public List<GameObject> Lance;
    public float SortieDeLance;
    public float TempoLancement;
    public float TempoRecharge;
    public int Damage;
    public Vector3 OldPos;
    public Vector3 NewPos;
    
    private int _trapStat = 1;
    private float _timer;
    void Start()
    {
        
    }

   
    void Update()
    {
        if (_trapStat == 2)
        {
            _timer += Time.deltaTime;
            if (_timer >= TempoLancement)
            {
                _timer = 0;
                _trapStat = 3;
            }
        }

        if (_trapStat == 3)
        {
           Collider[]toucher= Physics.OverlapBox(transform.position, new Vector3(1, 1, 1));
           foreach (Collider touche in toucher)
           {
               if (touche.CompareTag("Player"))
               {
                   touche.GetComponent<PlayerInfoComponent>().CurrentHP -= Damage;
               }
           }

           foreach (var lance in Lance)
           {
               lance.transform.localPosition = NewPos;
           }

           _trapStat = 4;
        }

        if (_trapStat == 4)
        {
            if (_timer >= 1)
            {
                foreach (var lance in Lance)
                {
                    lance.transform.localPosition = OldPos;
                }
            }
            _timer += Time.deltaTime;
            if (_timer >= TempoRecharge)
            {
                _timer = 0;
                _trapStat = 1;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Déclanche le combat");
            if (_trapStat == 1) _trapStat = 2;
        }
    }
}
