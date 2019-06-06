using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool
{
    public sealed class KaiTool_FaceToCamera : MonoBehaviour
    {
        private Camera m_mainCamera;
        private void Awake()
        {
            m_mainCamera = Camera.main;
        }
        private void FixedUpdate()
        {
            FaceToCamera();
        }
        private void FaceToCamera()
        {
            transform.LookAt(transform.position - m_mainCamera.transform.position + transform.position);
            //var euler = transform.eulerAngles;
            //transform.eulerAngles = new Vector3(euler.x, euler.y, m_mainCamera.transform.eulerAngles.z);
        }

    }
}