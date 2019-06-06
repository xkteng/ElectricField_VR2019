//***********************************************
//By Kai
//Description:Scene0 Manager
//
//
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Utilities
{
    public class Scene0_Manager : GameEventMonoBehaviour
    {
        private GameObject m_shape = null;

        [SerializeField]
        private Transform m_spawn;
        protected override void SubscribeEvent()
        {
            Scene0_Event.LoadShape.AddListener(OnLoadShape);
            Scene0_Event.ClearGameObject.AddListener(OnClearGameObject);
        }

        protected override void UnsubscribeEvent()
        {
            Scene0_Event.LoadShape.RemoveListener(OnLoadShape);
            Scene0_Event.ClearGameObject.RemoveListener(OnClearGameObject);
        }
        private void OnLoadShape(string name)
        {
            OnClearGameObject();
            m_shape = Manager.Instance.LoadGameObject(name, m_spawn);
        }

        private void OnClearGameObject()
        {
            if (m_shape)
            {
                DestroyImmediate(m_shape);
                m_shape = null;
            }
        }
    }
}