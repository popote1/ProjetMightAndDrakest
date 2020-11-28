using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiGroupComponent : MonoBehaviour
{
    public List<EnnemiInfo> Ennemis = new List<EnnemiInfo>();
    public float DetectionDistance;
    public float ChasingSpeed;
    public GameObject Ennemi1;
    public GameObject Ennemi2;
    public GameObject Ennemi3;
    public NavMeshAgent navMeshAgent;
    public List<EnnemiMaterialAnimationComponent> Animators;
    public float DistanceToPint;
    public float PatrolSpeed;
    public List<Transform> PatrolPoints;
    public static bool IsExploring=true;
    [Header("TemporalTest")]
    public List<SOEnnemi> SoEnnemis;

    private Vector3 _targetPos;
    private int _nextPos=-1;
    private Transform _player;
    private bool _chasingPlayer;
    

    private void Awake()
    {
        Debug.Log(SoEnnemis.Count);
        foreach (SOEnnemi so in SoEnnemis) {
            Ennemis.Add(new EnnemiInfo(so));
        }
        IniteEnnemis();
    }
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        if(PatrolPoints.Count > 0) SetNextPos();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsExploring)
        {
            if (!_chasingPlayer)
            {
                if (PatrolPoints.Count > 0)
                {
                    if ((transform.position - _targetPos).magnitude < DistanceToPint)
                    {
                        SetNextPos();
                    }

                    foreach (var animator in Animators)
                    {
                        animator.ChangeAnimationtype(EnnemiMaterialAnimationComponent.AnimationType.Walk);
                    }
                }
                else
                {
                    foreach (var animator in Animators)
                    {
                        animator.ChangeAnimationtype(EnnemiMaterialAnimationComponent.AnimationType.Idel);
                    }
                }

                if ((transform.position - _player.position).magnitude < DetectionDistance)
                {
                    _chasingPlayer = true;
                }
            }
            else
            {
                navMeshAgent.speed = ChasingSpeed;
                navMeshAgent.destination = _player.position;
                foreach (var animator in Animators)
                {
                    animator.ChangeAnimationtype(EnnemiMaterialAnimationComponent.AnimationType.Walk);
                }
            }
            
        }
    }

    private void SetNextPos()
    {
        _nextPos++;
        if (_nextPos >= PatrolPoints.Count) {_nextPos=0;}
        navMeshAgent.destination = PatrolPoints[_nextPos].position;
        _targetPos=PatrolPoints[_nextPos].position;
        navMeshAgent.speed = PatrolSpeed;
    }

    private void IniteEnnemis()
    {
        EnnemiMaterialAnimationComponent animateur;
        switch (Ennemis.Count)
        {
            case 1:
                Ennemi1.SetActive(true);
                animateur = Ennemi1.GetComponent<EnnemiMaterialAnimationComponent>();
                animateur.EnnemiAnimation = Ennemis[0].SoEnnemi.SoEnnemiAnimation;
                Animators.Add(animateur);
                Ennemi2.SetActive(false);
                Ennemi3.SetActive(false);
                break;
            case 2:
                Ennemi1.SetActive(false);
                Ennemi2.SetActive(true);
                animateur = Ennemi2.GetComponent<EnnemiMaterialAnimationComponent>();
                animateur.EnnemiAnimation = Ennemis[0].SoEnnemi.SoEnnemiAnimation;
                Animators.Add(animateur);
                Ennemi3.SetActive(true);
                animateur = Ennemi3.GetComponent<EnnemiMaterialAnimationComponent>();
                animateur.EnnemiAnimation = Ennemis[1].SoEnnemi.SoEnnemiAnimation;
                Animators.Add(animateur);
                break;
            case 3:
                Ennemi1.SetActive(true);
                animateur = Ennemi1.GetComponent<EnnemiMaterialAnimationComponent>();
                animateur.EnnemiAnimation = Ennemis[0].SoEnnemi.SoEnnemiAnimation;
                Animators.Add(animateur);
                Ennemi2.SetActive(true);
                animateur = Ennemi2.GetComponent<EnnemiMaterialAnimationComponent>();
                animateur.EnnemiAnimation = Ennemis[1].SoEnnemi.SoEnnemiAnimation;
                Animators.Add(animateur);
                Ennemi3.SetActive(true);
                animateur = Ennemi3.GetComponent<EnnemiMaterialAnimationComponent>();
                animateur.EnnemiAnimation = Ennemis[2].SoEnnemi.SoEnnemiAnimation;
                Animators.Add(animateur);
                break;
        }
    }

    private void SetEnnemi()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position , DetectionDistance);
    }
}
