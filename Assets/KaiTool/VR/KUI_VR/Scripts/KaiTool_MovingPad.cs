using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRTK;

namespace KaiTool.VR.UI
{
    public struct KaiTool_AxisChangeEventArgs
    {
        public Vector2 m_axis;
    }
    public class KaiTool_MovingPad : MonoBehaviour
    {
        [Header("PlayerRig")]
        [SerializeField]
        private Transform m_playerRig;
        [Header("Mesh")]
        [SerializeField]
        private Transform m_movingPad;
        [Header("Status")]
        [SerializeField]
        [Range(0, 2)]
        private float m_deadZone = 1f;
        [SerializeField]
        [Range(0, 4)]
        private float m_maxZone = 2f;
        [SerializeField]
        [Range(0, 0.5f)]
        private float m_head2CameraDistance = 0.1f;
        [SerializeField]
        private AnimationCurve m_movingCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [Header("Events")]
        [SerializeField]
        private UnityEvent m_startAxisChange;
        [SerializeField]
        private UnityEvent m_axisChanging;
        [SerializeField]
        private UnityEvent m_stopAxisChange;

        public Action<System.Object, KaiTool_AxisChangeEventArgs> m_startAxisChangeEventHandle;
        public Action<System.Object, KaiTool_AxisChangeEventArgs> m_AxisChangingEventHandle;
        public Action<System.Object, KaiTool_AxisChangeEventArgs> m_stopAxisChangeEventHandle;
        private Transform m_headsetCamera;

        //[SerializeField]
        private Vector2 m_axis = Vector2.zero;
        private Vector2 m_lastRecordedAxis = Vector2.zero;
        public Vector2 Axis
        {
            get
            {
                return m_axis;
            }
        }
        private void Start()
        {
            StartCoroutine(Init());
        }
        private IEnumerator Init()
        {
            yield return InitVar();
            yield return InitEvent();
        }
        private IEnumerator InitVar()
        {
            if (m_playerRig == null)
            {
                m_playerRig = transform.root;
            }
            while (m_headsetCamera == null)
            {
                m_headsetCamera = VRTK_DeviceFinder.HeadsetCamera();
                yield return null;
            }
        }
        private IEnumerator InitEvent()
        {
            yield return null;
        }
        public void OnStartAxisChange(KaiTool_AxisChangeEventArgs e)
        {
            if (m_startAxisChange != null)
            {
                m_startAxisChange.Invoke();
            }
            if (m_startAxisChangeEventHandle != null)
            {
                m_startAxisChangeEventHandle(this, e);
            }
        }
        public void OnAxisChanging(KaiTool_AxisChangeEventArgs e)
        {
            if (m_axisChanging != null)
            {
                m_axisChanging.Invoke();
            }
            if (m_AxisChangingEventHandle != null)
            {
                m_AxisChangingEventHandle(this, e);
            }
        }
        public void OnStopAxisChange(KaiTool_AxisChangeEventArgs e)
        {
            if (m_stopAxisChange != null)
            {
                m_stopAxisChange.Invoke();
            }
            if (m_stopAxisChangeEventHandle != null)
            {
                m_stopAxisChangeEventHandle(this, e);
            }
        }
        private void OnValidate()
        {
            if (m_movingPad != null && m_deadZone != 0f)
            {
                var times = (m_deadZone / (m_movingPad.lossyScale.x / 2));
                var localScale = m_movingPad.localScale;
                m_movingPad.transform.localScale = new Vector3(times * localScale.x, localScale.y, times * localScale.z);

            }
            m_movingCurve.keys[0].value = 0f;
            m_movingCurve.keys[m_movingCurve.keys.Length - 1].value = 1f;
        }
        private Vector2 CalculateAxis()
        {
            var project = GetVectorProjectedOnGround();
            var axisDir = project.normalized;
            var axisLength = project.magnitude - m_deadZone;
            axisLength = axisLength < 0 ? 0 : axisLength;
            axisLength = axisLength > (m_maxZone - m_deadZone) ? (m_maxZone - m_deadZone) : axisLength;
            var coefficient = m_movingCurve.Evaluate(axisLength / (m_maxZone - m_deadZone));
            var x = Vector3.Dot(axisDir, transform.right) / axisDir.magnitude * coefficient;
            var y = Vector3.Dot(axisDir, transform.forward) / axisDir.magnitude * coefficient;
            return new Vector2(x, y);
        }
        private Vector3 GetVectorProjectedOnGround()
        {
            if (m_headsetCamera != null)
            {
                var direction = (m_headsetCamera.position - m_headsetCamera.forward * m_head2CameraDistance) - transform.position;
                var project = Vector3.ProjectOnPlane(direction, Vector3.up);
                return project;
            }
            else
            {
                return Vector3.zero;
            }
        }
        private void KeepRotationWithCameraHeadset()
        {
            if (m_headsetCamera != null)
            {
                var cameraEuler = m_headsetCamera.eulerAngles;
                var yEuler = cameraEuler.y;
                transform.eulerAngles = new Vector3(0, yEuler, 0);
            }
        }
        private void FixedUpdate()
        {
            KeepRotationWithCameraHeadset();
            m_axis = CalculateAxis();
            if (m_axis != Vector2.zero && m_lastRecordedAxis == Vector2.zero)
            {
                var args = new KaiTool_AxisChangeEventArgs();
                args.m_axis = m_axis;
                OnStartAxisChange(args);
            }
            else if (m_axis == Vector2.zero && m_lastRecordedAxis != Vector2.zero)
            {
                var args = new KaiTool_AxisChangeEventArgs();
                args.m_axis = Vector2.zero;
                OnStopAxisChange(args);
            }

            if (m_axis != Vector2.zero)
            {
                var args = new KaiTool_AxisChangeEventArgs();
                args.m_axis = m_axis;
                OnAxisChanging(args);
            }
            Move();
            m_lastRecordedAxis = m_axis;
        }
        private void OnDrawGizmos()
        {
            if (m_axis != Vector2.zero)
            {
                Gizmos.color = Color.yellow;
                var distination = transform.position + transform.rotation * new Vector3(m_axis.x, 0, m_axis.y);
                Gizmos.DrawLine(transform.position, distination);
                Gizmos.DrawWireSphere(distination, 0.05f);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(m_headsetCamera.position, 0.05f);

            }
            else
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, 0.05f);

            }
        }
        public Vector3 Forward
        {
            get
            {
                return transform.forward;
            }
        }
        public Vector3 Right
        {
            get
            {
                return transform.right;
            }
        }

        private void Move()
        {
            var increment = Forward * m_axis.y + Right * m_axis.x;
            m_playerRig.transform.position += increment;
        }
    }
}