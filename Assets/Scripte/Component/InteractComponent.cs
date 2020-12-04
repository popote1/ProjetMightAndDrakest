using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class InteractComponent : MonoBehaviour
{
    public PlayerInfoComponent playerInfoComponent;
    public enum SelectStat{none,selectable,Preselected,Selec}
    public GameObject Interaction;
    public Transform SelectePoints;
    public float InteracteDistance;

    private List<Transform> Selectables = new List<Transform>();
    private Transform PreselectedTransform;

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (CheckCloserToCenter() != PreselectedTransform)
        {
            if(PreselectedTransform!=null)PreselectedTransform.GetComponent<IInteracteble>().DePreselectable();
            PreselectedTransform = CheckCloserToCenter();
            if(PreselectedTransform!=null)PreselectedTransform.GetComponent<IInteracteble>().SetPreselctable();
        }
        //ChechSelectebles();
    }

    public void AddSelectable(Collider selectable)
    {
        if (selectable.GetComponent<IInteracteble>() != null)
        {
            selectable.GetComponent<IInteracteble>().SetSelectable();
            Selectables.Add(selectable.transform);
        }
    }

    public void RemoveSelectable(Collider selectable)
    {
        if (selectable.GetComponent<IInteracteble>() != null)
        {
            selectable.GetComponent<IInteracteble>().DesetSelectable();
            if (Selectables.Contains(selectable.transform))
            {
                Selectables.Remove(selectable.transform);
            }
        }
    }
    
    
    public void OnClick(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            Debug.Log("click");
            if (PreselectedTransform != null)
            {
                if (PreselectedTransform.GetComponent<IInteracteble>().USe(playerInfoComponent))
                {
                    Selectables.Remove(PreselectedTransform);
                }
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(SelectePoints.position, InteracteDistance); 
        Gizmos.color = Color.green;
    }

    private Transform CheckCloserToCenter()
    {
        float distanceMin = InteracteDistance;
        Transform closerTransform = null;
        foreach (Transform item in Selectables) {
            if ((item.position - SelectePoints.position).magnitude < distanceMin) {
                distanceMin = (item.position - SelectePoints.position).magnitude;
                closerTransform = item;
            }
        }
        return closerTransform;
    }
}