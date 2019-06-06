using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace KaiTool.PC.MouseInteraction
{
    public struct MouseDraggingTrackerEventArgs
    {

    }
    [RequireComponent(typeof(KaiTool_MouseInteractiveObject))]
    public class KaiTool_MouseDraggingTracker : MonoBehaviour
    {
        [Header("MouseDraggigTracker", order = 1)]
        public float m_intervalTime = 0.03f;
        private KaiTool_MouseInteractiveObject m_mouseInteractiveObject;
        private Transform m_cameraTransform;
        private Vector3 m_lastMouseWorldPos = Vector3.zero;
        private Rigidbody m_rigidbody;
        private bool m_isKinematicOrigin;
        public Action<UnityEngine.Object, MouseDraggingTrackerEventArgs> m_startTracking;
        public Action<UnityEngine.Object, MouseDraggingTrackerEventArgs> m_stayTracking;
        public Action<UnityEngine.Object, MouseDraggingTrackerEventArgs> m_stopTracking;

        public UnityEvent m_startTracking_UnityEvent;
        public UnityEvent m_stopTracking_UnityEvent;

        private Coroutine m_trackingCoroutine;

        private Vector3 m_lastRecordPosition;
        private Quaternion m_lastRecordRotation;
        private void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            m_mouseInteractiveObject = GetComponent<KaiTool_MouseInteractiveObject>();
            m_rigidbody = GetComponent<Rigidbody>();
            m_isKinematicOrigin = m_rigidbody.isKinematic;
            m_cameraTransform = Camera.main.transform;
            m_lastRecordPosition = transform.position;
            m_lastRecordRotation = transform.rotation;
        }
        private void InitEvent()
        {
            m_mouseInteractiveObject.LeftButtonPressed += (sender, e) =>
            {
                Vector3 Pos = Camera.main.WorldToScreenPoint(transform.position);
                m_lastMouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Pos.z));
                if (m_rigidbody != null)
                {
                    m_rigidbody.isKinematic = true;
                }
                OnStartTracking(new MouseDraggingTrackerEventArgs());
            };

            m_mouseInteractiveObject.LeftButtonReleased += (sender, e) =>
            {
                if (m_rigidbody != null)
                {
                    m_rigidbody.isKinematic = m_isKinematicOrigin;
                }

                OnStopTracking(new MouseDraggingTrackerEventArgs());
            };
        }
        /// <summary>
        /// Invoked to be forced to start tracking.
        /// </summary>
        /// <param name="e"></param>
        public void OnStartTracking(MouseDraggingTrackerEventArgs e)
        {
            if (m_trackingCoroutine != null)
            {
                StopCoroutine(m_trackingCoroutine);
            }
            m_trackingCoroutine = StartCoroutine(TrackingEnumerator());
            if (m_startTracking != null)
            {
                m_startTracking(this, e);
            }
            RecordPositionAndRotation();
            m_startTracking_UnityEvent.Invoke();
        }
        /// <summary>
        /// Invoked to be forced to stop tracking
        /// </summary>
        /// <param name="e"></param>
        private void OnStayTracking(MouseDraggingTrackerEventArgs e)
        {
            if (m_stayTracking != null)
            {
                m_stayTracking(this, e);
            }
        }
        public void OnStopTracking(MouseDraggingTrackerEventArgs e)
        {
            if (m_trackingCoroutine != null)
            {
                StopCoroutine(m_trackingCoroutine);
            }
            if (m_stopTracking != null)
            {
                m_stopTracking(this, e);
            }
            // KaiTool_CommandManager.Instance.ExecuteCommand(new KaiTool_MoveCommand(transform, m_lastRecordPosition, m_lastRecordRotation, transform.position, transform.rotation));
            m_stopTracking_UnityEvent.Invoke();
        }
        private void RecordPositionAndRotation()
        {
            m_lastRecordPosition = transform.position;
            m_lastRecordRotation = transform.rotation;
        }
        private IEnumerator TrackingEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            while (true)
            {
                Vector3 Pos = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 currentmousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Pos.z));
                var deltaVec = currentmousePos - m_lastMouseWorldPos;
                transform.position += deltaVec;
                m_lastMouseWorldPos = currentmousePos;
                OnStayTracking(new MouseDraggingTrackerEventArgs());
                yield return wait;
            }
        }
    }
}