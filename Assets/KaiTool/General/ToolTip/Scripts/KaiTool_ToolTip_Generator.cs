using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaiTool.ToolTip;
using DG.Tweening;
using DG.Tweening.Core;

namespace KaiTool.ToolTip
{
    public class KaiTool_ToolTip_Generator : MonoBehaviour
    {
        private const float DESTROY_DELAY = 3f;
        [SerializeField]
        private string m_text = "";
        [SerializeField]
        private int m_fontSize = 50;
        [SerializeField]
        private string m_path = "UI/ToolTip/ToolTip_Small";
        [SerializeField]
        private Transform m_anchor = null;

        private bool m_generated = false;
        private Transform m_cameraTransform;
        private GameObject m_toolTipObject;

        private Transform CameraTransform
        {
            get
            {
                if (m_cameraTransform == null)
                {
                    m_cameraTransform = Camera.main.transform;//VRTK.VRTK_DeviceFinder.HeadsetCamera();
                }
                return m_cameraTransform;
            }
        }
        public bool Generated
        {
            get
            {
                return m_generated;
            }
        }
        protected virtual void Awake()
        {
            if (m_anchor == null)
            {
                m_anchor = transform;
            }
        }
        [ContextMenu("CreateToolTip")]
        public GameObject CreateToolTip()
        {
            if (m_toolTipObject == null/* && m_isValid*/)
            {
                if (CameraTransform)
                {
                    m_toolTipObject = Instantiate(Resources.Load<GameObject>(m_path), m_anchor.position, Quaternion.Euler(0, CameraTransform.eulerAngles.y, 0));
                    m_toolTipObject.transform.SetParent(m_anchor.root, true);
                }
                else
                {
                    m_toolTipObject = Instantiate(Resources.Load<GameObject>(m_path), m_anchor.position, Quaternion.identity);
                    m_toolTipObject.transform.SetParent(m_anchor.root, true);
                }
                m_toolTipObject.GetComponent<KaiTool_ToolTip>().Show(m_text, m_fontSize);
                //m_isValid = false;
                m_generated = true;
                return m_toolTipObject;
            }
            return null;
        }
        [ContextMenu("DestroyToolTip")]
        public Tweener DestroyToolTip()
        {
            var y = 0;
            return DOTween.To(() => y, (value) => y = value, 0, DESTROY_DELAY).OnComplete(() =>
            {
                if (m_toolTipObject)
                {
                    var toolTip = m_toolTipObject.GetComponent<KaiTool_ToolTip>();
                    toolTip.Hide();
                    Destroy(m_toolTipObject.gameObject, toolTip.FadingDuration);
                    m_toolTipObject = null;
                    var x = 0;
                    DOTween.To(() => x, (value) => x = value, 0, toolTip.FadingDuration).OnComplete(() =>
                     {
                         m_generated = false;
                     });
                }
            });
        }
        public void DestroyToolTipImmediately()
        {
            if (m_toolTipObject)
            {
                Destroy(m_toolTipObject.gameObject, 0f);
                m_generated = false;
            }
        }
        public void SetText(string text)
        {
            m_text = text;
            if (m_toolTipObject)
            {
                m_toolTipObject.GetComponent<KaiTool_ToolTip>().Message = text;
            }
        }
        public void SetFontSize(int size)
        {
            m_fontSize = size;
            if (m_toolTipObject)
            {
                m_toolTipObject.GetComponent<KaiTool_ToolTip>().Text.fontSize = size;
            }
        }
    }
}

