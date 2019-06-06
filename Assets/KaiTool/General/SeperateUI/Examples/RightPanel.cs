using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeperateUI
{
    public class RightPanel : PanelBase
    {
        public override void Init(UIManager uiManager, params object[] args)
        {
            base.Init(uiManager, args);
            m_layer = 1;
            m_skinPath = "RightPanel";
        }
        protected override void RegisterButtonEvent(string name)
        {
        }

        protected override void RegisterSliderEvent(string name, float value)
        {
        }

        protected override void RegisterToggleEvent(string name, bool value)
        {
        }
    }
}