using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaiTool.Utilities;
using System;

namespace KaiTool.CameraManipulator
{
    public struct CameraEdgeSliderEventArgs
    {
        public Direction m_direction;
    }

    [Flags]
    public enum Direction
    {
        // None = 0,
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8,
    }
    public sealed class KaiTool_CameraEdgeSlider : MonoBehaviour
    {
        [Header("Slider")]
        [SerializeField]
        private Transform m_cameraTarget;
        [SerializeField]
        [Range(0, 1f)]
        private float m_cameraTargetTranslateSpeed = 0.2f;
        [SerializeField]
        [Range(0, 0.1f)]
        private float m_edgeRatio = 0.1f;
        [SerializeField]
        private Vector3 m_topDirection = Vector3.up;
        [SerializeField]
        private Vector3 m_rightDirection = Vector3.right;
        [EnumFlags]
        [SerializeField]
        private Direction m_currentDirection = (Direction)(0);
        [Header("Gizmos")]
        [SerializeField]
        private bool m_isDrawGizmos = true;
        [SerializeField]
        private float m_gizmosSize = 1f;
        [SerializeField]
        private Color m_gizmosColor = Color.green;

        public Action<System.Object, CameraEdgeSliderEventArgs> m_startSlide;
        public Action<System.Object, CameraEdgeSliderEventArgs> m_endSlide;

        private void OnStartSlide(CameraEdgeSliderEventArgs e)
        {
            if (m_startSlide != null)
            {
                m_startSlide(this, e);
            }
            // print("Start Slide");
        }
        private void OnEndSlide(CameraEdgeSliderEventArgs e)
        {
            if (m_endSlide != null)
            {
                m_endSlide(this, e);
            }
            //print("End Slide");
        }

        private Coroutine m_targetMotionCoroutine;

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            InitVar();
            InitComponent();
        }
        private void InitVar()
        {

        }
        private void InitComponent()
        {
            // Camera.main.transform.LookAt(transform.parent.position);
        }

        private void Start()
        {
            m_targetMotionCoroutine = StartCoroutine(CameraTranslateEnumerator());
        }
        IEnumerator CameraTranslateEnumerator()
        {
            var intervalTime = 0.02f;
            WaitForSeconds wait = new WaitForSeconds(intervalTime);
            while (true)
            {
                var msPos = Input.mousePosition;
                var widthBorder = Screen.width * m_edgeRatio;
                var heightBorder = Screen.height * m_edgeRatio;
                Vector3 deltaVec = Vector3.zero;
                var lastDirection = m_currentDirection;
                m_currentDirection = 0;
                if (msPos.y > Screen.height - heightBorder)
                {
                    var tempAmount = (msPos.y - (Screen.height - heightBorder));
                    deltaVec += tempAmount * m_topDirection;
                    m_currentDirection = m_currentDirection | Direction.Up;
                    //m_currentDirection = Direction.Up;
                }
                if (msPos.y < heightBorder)
                {
                    var tempAmount = (msPos.y - heightBorder);
                    deltaVec += tempAmount * m_topDirection;
                    m_currentDirection = m_currentDirection | Direction.Down;
                    //m_currentDirection = Direction.Down;
                }
                if (msPos.x > Screen.width - widthBorder)
                {
                    var tempAmount = (msPos.x - (Screen.width - widthBorder));
                    deltaVec += tempAmount * m_rightDirection;
                    m_currentDirection = m_currentDirection | Direction.Right;
                    //m_currentDirection = Direction.Right;
                }
                if (msPos.x < widthBorder)
                {
                    var tempAmount = (msPos.x - widthBorder);
                    deltaVec += tempAmount * m_rightDirection;
                    m_currentDirection = m_currentDirection | Direction.Left;
                    //m_currentDirection = Direction.Left;
                }

                var common = m_currentDirection & lastDirection;
                var newStart = m_currentDirection & (~common);//(Direction)((1 << (int)m_currentDirection) - (1 << (int)common));
                var newEnd = lastDirection & (~common); //(Direction)((1 << (int)lastDirection) - (1 << (int)common));
                if (newStart != 0)
                {
                    var startArgs = new CameraEdgeSliderEventArgs();
                    startArgs.m_direction = newStart;
                    OnStartSlide(startArgs);
                }
                if (newEnd != 0)
                {
                    var endArgs = new CameraEdgeSliderEventArgs();
                    endArgs.m_direction = newEnd;
                    OnEndSlide(endArgs);

                }
                deltaVec *= m_cameraTargetTranslateSpeed * intervalTime;

                //   Camera.main.transform.position += deltaVec;

                m_cameraTarget.parent.position = Vector3.Lerp(m_cameraTarget.parent.position, m_cameraTarget.parent.position + deltaVec, 0.8f);
                yield return wait;
            }
        }

        public void FocusToTarget(Transform target)
        {
            FocusToTarget(target.position);
        }

        public void FocusToTarget(Vector3 targetPosition)
        {
            m_cameraTarget.parent.position = targetPosition;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FocusToTarget(Vector3.up);
            }
        }

        private void OnDrawGizmos()
        {
            if (m_isDrawGizmos)
            {
                Gizmos.color = m_gizmosColor;
                Gizmos.DrawWireSphere(m_cameraTarget.position, 0.1f * m_gizmosSize);
                var top = m_cameraTarget.position + m_topDirection * 3f * m_gizmosSize;
                Gizmos.DrawLine(m_cameraTarget.position, top);
                Gizmos.DrawWireSphere(top, 0.1f * m_gizmosSize);
                var right = m_cameraTarget.position + m_rightDirection * 3f * m_gizmosSize;
                Gizmos.DrawLine(m_cameraTarget.position, right);
                Gizmos.DrawWireSphere(right, 0.1f * m_gizmosSize);
            }
        }
    }
}