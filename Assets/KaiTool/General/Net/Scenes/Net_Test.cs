using KaiTool.Utilities;
using SeperateUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KaiTool.Net
{
    public class Net_Test : GameEventMonoBehaviour
    {
        [SerializeField]
        private Button m_btn_connect;
        [SerializeField]
        private Text m_message_text;


        public void PlayerTryConnect()
        {
            //EventManager.LocalInvoke(EventType.PlayerTryConnect,"192.168.1.123","4242");
        }

        protected override void SubscribeEvent()
        {
            //EventManager.AddListener<string, string>(EventType.PlayerTryConnect, OnPlayerTryConnect);
            //EventManager.AddListener<int>(EventType.PlayerLinkSucceed, OnPlayerLinkSucceed);
            //EventManager.AddListener(EventType.PlayerLinkFail, OnPlayerLinkFailed);

        }

        protected override void UnsubscribeEvent()
        {
            //EventManager.RemoveListener<string, string>(EventType.PlayerTryConnect, OnPlayerTryConnect);
            //EventManager.RemoveListener<int>(EventType.PlayerLinkSucceed, OnPlayerLinkSucceed);
            //EventManager.RemoveListener(EventType.PlayerLinkFail, OnPlayerLinkFailed);
        }

        private void OnPlayerTryConnect(string ip, string port) {
            m_message_text.text = "登陆中";
        }
        private void OnPlayerLinkSucceed(int id) {
            m_message_text.text = "登陆成功";
        }
        private void OnPlayerLinkFailed() {
            m_message_text.text = "登陆失败";
        }
    }
}