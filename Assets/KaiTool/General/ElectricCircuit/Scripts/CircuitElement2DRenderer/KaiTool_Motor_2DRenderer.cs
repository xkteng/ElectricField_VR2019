using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.ElectricCircuit
{
    public enum MotorState
    {
        Idle,
        Forward,
        Backward
    }
    public class KaiTool_Motor_2DRenderer : MonoBehaviour
    {
        [SerializeField]
        private KaiTool_Motor m_forwardMotor;
        [SerializeField]
        private KaiTool_Motor m_backwardMotor;
        [SerializeField]
        private DOTweenAnimation m_forwardAnimation;
        [SerializeField]
        private DOTweenAnimation m_backwardAnimation;
        [SerializeField]
        private MotorState m_currentState = MotorState.Idle;

        private bool m_isPlaying = false;

        public bool IsPlaying
        {
            get
            {
                return m_isPlaying;
            }
        }

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {

        }
        private void InitEvent()
        {
            if (m_forwardMotor)
            {
                m_forwardMotor.m_electrifiedEventHandle += (sender, e) =>
                {
                    //print("Electrified");
                    AttempToTransiteState(m_currentState, MotorState.Forward);
                };
                m_forwardMotor.m_unelectrifiedEventHandle += (sender, e) =>
                {
                    // print("Unelectrified");
                    AttempToTransiteState(m_currentState, MotorState.Idle);
                };
            }
            if (m_backwardMotor)
            {
                m_backwardMotor.Electrified.AddListener(() =>
                {
                    AttempToTransiteState(m_currentState, MotorState.Backward);
                });
                m_backwardMotor.Unelectrified.AddListener(() =>
                {
                    AttempToTransiteState(m_currentState, MotorState.Idle);
                });
                /*
                m_backwardMotor.m_electrifiedEventHandle += (sender, e) =>
                {
                    AttempToTransiteState(m_currentState, MotorState.Backward);
                };
                m_backwardMotor.m_unelectrifiedEventHandle += (sender, e) =>
                {
                    AttempToTransiteState(m_currentState, MotorState.Idle);
                };
                */
            }
        }

        private void RewindAllAnimation()
        {
            m_forwardAnimation.DORewind();
            m_backwardAnimation.DORewind();
        }
        private void PauseAllAnimation()
        {
            m_isPlaying = false;
            m_forwardAnimation.DOPause();
            m_backwardAnimation.DOPause();
        }

        public void ResetAllAnimation()
        {
            PauseAllAnimation();
            RewindAllAnimation();
        }
        public void PlayForwardAnimation()
        {
            PauseAllAnimation();
            RewindAllAnimation();
            m_forwardAnimation.DOPlayForward();
            m_isPlaying = true;
        }
        public void PlayBackwardAnimation()
        {
            PauseAllAnimation();
            RewindAllAnimation();
            m_backwardAnimation.DOPlayForward();
            m_isPlaying = true;
        }

        private void AttempToTransiteState(MotorState currentState, MotorState attemptState)
        {
            switch (currentState)
            {
                case MotorState.Idle:
                    switch (attemptState)
                    {
                        case MotorState.Forward:
                            m_currentState = MotorState.Forward;
                            PlayForwardAnimation();
                            break;
                        case MotorState.Backward:
                            m_currentState = MotorState.Backward;
                            PlayBackwardAnimation();
                            break;
                        case MotorState.Idle:
                            break;
                    }
                    break;
                case MotorState.Forward:
                    switch (attemptState)
                    {
                        case MotorState.Forward:
                            break;
                        case MotorState.Backward:
                            m_currentState = MotorState.Idle;
                            ResetAllAnimation();
                            break;
                        case MotorState.Idle:
                            m_currentState = MotorState.Idle;
                            ResetAllAnimation();
                            break;
                    }
                    break;
                case MotorState.Backward:
                    switch (attemptState)
                    {
                        case MotorState.Forward:
                            m_currentState = MotorState.Idle;
                            ResetAllAnimation();
                            break;
                        case MotorState.Backward:
                            break;
                        case MotorState.Idle:
                            m_currentState = MotorState.Idle;
                            ResetAllAnimation();
                            break;
                    }
                    break;
            }
        }

        private void OnDisable()
        {
            ResetAllAnimation();
            m_currentState = MotorState.Idle;
        }
    }
}