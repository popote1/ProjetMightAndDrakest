using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiGroupComponent : MonoBehaviour
{
    public List<EnnemiInfo> Ennemis = new List<EnnemiInfo>();
    public float DetectionDistance;
    public GameObject Ennemi1;
    public GameObject Ennemi2;
    public GameObject Ennemi3;
    public NavMeshAgent navMeshAgent;
    public EnnemiMaterialAnimationComponent Animator;
    public List<Transform> PatrolPoints;
    public static bool IsExploring;
    [Header("TemporalTest")]
    public List<SOEnnemi> SoEnnemis;

    private void Awake()
    {
        Debug.Log(SoEnnemis.Count);
        foreach (SOEnnemi so in SoEnnemis) {
          
            Ennemis.Add(new EnnemiInfo(so));
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
