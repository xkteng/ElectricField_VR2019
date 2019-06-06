using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KaiTool.ElectricCircuit
{
    public struct SwitchRendererEventArgs
    {

    }
    [RequireComponent(typeof(Image))]
    [ExecuteInEditMode]
    public class KaiTool_SingleInputSwitch_2DRenderer : MonoBehaviour
    {
        [SerializeField]
        private bool m_isOn = false;
        [SerializeField]
        private KaiTool_SingleInputSwitch m_switch;
        [SerializeField]
        private Sprite m_onSprite;
        [SerializeField]
        private Sprite m_offSprite;

        public event Action<System.Object, SwitchRendererEventArgs> m_turnedOnEventHandle;
        public event Action<System.Object, SwitchRendererEventArgs> m_turnedOffEventHandle;


        [SerializeField]
        [HideInInspector]
        private Image m_image;

        public bool IsOn
        {
            get
            {
                return m_isOn;
            }
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            //  m_image = GetComponent<Image>();
        }
        private void InitEvent()
        {
            if (m_switch != null)
            {
                m_switch.TurnedOn.AddListener(() =>
                {
                    OnTurnOn();
                });
                m_switch.TurnedOff.AddListener(() =>
                {
                    OnTurnOff();
                });
            }
        }
        private void Reset()
        {
            m_image = GetComponent<Image>();
        }
        private void OnValidate()
        {
            if (m_onSprite && m_offSprite)
            {
                if (m_switch != null)
                {
                    if (m_switch.IsOn == 0)
                    {
                        if (m_offSprite != null)
                        {
                            OnTurnOff();
                        }
                    }
                    else
                    {
                        if (m_onSprite != null)
                        {
                            OnTurnOn();
                        }
                    }
                }
                else
                {
                    if (m_isOn)
                    {
                        m_image.sprite = m_onSprite;
                    }
                    else
                    {
                        m_image.sprite = m_offSprite;
                    }
                }
            }
        }
        [ContextMenu("TurnOn")]
        public void OnTurnOn()
        {
            if (!m_isOn)
            {
                if (m_turnedOnEventHandle != null)
                {
                    var args = new SwitchRendererEventArgs();
                    m_turnedOnEventHandle(this, args);
                }
                m_isOn = true;
                if (m_onSprite != null)
                {
                    m_image.sprite = m_onSprite;
                }
            }
        }
        [ContextMenu("TurnOff")]
        public void OnTurnOff()
        {
            if (m_isOn)
            {
                if (m_turnedOffEventHandle != null)
                {
                    var args = new SwitchRendererEventArgs();
                    m_turnedOffEventHandle(this, args);
                }
                m_isOn = false;
                if (m_offSprite != null)
                {
                    m_image.sprite = m_offSprite;
                }
            }
        }
    }
}