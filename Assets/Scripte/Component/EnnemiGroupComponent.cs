using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnnemiGroupComponent : MonoBehaviour
{
    public List<EnnemiInfo> Ennemis = new List<EnnemiInfo>();
    public float FightDistance;
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
    [Header("TemporalTest")]
    public List<SOEnnemi> SoEnnemis;
    public bool IsVictory;

    private Vector3 _targetPos;
    private int _nextPos=-1;
    private Transform _player;
    private bool _chasingPlayer;
    private bool _isdead;
    private bool _isInFight = false;


    private void Awake()
    {
        Debug.Log(SoEnnemis.Count);
        foreach (SOEnnemi so in SoEnnemis) {
            Debug.Log(" création de l'info ennemi "+so.Name);
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
        if (!_isdead) {
            if (HUDComponent.IsExploring) {
                _isInFight = false;
                if (!_chasingPlayer) {
                    navMeshAgent.speed = PatrolSpeed;
                    if (PatrolPoints.Count > 0) {
                        if ((transform.position - _targetPos).magnitude < DistanceToPint) {
                            SetNextPos();
                        }
                        foreach (var animator in Animators) {
                            animator.ChangeAnimationtype(EnnemiMaterialAnimationComponent.AnimationType.Walk);
                        }
                    }
                    else {
                        
                            foreach (var animator in Animators)
                            {
                                animator.ChangeAnimationtype(EnnemiMaterialAnimationComponent.AnimationType.Idel);
                            }
                       
                    }

                    if ((transform.position - _player.position).magnitude < DetectionDistance) {
                        _chasingPlayer = true;
                    }
                }
                else {
                    navMeshAgent.speed = ChasingSpeed;
                    navMeshAgent.destination = _player.position;
                    foreach (var animator in Animators) {
                        animator.ChangeAnimationtype(EnnemiMaterialAnimationComponent.AnimationType.Walk);
                    }


                }

                if ((transform.position - _player.position).magnitude < FightDistance) {
                    Debug.Log(" début du comat");
                    _player.GetComponent<HUDComponent>().StartFight(this);
                }
            }
            else {
                navMeshAgent.speed = 0;
                navMeshAgent.velocity = Vector3.zero;
                if (_isInFight==false)
                {
                    foreach (var animator in Animators) {
                        animator.ChangeAnimationtype(EnnemiMaterialAnimationComponent.AnimationType.Idel);
                    }
                }
                _isInFight = true;
            }
        }
        else {
            navMeshAgent.speed = 0;
            navMeshAgent.velocity = Vector3.zero;
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

    public void Die()
    {
        _isdead = true;
    }
}
