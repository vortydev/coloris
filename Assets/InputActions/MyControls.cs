// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/MyControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MyControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MyControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MyControls"",
    ""maps"": [
        {
            ""name"": ""Coloris"",
            ""id"": ""cdae62df-845f-48b5-8f03-562368b97f78"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""1dbe4e43-d0ff-4ec3-8f0c-70e67381d4a1"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate Left"",
                    ""type"": ""Button"",
                    ""id"": ""78d84b6c-a2b9-4160-959b-fdda4c9bbc4b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate Right"",
                    ""type"": ""Button"",
                    ""id"": ""ad4e31f8-8b4f-4795-bdb2-314af00000dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Soft Drop"",
                    ""type"": ""Button"",
                    ""id"": ""ce9c0d2d-f06a-431c-8c4d-868ceefbf9fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hard Drop"",
                    ""type"": ""Button"",
                    ""id"": ""87ec64a4-23fc-4f4b-8a1e-49ecf794a4c6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hold"",
                    ""type"": ""Button"",
                    ""id"": ""acfc851c-7f40-41e6-87c3-816adc80a419"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""644c213f-fe9e-4842-b69d-7f8f00eb6f24"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fab095b4-954e-491d-8024-be0bddf24d8b"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default;Vim"",
                    ""action"": ""Rotate Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e2b61b4-0ae9-4d6d-bb28-5c0070e54356"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Rotate Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f881ff1-0ac4-4b81-ae9c-15427b107862"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1adf05df-4334-4efa-b54c-3be847c40725"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Rotate Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7510fce-fa20-46c0-b397-009491b723ff"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default;Vim"",
                    ""action"": ""Rotate Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2bc0ce8-7de1-44f4-a4fd-fb8b835a35ac"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Vim"",
                    ""action"": ""Rotate Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""716accb3-16e1-4419-9ba0-e27bab6321a4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamer"",
                    ""action"": ""Rotate Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26c54c5b-5f43-45a0-ba15-b34cb151e57b"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0338506e-3ed2-4b1f-a8ef-27b4a2787429"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Soft Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4965f72c-90eb-4561-acad-8be7689c8c37"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Vim"",
                    ""action"": ""Soft Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa9bba3d-c129-45fb-90b2-acbacd8fee1e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamer"",
                    ""action"": ""Soft Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a9c0a5f-3882-4d62-ba04-fbff21de60cd"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Soft Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0b584c3-b8fd-4f1e-bee2-61439c05738c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default;Vim;Gamer"",
                    ""action"": ""Hard Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90ce1a1b-ab36-4582-b009-c901d9b09280"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Hard Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a96a5d64-38ba-4125-9c6c-e7097facef6c"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default;Vim"",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67ba2c63-4526-4378-85fa-aecb89151bb3"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c73b4c2-7976-4014-b4aa-2ca9fd8cad9e"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07bda50d-db33-4065-be5d-41a84ab24887"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamer"",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e5c7e8b-d2bf-4e7e-bdff-78349ed53548"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41b93ed4-6af1-46df-a41d-5762bf1a0a3d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default;Vim;Gamer"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e38d5ee7-cfc3-447e-9f91-4981c2014e74"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Default"",
                    ""id"": ""9dbf492f-e845-4074-ba0e-8ed61f4bb9f8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""889d0bed-0f39-4323-aad0-7c38dea37a35"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""0a9a7caf-604c-4e6c-a9dc-808d7b7c1e6d"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Vim"",
                    ""id"": ""c9052afa-dd07-4a27-b058-c7c10e4d304b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3261e8a6-3d34-4be2-9068-19d92d01f170"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Vim"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""072720f5-0395-40bd-9fc0-8a34beae0cdb"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Vim"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamer"",
                    ""id"": ""26bc86a8-f1ce-492f-a56e-0545ffdbd8e1"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8c43821d-d49e-4c54-9cc4-aa7085d75a6a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamer"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e04d114d-d849-49f4-94fb-10c50f5c9e5c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamer"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Numpad"",
                    ""id"": ""910c124f-bc13-4676-a035-5464c53dd517"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""D-Pad"",
                    ""id"": ""eb21d887-dd10-4cff-bc17-2739dc305a94"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""73af7ab4-f849-4c2e-81ff-b0398f090eff"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4fa6bcb9-4f5d-4448-9f63-600dd984c4dd"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Default"",
            ""bindingGroup"": ""Default"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Vim"",
            ""bindingGroup"": ""Vim"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamer"",
            ""bindingGroup"": ""Gamer"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Coloris
        m_Coloris = asset.FindActionMap("Coloris", throwIfNotFound: true);
        m_Coloris_Move = m_Coloris.FindAction("Move", throwIfNotFound: true);
        m_Coloris_RotateLeft = m_Coloris.FindAction("Rotate Left", throwIfNotFound: true);
        m_Coloris_RotateRight = m_Coloris.FindAction("Rotate Right", throwIfNotFound: true);
        m_Coloris_SoftDrop = m_Coloris.FindAction("Soft Drop", throwIfNotFound: true);
        m_Coloris_HardDrop = m_Coloris.FindAction("Hard Drop", throwIfNotFound: true);
        m_Coloris_Hold = m_Coloris.FindAction("Hold", throwIfNotFound: true);
        m_Coloris_Pause = m_Coloris.FindAction("Pause", throwIfNotFound: true);
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

    // Coloris
    private readonly InputActionMap m_Coloris;
    private IColorisActions m_ColorisActionsCallbackInterface;
    private readonly InputAction m_Coloris_Move;
    private readonly InputAction m_Coloris_RotateLeft;
    private readonly InputAction m_Coloris_RotateRight;
    private readonly InputAction m_Coloris_SoftDrop;
    private readonly InputAction m_Coloris_HardDrop;
    private readonly InputAction m_Coloris_Hold;
    private readonly InputAction m_Coloris_Pause;
    public struct ColorisActions
    {
        private @MyControls m_Wrapper;
        public ColorisActions(@MyControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Coloris_Move;
        public InputAction @RotateLeft => m_Wrapper.m_Coloris_RotateLeft;
        public InputAction @RotateRight => m_Wrapper.m_Coloris_RotateRight;
        public InputAction @SoftDrop => m_Wrapper.m_Coloris_SoftDrop;
        public InputAction @HardDrop => m_Wrapper.m_Coloris_HardDrop;
        public InputAction @Hold => m_Wrapper.m_Coloris_Hold;
        public InputAction @Pause => m_Wrapper.m_Coloris_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Coloris; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ColorisActions set) { return set.Get(); }
        public void SetCallbacks(IColorisActions instance)
        {
            if (m_Wrapper.m_ColorisActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_ColorisActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ColorisActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ColorisActionsCallbackInterface.OnMove;
                @RotateLeft.started -= m_Wrapper.m_ColorisActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.performed -= m_Wrapper.m_ColorisActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.canceled -= m_Wrapper.m_ColorisActionsCallbackInterface.OnRotateLeft;
                @RotateRight.started -= m_Wrapper.m_ColorisActionsCallbackInterface.OnRotateRight;
                @RotateRight.performed -= m_Wrapper.m_ColorisActionsCallbackInterface.OnRotateRight;
                @RotateRight.canceled -= m_Wrapper.m_ColorisActionsCallbackInterface.OnRotateRight;
                @SoftDrop.started -= m_Wrapper.m_ColorisActionsCallbackInterface.OnSoftDrop;
                @SoftDrop.performed -= m_Wrapper.m_ColorisActionsCallbackInterface.OnSoftDrop;
                @SoftDrop.canceled -= m_Wrapper.m_ColorisActionsCallbackInterface.OnSoftDrop;
                @HardDrop.started -= m_Wrapper.m_ColorisActionsCallbackInterface.OnHardDrop;
                @HardDrop.performed -= m_Wrapper.m_ColorisActionsCallbackInterface.OnHardDrop;
                @HardDrop.canceled -= m_Wrapper.m_ColorisActionsCallbackInterface.OnHardDrop;
                @Hold.started -= m_Wrapper.m_ColorisActionsCallbackInterface.OnHold;
                @Hold.performed -= m_Wrapper.m_ColorisActionsCallbackInterface.OnHold;
                @Hold.canceled -= m_Wrapper.m_ColorisActionsCallbackInterface.OnHold;
                @Pause.started -= m_Wrapper.m_ColorisActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_ColorisActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_ColorisActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_ColorisActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @RotateLeft.started += instance.OnRotateLeft;
                @RotateLeft.performed += instance.OnRotateLeft;
                @RotateLeft.canceled += instance.OnRotateLeft;
                @RotateRight.started += instance.OnRotateRight;
                @RotateRight.performed += instance.OnRotateRight;
                @RotateRight.canceled += instance.OnRotateRight;
                @SoftDrop.started += instance.OnSoftDrop;
                @SoftDrop.performed += instance.OnSoftDrop;
                @SoftDrop.canceled += instance.OnSoftDrop;
                @HardDrop.started += instance.OnHardDrop;
                @HardDrop.performed += instance.OnHardDrop;
                @HardDrop.canceled += instance.OnHardDrop;
                @Hold.started += instance.OnHold;
                @Hold.performed += instance.OnHold;
                @Hold.canceled += instance.OnHold;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public ColorisActions @Coloris => new ColorisActions(this);
    private int m_DefaultSchemeIndex = -1;
    public InputControlScheme DefaultScheme
    {
        get
        {
            if (m_DefaultSchemeIndex == -1) m_DefaultSchemeIndex = asset.FindControlSchemeIndex("Default");
            return asset.controlSchemes[m_DefaultSchemeIndex];
        }
    }
    private int m_VimSchemeIndex = -1;
    public InputControlScheme VimScheme
    {
        get
        {
            if (m_VimSchemeIndex == -1) m_VimSchemeIndex = asset.FindControlSchemeIndex("Vim");
            return asset.controlSchemes[m_VimSchemeIndex];
        }
    }
    private int m_GamerSchemeIndex = -1;
    public InputControlScheme GamerScheme
    {
        get
        {
            if (m_GamerSchemeIndex == -1) m_GamerSchemeIndex = asset.FindControlSchemeIndex("Gamer");
            return asset.controlSchemes[m_GamerSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IColorisActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotateLeft(InputAction.CallbackContext context);
        void OnRotateRight(InputAction.CallbackContext context);
        void OnSoftDrop(InputAction.CallbackContext context);
        void OnHardDrop(InputAction.CallbackContext context);
        void OnHold(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
