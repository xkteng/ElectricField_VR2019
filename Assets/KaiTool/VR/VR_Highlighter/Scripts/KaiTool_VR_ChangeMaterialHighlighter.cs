using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.VR.Highlighter
{
    public class KaiTool_VR_ChangeMaterialHighlighter : KaiTool_VR_BasicHighlighter
    {
        [SerializeField]
        private Material m_highlight_Mat = null;

        private Dictionary<MeshRenderer, Material[]> m_mat_Dic = new Dictionary<MeshRenderer, Material[]>();
        protected override void Awake()
        {
            base.Awake();
            InitVar();
        }
        private void InitVar()
        {
            if (m_highlight_Mat == null)
            {
                var path = "Materials/Green";
                m_highlight_Mat = Resources.Load<Material>(path);
                if (m_highlight_Mat == null)
                {
                    throw new Exception("There is no material at the path : " + path);
                }
            }
            var meshRenders = GetComponentsInChildren<MeshRenderer>();
            foreach (var item in meshRenders)
            {
                m_mat_Dic.Add(item, item.materials);
            }
        }
        public override void Highlight()
        {
            base.Highlight();
            foreach (var item in m_mat_Dic.Keys)
            {
                item.materials = new Material[] { m_highlight_Mat };
            }
        }
        public override void Unhighlight()
        {
            base.Unhighlight();
            foreach (var item in m_mat_Dic.Keys)
            {
                item.materials = m_mat_Dic[item];
            }
        }

    }
}