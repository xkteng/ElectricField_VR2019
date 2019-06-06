using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KaiTool.UI
{
    public abstract class KaiTool_BasicUINode : MonoBehaviour
    {
        public event UnityAction<string> m_buttonEventHandle;
        public event UnityAction<string> m_toggleEventHandle;

        private void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {
            RegisterAllUIEvent();
        }

        private void RegisterAllUIEvent()
        {
            var buttons = GetComponentsInChildren<Button>(true);
            foreach (var item in buttons)
            {
                item.onClick.AddListener(() =>
                {
                    RegisterButtonEvent(item.name);
                    if (m_buttonEventHandle != null)
                    {
                        m_buttonEventHandle(item.name);
                    }
                });
            }
            var toggles = GetComponentsInChildren<Toggle>(true);
            foreach (var item in toggles)
            {
                item.onValueChanged.AddListener((toggle) =>
                {
                    RegisterToggleEvent(item.name, toggle);
                    if (m_toggleEventHandle != null)
                    {
                        m_toggleEventHandle(item.name);
                    }
                });
            }
        }
        protected abstract void RegisterButtonEvent(string name);
        protected abstract void RegisterToggleEvent(string name, bool toggle);
    }
}