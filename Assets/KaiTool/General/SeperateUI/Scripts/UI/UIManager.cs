using KaiTool.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace SeperateUI
{
    public class PanelEvent : UnityEvent<PanelBase>
    {

    }
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_canvas_Overlay;
        [SerializeField]
        private SameLevelPanelGroup[] m_panelGroups;
        public Dictionary<string, PanelBase> m_panelDict = new Dictionary<string, PanelBase>();
        private Dictionary<int, Transform> m_layerDict = new Dictionary<int, Transform>();

        public PanelEvent m_openPanel = new PanelEvent();
        public PanelEvent m_closePanel = new PanelEvent();
        #region PRIVATE_METHOD
        protected virtual void Awake()
        {
            InitEvent();
        }
        private void InitEvent()
        {
            m_openPanel.AddListener(CloseSameLevelPanels);
        }
        private void CloseSameLevelPanels(PanelBase panel)
        {
            if (m_panelGroups != null)
            {
                var level = int.MaxValue;
                var panelName = panel.GetType().ToString();
                foreach (var group in m_panelGroups)
                {
                    if (!group.m_typeNames.Contains(panelName))
                    {
                        continue;
                    }
                    else
                    {
                        level = Mathf.Min(level, group.m_level);
                    }
                }
                foreach (var group in m_panelGroups)
                {
                    if (group.m_level >= level)
                    {
                        foreach (var otherName in group.m_typeNames)
                        {
                            if (otherName != panelName)
                            {
                                this.GetType().GetMethod("ClosePanel").MakeGenericMethod(new Type[] { Type.GetType(otherName) })
                                    .Invoke(this, null);
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region PUBLIC_METHOD
        public PanelBase OpenPanel<T>(string skinPath = "", params object[] args) where T : PanelBase
        {
            string name = typeof(T).ToString();
            if (m_panelDict.ContainsKey(name))
            {
                return null;
            }
            var panel = gameObject.AddComponent<T>();
            panel.Init(this, args);
            m_panelDict.Add(name, panel);
            skinPath = (skinPath != "" ? skinPath : panel.m_skinPath);
            var skin = Resources.Load<GameObject>(skinPath);
            if (!skin)
            {
                Debug.LogError("UIManager failed to load skin.The skin path is " + "'" + skinPath + "'");
            }
            m_openPanel.Invoke(panel);
            panel.m_skin = Instantiate(skin);
            panel.SubscribeEvents();
            Transform skinTransform = panel.m_skin.transform;
            if (!m_layerDict.ContainsKey(panel.m_layer))
            {
                var layerIndex = panel.m_layer;
                var new_layerObject = Instantiate(Resources.Load<GameObject>("UI/LayerRoot"), m_canvas_Overlay.transform);
                new_layerObject.name = layerIndex.ToString();
                new_layerObject.transform.SetParent(m_canvas_Overlay.transform);
                new_layerObject.transform.SetAsFirstSibling();
                m_layerDict.Add(panel.m_layer, new_layerObject.transform);
                for (int i = 0; i < m_canvas_Overlay.transform.childCount; i++)
                {
                    var tempIndex = int.Parse(m_canvas_Overlay.transform.GetChild(i).name);
                    if (tempIndex < panel.m_layer)
                    {
                        new_layerObject.transform.SetSiblingIndex(i);
                    }
                }
            }
            skinTransform.SetParent(m_layerDict[panel.m_layer], false);
            panel.OnShowing();
            return panel;
        }
        public void ClosePanel<T>()
        {
            var name = typeof(T).ToString();
            if (!m_panelDict.ContainsKey(name))
            {
                return;
            }
            var panel = m_panelDict[name];
            DestroyImmediate(panel.m_skin);
            m_panelDict.Remove(name);
            panel.OnClosing();
            m_closePanel.Invoke(panel);
            /* Component.*/
            Destroy(panel);
        }
        private void ClosePanel(Type type)
        {
            var name = type.ToString();
            if (!m_panelDict.ContainsKey(name))
            {
                return;
            }
            var panel = m_panelDict[name];
            DestroyImmediate(panel.m_skin);
            m_panelDict.Remove(name);
            panel.OnClosing();
            m_closePanel.Invoke(panel);
            Destroy(panel);
        }

        public void CloseAllPanel()
        {
            var keys = m_panelDict.Keys;
            foreach (var item in keys)
            {
                var panel = m_panelDict[item];
                DestroyImmediate(panel.m_skin);
                panel.OnClosing();
                m_closePanel.Invoke(panel);
                Destroy(panel);
            }
            m_panelDict.Clear();
        }

        #endregion
    }
    [Serializable]
    public class SameLevelPanelGroup
    {
        public int m_level;
        public string[] m_typeNames;
    }
}
