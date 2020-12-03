using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class InteractComponent : MonoBehaviour
{
    public enum SelectStat{none,selectable,Preselected,Selec}
    public GameObject Interaction;
    public Transform SelectePoints;
    public float InteracteDistance;

    private List<IInteracteble> Interactebles = new List<IInteracteble>();
    private GameObject[] SelectableInteraction;
    private GameObject PreselecteInteraction;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        ChechSelectebles();
    }

    private void ChechSelectebles() {
        List<IInteracteble> interactable = new List<IInteracteble>();
        Collider[] colliders = Physics.OverlapSphere(SelectePoints.position, InteracteDistance);
        foreach (Collider collider in colliders) {
            if (collider.GetComponent<IInteracteble>() != null) {
                if (!Interactebles.Contains(collider.GetComponent<IInteracteble>())) {
                    Debug.Log("Ajout de " + collider.name + " a la liste des interactibles");
                    collider.GetComponent<IInteracteble>().Intecract(SelectStat.selectable);
                    Interactebles.Add(collider.GetComponent<IInteracteble>());
                }
            }
        }

        if (Interactebles.Count > 0)
        {
            foreach (IInteracteble item in Interactebles)
            {
                if (!interactable.Contains(item))
                {
                    item.Intecract(SelectStat.none);
                    //Interactebles.Remove(item);
                }
            }
        }
    }
    public void OnClick(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            Debug.Log("click");
        }
        Interaction.GetComponent<IInteracteble>().Intecract(SelectStat.selectable);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(SelectePoints.position, InteracteDistance); 
        Gizmos.color = Color.green;
    }
}
