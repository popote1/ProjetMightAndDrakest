using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HUDComponent : MonoBehaviour
{
    [Header("UIElements")] public Slider SliderHP;
    public TMP_Text TxtHP;
    public Image specialStat;
    
    
    [Header("TechnicalShit")]
    public Slider InventorySlyder;
    public float TimeToOpeninventory;
    public CanvasGroup canvasGroup;
    public PlayerInfoComponent PlayerInfoComponent;
    public InventoryComponent InventoryComponent;
    public FightComonent fightComonent;
    public GameObject PanelInventory;
    public PlayerInput PlayerInput;
    public GameObject PanelHUD;
    public GameObject PanelComabt;
    public static bool IsExploring=false;
    public GameObject Camera;

    [Header("Dialogue")] 
    public GameObject PanelDialogue;
    public TMP_Text TxtDialogueText;
    public TMP_Text txtDialogueSkipText;
    public float DialogueSpeed;
    public float DialogueStepSpeed;
    
    [Header("Sound")] 
    public AudioClip OpenInventoryClip;
    public AudioClip CombateMusic;
    public AudioClip StartCombat;
    [Range(0, 1)] public float StartCombatVolume;
    public AudioClip EndCombat;
    [Range(0, 1)] public float EndCombatVolume;
    
    [Range(0, 1)] public float OpenInventotyVolume = 1;
    private float _inventorytimer;
    private bool isClicked;
    private bool _showInventoryBar;
    private bool _isInDialogue;
    private int _skipIndex;
    private float _originalSpeed;
    private float _originalStepSpeed;

    // Start is called before the first frame update
    void Start()
    {
        specialStat.enabled = false;
    }

    public void ExitMainMenu()
    {
        Camera.SetActive(true);
        PanelHUD.SetActive(true);
        IsExploring = true;
        PlayerInput.actions.FindActionMap("ExplorationControl").Enable();
        PlayerInput.actions.FindActionMap("MenuControl").Disable();
    }

    void Update()
    {
        if (isClicked)
        {
            _inventorytimer += Time.deltaTime;
            if (_inventorytimer > TimeToOpeninventory / 5) _showInventoryBar = true;
            if (_inventorytimer > TimeToOpeninventory)
            {
                _inventorytimer = TimeToOpeninventory;
                PanelInventory.SetActive(true);
                PanelHUD.SetActive(false);
                IsExploring = false;
                Debug.Log("IsExploring esr false");
                PlayerInput.actions.FindActionMap("ExplorationControl").Disable();
                PlayerInput.actions.FindActionMap("MenuControl").Enable();
                InventoryComponent.LaunchInventory();
                SoundManager.PlaySound(OpenInventoryClip,OpenInventotyVolume);
                if(_isInDialogue&&_skipIndex==1)CloseDialogue();
            }
            InventorySlyder.value = _inventorytimer / TimeToOpeninventory;
        }
        else if(!isClicked&&_inventorytimer!=0)
        {
            _inventorytimer -= Time.deltaTime;
            if (_inventorytimer < TimeToOpeninventory / 5) _showInventoryBar = false;
            if (_inventorytimer < 0) _inventorytimer = 0;
            InventorySlyder.value = _inventorytimer / TimeToOpeninventory;
        }

        if (_showInventoryBar)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, 0.1f);
        }
        else
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, 0.1f);
        }


        SliderHP.value = Mathf.Lerp(SliderHP.value, (float)PlayerInfoComponent.CurrentHP / PlayerInfoComponent.MaxHP, 0.2f);
        TxtHP.text = PlayerInfoComponent.CurrentHP + "/" + PlayerInfoComponent.MaxHP;
    }

    public void OnClick(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            isClicked = true;
            if (_isInDialogue&&_skipIndex==0)
            {
             CloseDialogue();   
            }
        }
        if (callbackContext.canceled)
        {
            isClicked = false;
        }
    }

    public void CloseInventory()
    {
        IsExploring = true;
        PanelInventory.SetActive(false);
        PanelHUD.SetActive(true);
        PlayerInput.actions.FindActionMap("ExplorationControl").Enable();
        PlayerInput.actions.FindActionMap("MenuControl").Disable();
        SoundManager.PlaySound(OpenInventoryClip,OpenInventotyVolume);
    }

    public void StartFight(EnnemiGroupComponent ennemiGroupComponent)
    {
        PlayerInput.actions.FindActionMap("CombatsControls").Enable();
        PlayerInput.actions.FindActionMap("ExplorationControl").Disable();
        IsExploring = false;
        fightComonent.EnnemiGroupComponent = ennemiGroupComponent;
        PanelComabt.SetActive(true);
        PanelHUD.SetActive(false);
        fightComonent.StartFight();
        SoundManager.PlaySound(StartCombat,StartCombatVolume);
        SoundManager.StartCombatMusic(CombateMusic);
    }

    public void EndFight()
    {
        PlayerInput.actions.FindActionMap("CombatsControls").Disable();
        PlayerInput.actions.FindActionMap("ExplorationControl").Enable();
        IsExploring = true;
        PanelComabt.SetActive(false);
        PanelHUD.SetActive(true);
        SoundManager.EndCombatMusic();
        SoundManager.PlaySound(EndCombat,EndCombatVolume);
    }

    public void OpenDialogue(string text, int skipIndex)
    {
        if (!_isInDialogue)
        {
            _isInDialogue = true;
            PanelDialogue.SetActive(true);
            TxtDialogueText.text = text;
            _skipIndex = skipIndex;
            switch (skipIndex)
            {
                case 0:
                    txtDialogueSkipText.text = "Clique pour passer";
                    break;
                case 1:
                    txtDialogueSkipText.text = "Maintien Clique pour passer";
                    break;
            }

            FPControlerComponent FPControler = PlayerInfoComponent.transform.GetComponent<FPControlerComponent>();
            _originalSpeed = FPControler.Speed;
            _originalStepSpeed = FPControler.StepTime;
            FPControler.Speed = DialogueSpeed;
            FPControler.StepTime = DialogueStepSpeed;
        }
    }

    public void CloseDialogue()
    {
        PanelDialogue.SetActive(false);
        PlayerInfoComponent.transform.GetComponent<FPControlerComponent>().Speed = _originalSpeed;
        PlayerInfoComponent.transform.GetComponent<FPControlerComponent>().StepTime = _originalStepSpeed;
        _isInDialogue = false;
    }
}
