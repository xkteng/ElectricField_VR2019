using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using VRTK;

namespace KaiTool.VR
{
    public enum DetectType
    {
        Headset,
        LeftHand,
        RightHand
    }
    public struct DetectorEventArgs
    {

    }
    /// <summary>
    /// Put this script on portal Crystal ball.
    /// </summary>
    public class KaiTool_DeviceDetector : MonoBehaviour
    {
        private const float INTERVALTIME = 0.03f;
        [SerializeField]
        private DetectType m_detectType = DetectType.Headset;
        [SerializeField]
        [Range(0f, 0.5f)]
        private float m_radius = 0.2f;
        [Header("Events")]
        [SerializeField]
        private UnityEvent m_triggerEnter;
        [SerializeField]
        private UnityEvent m_triggerStay;
        [SerializeField]
        private UnityEvent m_triggerExit;
        [Header("Gizmos")]
        [SerializeField]
        private bool m_isDrawGizmos = true;
        [SerializeField]
        private Color m_gizmosColor = Color.red;


        public event Action<System.Object, DetectorEventArgs> m_triggerEnterEventHandle;
        public event Action<System.Object, DetectorEventArgs> m_triggerStayEventHandle;
        public event Action<System.Object, DetectorEventArgs> m_triggerExitEventHandle;

        private bool m_isEntered = false;
        private float m_distance = Mathf.Infinity;
        private Transform m_target;

        private void Awake()
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
            while (m_target == null)
            {
                switch (m_detectType)
                {
                    case DetectType.Headset:
                        m_target = VRTK_DeviceFinder.HeadsetCamera();
                        break;
                    case DetectType.LeftHand:
                        m_target = VRTK_DeviceFinder.GetControllerLeftHand().transform;
                        break;
                    case DetectType.RightHand:
                        m_target = VRTK_DeviceFinder.GetControllerRightHand().transform;
                        break;
                }
                yield return null;
            }
        }
        private IEnumerator InitEvent()
        {
            yield return null;
        }

        private void OnPortalTriggerEnter(DetectorEventArgs e = new DetectorEventArgs())
        {
            if (m_triggerEnter != null)
            {
                m_triggerEnter.Invoke();
            }
            if (m_triggerEnterEventHandle != null)
            {
                m_triggerEnterEventHandle(this, e);
            }
            m_isEntered = true;
        }
        private void OnPortalTriggerStay(DetectorEventArgs e = new DetectorEventArgs())
        {
            if (m_triggerStay != null)
            {
                m_triggerStay.Invoke();
            }
            if (m_triggerStayEventHandle != null)
            {
                m_triggerStayEventHandle(this, e);
            }
            m_distance = (m_target.position - transform.position).magnitude;
        }
        private void OnPortalTriggerExit(DetectorEventArgs e = new DetectorEventArgs())
        {
            if (m_triggerExit != null)
            {
                m_triggerExit.Invoke();
            }
            if (m_triggerExitEventHandle != null)
            {
                m_triggerExitEventHandle(this, e);
            }
            m_distance = Mathf.Infinity;
            m_isEntered = false;
        }


        private void OnEnable()
        {
            StartCoroutine(JudgeEnumerator());
        }

        private IEnumerator JudgeEnumerator()
        {
            var wait = new WaitForSeconds(INTERVALTIME);
            while (true)
            {
                if (!m_isEntered)
                {
                    var tempDistance = (m_target.position - transform.position).magnitude;
                    if (tempDistance < m_radius)
                    {
                        var args = new DetectorEventArgs();
                        OnPortalTriggerEnter(args);
                    }
                }
                else
                {
                    var tempDistance = (m_target.position - transform.position).magnitude;
                    if (tempDistance <= m_radius)
                    {
                        var args = new DetectorEventArgs();
                        OnPortalTriggerStay(args);
                    }
                    else
                    {
                        var args = new DetectorEventArgs();
                        OnPortalTriggerExit(args);
                    }
                }
                yield return wait;
            }
        }

        private void OnDrawGizmos()
        {
            if (m_isDrawGizmos)
            {
                Gizmos.color = m_gizmosColor;
                Gizmos.DrawWireSphere(transform.position, m_radius);
            }
        }
    }
}