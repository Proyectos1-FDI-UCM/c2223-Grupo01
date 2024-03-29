//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Input/UniversalInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @UniversalInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @UniversalInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UniversalInput"",
    ""maps"": [
        {
            ""name"": ""Mighty"",
            ""id"": ""77531062-da48-4377-bfee-3cb7bfd71a60"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""52d8d04a-ef00-4d10-b1cb-75d14eecc529"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""ad4060cd-1f8b-442b-b596-29030e155ab7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""501457ac-f6e3-4584-9140-d4e26c247812"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Melee"",
                    ""type"": ""Button"",
                    ""id"": ""49921be3-7265-448e-9adb-0dbe2dc02b61"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""bc52fb52-9ba7-4c1e-a9ec-af2da62586b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Change"",
                    ""type"": ""Button"",
                    ""id"": ""8a2428d3-c75a-4088-9314-69d827fb9a0b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""fefe2092-fb8c-4582-8e49-4397d6d4577c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""5b07ef7d-3f00-463c-9da8-c83ae1f72b6f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""93c7e9df-474c-42cc-86a6-ac5765226573"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5bf42db0-f18b-4f2b-8e03-cb7da06adf0c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c1fc303e-2fe5-4149-8881-303e6fbda1e2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""515b27a8-d9d3-49dd-a209-d0a5401dd3c7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""093a493c-412a-41f1-9665-3913394fa6e8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8d651d90-96e1-4e92-a2cc-29bd616ba616"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2b53a75f-d6dc-4f02-ac00-e7030255854b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fec22e66-a66e-467b-a623-fab0ca700bd8"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""27ef82d8-5b16-4ddf-82c7-540814e1dcfa"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""affac486-744f-4557-92fb-d3aee83079b9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8146862e-c03e-4e23-a0ad-6736795da83d"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""61d759f9-25b4-439a-8bc8-30dd3f15a097"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2fdbac98-d020-4f9e-9279-a27615eb08fa"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8f185a68-ea00-4047-96b6-edb8395cb31f"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6609836b-e395-4e1c-9e30-a5e43ad29ad8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""951f4ac2-d25b-4840-a87d-7c180c70e26a"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""81b89832-2435-426c-b00c-3fbe29a99217"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5b97f84f-7035-4e18-953e-9408e6161f38"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3e689ee2-348a-4a15-8270-23460268cf75"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7636aaae-3fc4-4aaf-a25f-92b2a1341c99"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69f812e4-2cf1-4e31-bedb-c77d08a2a740"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fe9ad07-c3e6-43b3-8c82-d57d51aa231e"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e86a8866-7572-458c-a44c-b30f701816ff"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6fab7be8-b028-45b5-921e-465a6143c9b6"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12258d60-aa3f-4c75-870a-b83e0737c36c"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""037d2880-04de-46cd-a1aa-ecdd5a7cbdcf"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86fbbcb0-6a16-4a51-b321-57d655c5aad4"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f32da1d-8511-4208-92ef-78c3ea82bf2e"",
                    ""path"": ""<Keyboard>/n"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Change"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2dbb9f9c-3d34-4fbf-8e2e-aa876f5d8b94"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Change"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0835a42d-d7ca-4fb0-87e9-76b57cccbc9b"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""790ccfd8-9381-45d5-94f1-13696b438d81"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Mighty
        m_Mighty = asset.FindActionMap("Mighty", throwIfNotFound: true);
        m_Mighty_Movement = m_Mighty.FindAction("Movement", throwIfNotFound: true);
        m_Mighty_Shoot = m_Mighty.FindAction("Shoot", throwIfNotFound: true);
        m_Mighty_Dash = m_Mighty.FindAction("Dash", throwIfNotFound: true);
        m_Mighty_Melee = m_Mighty.FindAction("Melee", throwIfNotFound: true);
        m_Mighty_Jump = m_Mighty.FindAction("Jump", throwIfNotFound: true);
        m_Mighty_Change = m_Mighty.FindAction("Change", throwIfNotFound: true);
        m_Mighty_Interact = m_Mighty.FindAction("Interact", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Mighty
    private readonly InputActionMap m_Mighty;
    private IMightyActions m_MightyActionsCallbackInterface;
    private readonly InputAction m_Mighty_Movement;
    private readonly InputAction m_Mighty_Shoot;
    private readonly InputAction m_Mighty_Dash;
    private readonly InputAction m_Mighty_Melee;
    private readonly InputAction m_Mighty_Jump;
    private readonly InputAction m_Mighty_Change;
    private readonly InputAction m_Mighty_Interact;
    public struct MightyActions
    {
        private @UniversalInput m_Wrapper;
        public MightyActions(@UniversalInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Mighty_Movement;
        public InputAction @Shoot => m_Wrapper.m_Mighty_Shoot;
        public InputAction @Dash => m_Wrapper.m_Mighty_Dash;
        public InputAction @Melee => m_Wrapper.m_Mighty_Melee;
        public InputAction @Jump => m_Wrapper.m_Mighty_Jump;
        public InputAction @Change => m_Wrapper.m_Mighty_Change;
        public InputAction @Interact => m_Wrapper.m_Mighty_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Mighty; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MightyActions set) { return set.Get(); }
        public void SetCallbacks(IMightyActions instance)
        {
            if (m_Wrapper.m_MightyActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_MightyActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_MightyActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_MightyActionsCallbackInterface.OnMovement;
                @Shoot.started -= m_Wrapper.m_MightyActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_MightyActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_MightyActionsCallbackInterface.OnShoot;
                @Dash.started -= m_Wrapper.m_MightyActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_MightyActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_MightyActionsCallbackInterface.OnDash;
                @Melee.started -= m_Wrapper.m_MightyActionsCallbackInterface.OnMelee;
                @Melee.performed -= m_Wrapper.m_MightyActionsCallbackInterface.OnMelee;
                @Melee.canceled -= m_Wrapper.m_MightyActionsCallbackInterface.OnMelee;
                @Jump.started -= m_Wrapper.m_MightyActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MightyActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MightyActionsCallbackInterface.OnJump;
                @Change.started -= m_Wrapper.m_MightyActionsCallbackInterface.OnChange;
                @Change.performed -= m_Wrapper.m_MightyActionsCallbackInterface.OnChange;
                @Change.canceled -= m_Wrapper.m_MightyActionsCallbackInterface.OnChange;
                @Interact.started -= m_Wrapper.m_MightyActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_MightyActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_MightyActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_MightyActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Melee.started += instance.OnMelee;
                @Melee.performed += instance.OnMelee;
                @Melee.canceled += instance.OnMelee;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Change.started += instance.OnChange;
                @Change.performed += instance.OnChange;
                @Change.canceled += instance.OnChange;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public MightyActions @Mighty => new MightyActions(this);
    public interface IMightyActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnMelee(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnChange(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
