using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.CommandPattern
{
    public class KaiTool_MoveCommand : KaiTool_BasicCommand
    {
        private Transform m_targetObject;
        private Vector3 m_originalPos;
        private Quaternion m_originalRot;
        private Vector3 m_targetPos;
        private Quaternion m_targetRot;

        internal KaiTool_MoveCommand(Transform targetObject, Vector3 originalPosition, Quaternion originalRotation, Vector3 targetPosition, Quaternion targetRotation) : base()
        {
            m_targetObject = targetObject;
            m_originalPos = originalPosition;
            m_originalRot = originalRotation;
            m_targetPos = targetPosition;
            m_targetRot = targetRotation;
        }
        internal override void Execute()
        {
            m_targetObject.position = m_targetPos;
            m_targetObject.rotation = m_targetRot;
        }
        internal override void Revoke()
        {
            m_targetObject.position = m_originalPos;
            m_targetObject.rotation = m_originalRot;
        }
    }
}