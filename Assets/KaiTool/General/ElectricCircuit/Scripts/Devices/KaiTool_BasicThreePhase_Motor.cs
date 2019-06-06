using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.ElectricCircuit
{

    public enum ThreePhaseMotorState
    {
        Forward,
        Backward,
        Idle
    }
    public struct ThreePhaseMotorEventArgs
    {

    }
    public class KaiTool_BasicThreePhase_Motor : KaiTool_BasicThreePhaseCircuitDevice
    {
        [SerializeField]
        private ThreePhaseMotorState m_currentState = ThreePhaseMotorState.Idle;

        public Action<System.Object, ThreePhaseMotorEventArgs> m_playForwardEventHandle;
        public Action<System.Object, ThreePhaseMotorEventArgs> m_stopForwardEventHandle;

        public Action<System.Object, ThreePhaseMotorEventArgs> m_playBackwardEventHandle;
        public Action<System.Object, ThreePhaseMotorEventArgs> m_stopBackwardEventHandle;


        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {

        }
        private void InitEvent()
        {

        }

        private void OnPlayForward(ThreePhaseMotorEventArgs e)
        {
            switch (m_currentState)
            {
                case ThreePhaseMotorState.Idle:
                    if (m_playForwardEventHandle != null)
                    {
                        m_playForwardEventHandle(this, e);
                    }
                    m_currentState = ThreePhaseMotorState.Forward;
                    break;
                case ThreePhaseMotorState.Forward:

                    break;
                case ThreePhaseMotorState.Backward:
                    break;
            }

        }
        private void OnStopForward(ThreePhaseMotorEventArgs e)
        {
            if (m_stopForwardEventHandle != null)
            {
                m_stopForwardEventHandle(this, e);
            }
            switch (m_currentState)
            {
                case ThreePhaseMotorState.Idle:
                    break;
                case ThreePhaseMotorState.Forward:
                    break;
                case ThreePhaseMotorState.Backward:
                    break;
            }
        }
        private void OnPlayBackward(ThreePhaseMotorEventArgs e)
        {
            if (m_playBackwardEventHandle != null)
            {
                m_playBackwardEventHandle(this, e);
            }
            switch (m_currentState)
            {
                case ThreePhaseMotorState.Idle:
                    break;
                case ThreePhaseMotorState.Forward:
                    break;
                case ThreePhaseMotorState.Backward:
                    break;
            }
        }
        private void OnStopBackward(ThreePhaseMotorEventArgs e)
        {
            if (m_stopBackwardEventHandle != null)
            {
                m_stopBackwardEventHandle(this, e);
            }
            switch (m_currentState)
            {
                case ThreePhaseMotorState.Idle:
                    break;
                case ThreePhaseMotorState.Forward:
                    break;
                case ThreePhaseMotorState.Backward:
                    break;
            }
        }


    }
}