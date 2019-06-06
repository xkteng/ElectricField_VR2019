using System.Collections;
using System.Collections.Generic;
using KaiTool.PC.Absorption;
using KaiTool.VR.Highlighter;
using UnityEngine;
namespace KaiTool.VR.Absorption
{
    public class KaiTool_VR_HighlightAbsorber : KaiTool_VR_GenerializedAbsorber
    {
        private KaiTool_VR_BasicHighlighter m_highlighter;
        private MeshRenderer[] m_meshRenderers;

        protected override void Init()
        {
            base.Init();
            InitVar();
        }
        private void InitVar()
        {
            m_highlighter = GetComponent<KaiTool_VR_BasicHighlighter>();
            m_meshRenderers = GetComponentsInChildren<MeshRenderer>();
        }
        public override void OnAbsorbbing(Object sender, AbsorberEventArgs e)
        {
            base.OnAbsorbbing(sender, e);
            ToggleAllMeshRenderers(false);
        }
        public override void OnReleasing(Object sender, AbsorberEventArgs e)
        {
            base.OnReleasing(sender, e);
            ToggleAllMeshRenderers(true);
        }
        public override void OnHoveringIn(Object sender, AbsorberEventArgs e)
        {
            base.OnHoveringIn(sender, e);
            m_highlighter.Highlight();
        }
        public override void OnHoveringOut(Object sender, AbsorberEventArgs e)
        {
            base.OnHoveringOut(sender, e);
            m_highlighter.Unhighlight();
        }
        private void ToggleAllMeshRenderers(bool toggle)
        {
            foreach (var item in m_meshRenderers)
            {
                item.enabled = toggle;
            }
        }

    }
}