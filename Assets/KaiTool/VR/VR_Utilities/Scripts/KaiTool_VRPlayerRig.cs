using KaiTool.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace KaiTool.VR.VRUtilities
{
    public class KaiTool_VRPlayerRig : Singleton<KaiTool_VRPlayerRig>
    {
        [SerializeField]
        private bool m_isHaptic = true;
        [SerializeField]
        private KaiTool_BasicGazer m_gazer;
        private Transform m_headsetCamera;
        private Transform m_playArea;
        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(InitEnumerator());
        }
        private IEnumerator InitEnumerator()
        {
            while (Headset == null)
            {
                yield return null;
            }
            m_gazer.transform.SetParent(Headset);
            m_gazer.transform.localPosition = Vector3.zero;
            m_gazer.transform.localRotation = Quaternion.identity;
            m_gazer.transform.localScale = Vector3.one;
        }
        private Transform Headset
        {
            get
            {
                if (m_headsetCamera == null)
                {
                    m_headsetCamera = VRTK_DeviceFinder.HeadsetCamera();
                }
                return m_headsetCamera;
            }
        }
        private Transform PlayArea
        {
            get
            {
                if (m_playArea == null)
                {
                    m_playArea = VRTK_DeviceFinder.PlayAreaTransform();
                }
                return m_playArea;
            }
        }
        public bool IsHaptic
        {
            get
            {
                return m_isHaptic;
            }
        }
        public void PlacePlayer(Transform target)
        {
            if (Headset && PlayArea)
            {
                var head_Forward = Headset.forward;
                var head_Forward_Project = Vector3.ProjectOnPlane(head_Forward, target.up);
                var euler_Y = Quaternion.FromToRotation(head_Forward_Project, target.forward).eulerAngles.y;
                PlayArea.rotation = Quaternion.Euler(0, euler_Y, 0) * PlayArea.rotation;
                var relativePos = PlayArea.position - PlayArea.position;
                PlayArea.position = target.position - relativePos;
            }
            else
            {
                if (Headset == null)
                {
                    throw new Exception("Can not find headset.");
                }
                if (PlayArea == null)
                {
                    throw new Exception("Can not find playarea.");
                }
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }
    }
}

