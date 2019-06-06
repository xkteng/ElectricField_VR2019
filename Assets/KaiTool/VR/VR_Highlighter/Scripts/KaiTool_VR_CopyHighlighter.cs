using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.VR.Highlighter
{
    public class KaiTool_VR_CopyHighlighter : KaiTool_VR_BasicHighlighter
    {
        [SerializeField]
        private Material m_highlight_Mat;
        private GameObject m_highlightObject = null;

        public override void Highlight()
        {
            base.Highlight();
            HighlightObject.SetActive(true);
        }
        public override void Unhighlight()
        {
            base.Unhighlight();
            HighlightObject.SetActive(false);
        }

        private GameObject HighlightObject
        {
            get
            {
                if (m_highlightObject == null)
                {
                    m_highlightObject = Instantiate(gameObject, transform.position, transform.rotation, transform);
                    m_highlightObject.transform.localScale = Vector3.one;
                    var components = m_highlightObject.GetComponentsInChildren<Component>();
                    foreach (var item in components)
                    {
                        switch (item.GetType().ToString())
                        {
                            case "UnityEngine.Transform":
                            case "UnityEngine.MeshFilter":
                                break;
                            case "UnityEngine.MeshRenderer":
                                ((MeshRenderer)item).material = m_highlight_Mat;
                                break;
                            default:
                                if (Application.IsPlaying(this))
                                {
                                    Destroy(item);
                                }
                                else
                                {
                                    DestroyImmediate(item);
                                }
                                break;
                        }
                    }
                }
                return m_highlightObject;
            }
        }
        protected override void Awake()
        {
            base.Awake();
            InitVar();
        }
        private void InitVar()
        {
            if (m_highlight_Mat == null)
            {
                var path = "Materials/Highlight_Orange";
                m_highlight_Mat = Resources.Load<Material>(path);
                if (m_highlight_Mat == null)
                {
                    throw new Exception("There is no material at the path : " + path);
                }
            }
        }
    }
}