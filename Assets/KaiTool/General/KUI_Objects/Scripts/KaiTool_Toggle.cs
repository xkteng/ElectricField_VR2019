using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KaiTool.UI
{
    public class KaiTool_Toggle : MonoBehaviour
    {
        [SerializeField]
        private bool m_isOn = true;
        [SerializeField]
        private Button m_button_On;
        [SerializeField]
        private Button m_button_Off;


        public event Action<bool> m_setValueEventHandle;

        protected virtual void Awake()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {

        }
        private void InitEvent()
        {
            m_button_On.onClick.AddListener(() =>
            {
                OnSetValue(false);
            });
            m_button_Off.onClick.AddListener(() =>
            {
                OnSetValue(true);
            });
        }

        public void OnSetValue(bool boolean)
        {
            if (m_isOn != boolean)
            {
                m_isOn = boolean;
                if (m_setValueEventHandle != null)
                {
                    m_setValueEventHandle(boolean);
                }
                if (boolean)
                {
                    ShowOnButton();
                }
                else
                {
                    ShowOffButton();
                }
            }
        }
        private void OnValidate()
        {
            if (m_button_On && m_button_Off)
            {
                if (m_isOn)
                {
                    ShowOnButton();
                }
                else
                {
                    ShowOffButton();
                }
            }
        }
        private void ShowOnButton()
        {
            m_button_On.gameObject.SetActive(true);
            m_button_Off.gameObject.SetActive(false);
        }
        private void ShowOffButton()
        {
            m_button_On.gameObject.SetActive(false);
            m_button_Off.gameObject.SetActive(true);
        }
    }
}

