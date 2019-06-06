//***********************************************
//By Kai
//Description:加载过渡界面
//
//
//***********************************************
using SeperateUI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace KaiTool.Utilities
{
    public class Loading_Panel : PanelBase
    {
        private Text m_text;
        private Slider m_slider;
        public override void Init(UIManager uiManager, params object[] args)
        {
            base.Init(uiManager, args);
            m_skinPath = "UI/Panels/Loading_Panel";
            m_layer = 100;
        }
        public override void OnShowing()
        {
            base.OnShowing();
            m_text = m_skin.transform.Find("Percent_Text").GetComponent<Text>();
            m_slider = m_skin.transform.Find("Loading_Slider").GetComponent<Slider>();

        }
        public override void SubscribeEvents()
        {
            base.SubscribeEvents();
            //GameEvent.UpdateLoadingProgress.AddListener(OnUpdateLoadingProgress);
            Manager.Instance.UpdateLoadingProgress.AddListener(OnUpdateLoadingProgress);

        }
        public override void UnsubscribeEvent()
        {
            base.UnsubscribeEvent();
            //GameEvent.UpdateLoadingProgress.RemoveListener(OnUpdateLoadingProgress);
            Manager.Instance.UpdateLoadingProgress.RemoveListener(OnUpdateLoadingProgress);
        }

        private void OnUpdateLoadingProgress(float progress)
        {
            m_slider.value = progress;
            m_text.text = new StringBuilder(((int)(progress * 100f)).ToString()).Append("%").ToString();
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