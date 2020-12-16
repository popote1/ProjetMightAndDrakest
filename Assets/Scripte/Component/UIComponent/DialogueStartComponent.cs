using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStartComponent : MonoBehaviour
{
    [Header("Dialogue1")]
    [TextArea] public string DialogueText1;
    public int SkipIndex1=0;
    [Header("Dialogue2")]
    [TextArea] public string DialogueText2;
    public int SkipIndex2=0;


    private HUDComponent HudComponent;
    void Start()
    {
        HudComponent = GameObject.FindWithTag("Player").GetComponent<HUDComponent>();
    }

    public void PlayDialogue1()
    {
        HudComponent.OpenDialogue(DialogueText1,SkipIndex1);
    }
    public void PlayDialogue2 ()
    {
        HudComponent.OpenDialogue(DialogueText2, SkipIndex2);
    }
}
