using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour , IInteracteble
{
    public SOObject SoObject;
    public ItemData ItemData;
    private MeshRenderer _meshRenderer;
    private LineRenderer _lineRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _lineRenderer = GetComponent<LineRenderer>();
        if (ItemData != null)
        {
            ItemData= new ItemData(SoObject);
        }
    }
    

    public void Intecract(InteractComponent.SelectStat selectStat)
    {
        switch (selectStat)
        {
            case InteractComponent.SelectStat.none:
                DesetSelectable();
                break;
            case InteractComponent.SelectStat.selectable :
                SetSelectable();
                break;
            case InteractComponent.SelectStat.Preselected:
                break;
            case InteractComponent.SelectStat.Selec:
                break;
        }
    }

    private void SetSelectable()
    {
        Debug.Log("Objet en selectable");
        _lineRenderer.enabled = true;
        Vector3[] linePos = new Vector3[2];
        linePos[0] = transform.position;
        linePos[1] = transform.position+transform.up*2;
        _lineRenderer.SetPositions(linePos);
    }

    private void DesetSelectable()
    {
        Debug.Log("Objet Deséléctioner");
        _lineRenderer.enabled=false;
    }
}
