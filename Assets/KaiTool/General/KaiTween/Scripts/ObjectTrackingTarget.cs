using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.Motion
{
    public struct ObjectTrackingTargetEventArgs
    {
        public Transform target;
    }
    public class ObjectTrackingTarget : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField]
        private Transform m_target;

        private Vector3 m_relativePos = Vector3.zero;
        private Quaternion m_originalObjectRotation;
        private Quaternion m_originalTargetRotation;

        [Header("Events")]
        [SerializeField]
        private UnityEvent m_startTracking;
        [SerializeField]
        private UnityEvent m_stopTracking;

        private float m_intervalTime = 0.02f;
        private Coroutine m_trackingCoroutine;

        public Action<System.Object, ObjectTrackingTargetEventArgs> m_startTrackingEventHandle;
        public Action<System.Object, ObjectTrackingTargetEventArgs> m_stopTrackingEventHandle;

        private void Start()
        {
            if (m_target != null)
            {
                var args = new ObjectTrackingTargetEventArgs();
                args.target = m_target;
                OnStartTracking(args);
            }
        }

        public void OnStartTracking(ObjectTrackingTargetEventArgs e)
        {
            if (m_trackingCoroutine == null)
            {
                SetStatus(e);
                if (m_startTracking != null)
                {
                    m_startTracking.Invoke();
                }
                if (m_startTrackingEventHandle != null)
                {
                    m_startTrackingEventHandle(this, e);
                }
                m_trackingCoroutine = StartCoroutine(TrackingEnumerator());
            }
        }
        public void OnStopTracking(ObjectTrackingTargetEventArgs e)
        {
            if (m_trackingCoroutine != null)
            {
                if (m_stopTracking != null)
                {
                    m_stopTracking.Invoke();
                }
                if (m_stopTrackingEventHandle != null)
                {
                    m_stopTrackingEventHandle(this, e);
                }
                StopCoroutine(m_trackingCoroutine);
            }
        }
        private void SetStatus(ObjectTrackingTargetEventArgs e)
        {
            m_target = e.target;
            m_relativePos = transform.position - m_target.position;
            m_originalObjectRotation = transform.rotation;
            m_originalTargetRotation = m_target.rotation;
        }
        private IEnumerator TrackingEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            while (true)
            {
                KeepTracking();
                yield return wait;
            }
        }
        private void KeepTracking()
        {
            transform.rotation = m_target.rotation * Quaternion.Inverse(m_originalTargetRotation) * m_originalObjectRotation;
            transform.position = m_target.position + m_target.rotation * Quaternion.Inverse(m_originalTargetRotation) * m_relativePos;
        }
        private void Reset()
        {
            m_target = null;
        }
    }
}