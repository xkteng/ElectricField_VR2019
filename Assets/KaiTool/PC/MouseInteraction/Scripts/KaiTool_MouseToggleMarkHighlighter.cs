using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.PC.MouseInteraction
{
    public class KaiTool_MouseToggleMarkHighlighter : KaiTool_BasicMouseHighlighter
    {
        [Header("ToggleMark")]
        [SerializeField]
        private GameObject m_mark;
        protected override void Init()
        {
            base.Init();
        }
        protected override void OnHighlighted(HighlighterEventArgs e)
        {
            base.OnHighlighted(e);
            ToggleMark(true);
        }
        protected override void OnUnhighlighted(HighlighterEventArgs e)
        {
            base.OnUnhighlighted(e);
            ToggleMark(false);
        }
        private void ToggleMark(bool toggle) {
            m_mark.SetActive(toggle);
        }
        private void OnValidate()
        {
            ToggleMark(false);
        }
    }
}