using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InteractComponent : MonoBehaviour
{
    public GameObject Interaction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Debug.Log("click");
        }
        Interaction.GetComponent<IInteracteble>().Intecract();
    }
}
