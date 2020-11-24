// GENERATED AUTOMATICALLY FROM 'Assets/FirstCaracterControler/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""625489e7-80d1-4c91-a7dc-968328558a48"",
            ""actions"": [
                {
                    ""name"": ""InputDeplacement"",
                    ""type"": ""Value"",
                    ""id"": ""c2bbb5a5-c0c0-413d-afc2-da059a2db0ee"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""InputCamera"",
                    ""type"": ""Value"",
                    ""id"": ""9e0701e9-3345-4d43-a008-bf0b0671b1f2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""InputKeyboardZQSD"",
                    ""id"": ""cbb0e430-d2a8-481e-8686-cb2f2cf19c12"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputDeplacement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d84135cd-f07b-49da-a23f-dea63dde37ed"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputDeplacement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8c3f1750-43f2-42e0-bfff-069e2cda3bd9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputDeplacement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""80d002ab-b698-4c77-abd2-9de89e7b46b7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputDeplacement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d1a40a9c-d1ff-4d93-8816-bbc5b3019fef"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputDeplacement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""InputKeyboardArrow"",
                    ""id"": ""9233cb31-247e-4a8e-8929-b695a918d1dc"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputDeplacement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""cb8d9ab8-708f-415c-8e3e-021403166f40"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputDeplacement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2659a4fd-999e-459d-bf0b-bb66c5f9a8a1"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputDeplacement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""964ac5e8-6ca6-49a4-96ab-cf34b641608a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputDeplacement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1d3809b3-e836-43e8-ae39-ba0b6b7dc091"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputDeplacement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7dc44dbd-8fe9-46d0-ba2e-8468f8fdbe69"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""InputCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_InputDeplacement = m_Player.FindAction("InputDeplacement", throwIfNotFound: true);
        m_Player_InputCamera = m_Player.FindAction("InputCamera", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_InputDeplacement;
    private readonly InputAction m_Player_InputCamera;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @InputDeplacement => m_Wrapper.m_Player_InputDeplacement;
        public InputAction @InputCamera => m_Wrapper.m_Player_InputCamera;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @InputDeplacement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInputDeplacement;
                @InputDeplacement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInputDeplacement;
                @InputDeplacement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInputDeplacement;
                @InputCamera.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInputCamera;
                @InputCamera.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInputCamera;
                @InputCamera.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInputCamera;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @InputDeplacement.started += instance.OnInputDeplacement;
                @InputDeplacement.performed += instance.OnInputDeplacement;
                @InputDeplacement.canceled += instance.OnInputDeplacement;
                @InputCamera.started += instance.OnInputCamera;
                @InputCamera.performed += instance.OnInputCamera;
                @InputCamera.canceled += instance.OnInputCamera;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnInputDeplacement(InputAction.CallbackContext context);
        void OnInputCamera(InputAction.CallbackContext context);
    }
}
