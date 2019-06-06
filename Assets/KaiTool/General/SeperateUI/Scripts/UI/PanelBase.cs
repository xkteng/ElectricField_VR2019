using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace SeperateUI
{
    public abstract class PanelBase : MonoBehaviour
    {
        public string m_skinPath;
        public GameObject m_skin;
        public int m_layer = 0;
        public object[] m_args;
        protected UIManager m_uiManager = null;

        [HideInInspector]
        public UnityEvent m_showing = new UnityEvent();
        [HideInInspector]
        public UnityEvent m_showed = new UnityEvent();
        [HideInInspector]
        public UnityEvent m_closing = new UnityEvent();
        [HideInInspector]
        public UnityEvent m_closed = new UnityEvent();

        public virtual void Init(UIManager uiManager, params object[] args)
        {
            m_uiManager = uiManager;
            m_args = args;
        }
        public virtual void SubscribeEvents()
        {
            if (!m_skin)
            {
                return;
            }
            foreach (var item in m_skin.GetComponentsInChildren<Button>(true))
            {
                item.onClick.AddListener(() =>
                {
                    RegisterButtonEvent(item.name);
                });
            }
            foreach (var item in m_skin.GetComponentsInChildren<Slider>(true))
            {
                item.onValueChanged.AddListener((value) =>
                {
                    RegisterSliderEvent(item.name, value);
                });
            }
            foreach (var item in m_skin.GetComponentsInChildren<Toggle>(true))
            {
                item.onValueChanged.AddListener((value) =>
                {
                    RegisterToggleEvent(item.name, value);
                });
            }

        }
        public virtual void UnsubscribeEvent()
        {
        }
        public virtual void OnShowing()
        {
            m_showing.Invoke();
        }
        public virtual void OnClosing()
        {
            m_closing.Invoke();
        }
        //protected virtual void OnShowed()
        //{
        //    m_showed.Invoke();
        //}
        //protected virtual void OnClosed()
        //{
        //    m_closed.Invoke();
        //}
        protected abstract void RegisterButtonEvent(string name);
        protected abstract void RegisterSliderEvent(string name, float value);
        protected abstract void RegisterToggleEvent(string name, bool value);
        protected virtual void OnDestroy()
        {
            UnsubscribeEvent();
        }
        protected virtual void OnDisable()
        {
            UnsubscribeEvent();
        }
    }
}