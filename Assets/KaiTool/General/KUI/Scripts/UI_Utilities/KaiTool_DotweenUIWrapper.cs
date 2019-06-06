using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.UI
{
    public class KaiTool_DotweenUIWrapper : MonoBehaviour
    {
        [SerializeField]
        private float m_maxDelay = 0f;
        [SerializeField]
        private float m_duration = 0.2f;
        [SerializeField]
        private bool m_isInverse = false;



        private void Reset()
        {
            SetUp();
        }
        private void OnValidate()
        {
            SetUp();
        }
        [ContextMenu("SetUp")]
        public void SetUp()
        {

            var UIObjects = GetComponentsInChildren<KaiTool_DotweenUIObject>();
            var length = UIObjects.Length;
            var deltaDelay = m_maxDelay / (length - 1);
            foreach (var item in UIObjects)
            {
                item.Duration = m_duration;
            }
            if (!m_isInverse)
            {
                for (int i = 0; i < length; i++)
                {
                    UIObjects[i].ShowDelay = (length - i - 1) * deltaDelay;
                    UIObjects[i].HideDelay = i * deltaDelay;
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    UIObjects[i].ShowDelay = i * deltaDelay;
                    UIObjects[i].HideDelay = (length - i - 1) * deltaDelay;
                }
            }
        }
    }
}