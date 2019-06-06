using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeperateUI
{
    public class TopPanel : PanelBase
    {
        public override void Init(UIManager uiManager, params object[] args)
        {
            base.Init(uiManager, args);
            m_layer = 0;
            m_skinPath = "TopPanel";
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