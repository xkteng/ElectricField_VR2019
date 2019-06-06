using System;
using UnityEngine;
using UnityEngine.UI;

namespace KaiTool.ElectricCircuit
{
    public struct CircuitDeviceRendererEventArgs
    {

    }
    [RequireComponent(typeof(Image))]
    [ExecuteInEditMode]
    public class KaiTool_CircuitDevice_2DRenderer : MonoBehaviour
    {
        [SerializeField]
        private bool m_isElectrified = false;
        [SerializeField]
        private KaiTool_CircuitDevice m_device;
        [SerializeField]
        private Sprite m_electrifiedSprite;
        [SerializeField]
        private Sprite m_unelectrifiedSprite;

        public Action<System.Object, CircuitDeviceRendererEventArgs> m_electrifiedEventHandle;
        public Action<System.Object, CircuitDeviceRendererEventArgs> m_unelectrifiedEventHandle;

        [SerializeField]
        [HideInInspector]
        private Image m_image;
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar() { }
        private void InitEvent()
        {
            m_device.m_electrifiedEventHandle += (sender, e) =>
            {
                m_image.sprite = m_electrifiedSprite;
                m_isElectrified = true;
            };
            m_device.m_unelectrifiedEventHandle += (sender, e) =>
            {
                m_image.sprite = m_unelectrifiedSprite;
                m_isElectrified = false;
            };
        }

        private void OnValidate()
        {
            if (m_electrifiedSprite && m_unelectrifiedSprite)
            {
                if (m_device != null)
                {
                    if (m_device.IsElectrified != 0)
                    {
                        m_image.sprite = m_electrifiedSprite;
                        m_isElectrified = false;
                    }
                    else
                    {
                        m_image.sprite = m_unelectrifiedSprite;
                        m_isElectrified = true;
                    }
                }
                else
                {
                    if (m_isElectrified)
                    {
                        m_image.sprite = m_electrifiedSprite;
                    }
                    else
                    {
                        m_image.sprite = m_unelectrifiedSprite;
                    }
                }
            }
        }
        private void Reset()
        {
            m_image = GetComponent<Image>();
        }


    }
}