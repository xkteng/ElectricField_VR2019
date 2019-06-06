using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Machinery
{

    public class KaiTool_FixedJoint : KaiTool_BasicJoint
    {
        [Header("FixedJoint")]
        [SerializeField]
        protected Transform m_anchor;
        [Header("Motion")]
        [SerializeField]
        protected EnumMotionMode m_X_Motion = EnumMotionMode.Locked;
        [SerializeField]
        protected EnumMotionMode m_Y_Motion = EnumMotionMode.Locked;
        [SerializeField]
        protected EnumMotionMode m_Z_Motion = EnumMotionMode.Locked;
        [Header("AngularMotion")]
        [SerializeField]
        protected EnumMotionMode m_X_AngularMotion = EnumMotionMode.Locked;
        [SerializeField]
        protected EnumMotionMode m_Y_AngularMotion = EnumMotionMode.Locked;
        [SerializeField]
        protected EnumMotionMode m_Z_AngularMotion = EnumMotionMode.Locked;
        [Header("Limit")]
        [SerializeField]
        protected float m_X_limit = 0.1f;
        [SerializeField]
        protected float m_Y_limit = 0.1f;
        [SerializeField]
        protected float m_Z_limit = 0.1f;
        [Header("AngularLimit")]
        [SerializeField]
        protected float m_X_AngularLimit = 10f;
        [SerializeField]
        protected float m_Y_AngularLimit = 10f;
        [SerializeField]
        protected float m_Z_AngularLimit = 10f;
        private Vector3 m_originalAnchorForward;

        private float m_X_RelativeDistance;
        private float m_Y_RelativeDistance;
        private float m_Z_RelativeDistance;

        private float m_X_RelativeEuler;
        private float m_Y_RelativeEuler;
        private float m_Z_RelativeEuler;
        protected override void Init()
        {
            base.Init();
            InitVar();
        }
        private void InitVar()
        {
            m_originalAnchorForward = m_anchor.forward;
            var deltaVector = transform.position - m_anchor.position;
            m_X_RelativeDistance = deltaVector.x;
            m_Y_RelativeDistance = deltaVector.y;
            m_Z_RelativeDistance = deltaVector.z;
            var deltaQuaterion = Quaternion.FromToRotation(m_anchor.forward, transform.forward);
            var deltaEuler = deltaQuaterion.eulerAngles;
            m_X_RelativeEuler = deltaEuler.x;
            m_Y_RelativeEuler = deltaEuler.y;
            m_Z_RelativeEuler = deltaEuler.z;
        }

        protected override void OnDrawGizmos()
        {
            if (m_anchor != null)
            {
                Gizmos.color = m_gizmosColor;
                Gizmos.DrawSphere(m_anchor.position, 0.05f * m_gizmosSize);
            }
        }
        protected override void ResetTranform()
        {
            ResetRelativePosition();
        }
        private void ResetRelativePosition()
        {
            Vector3 vec = Vector3.zero;
            switch (m_X_Motion)
            {
                case EnumMotionMode.Free:
                    //Do Nothing
                    break;
                case EnumMotionMode.Limited:
                    var delta = transform.position.x - m_anchor.position.x;
                    delta = Mathf.Clamp(delta, m_X_RelativeDistance - m_X_limit, m_X_RelativeDistance + m_X_limit);
                    vec += Vector3.right * delta;
                    break;
                case EnumMotionMode.Locked:
                    vec += Vector3.right * m_X_RelativeDistance;
                    break;
            }
            switch (m_Y_Motion)
            {
                case EnumMotionMode.Free:
                    //Do Nothing
                    break;
                case EnumMotionMode.Limited:
                    var delta = transform.position.y - m_anchor.position.y;
                    delta = Mathf.Clamp(delta, m_Y_RelativeDistance - m_Y_limit, m_Y_RelativeDistance + m_Y_limit);
                    vec += Vector3.up * delta;
                    break;
                case EnumMotionMode.Locked:
                    vec += Vector3.up * m_Y_RelativeDistance;
                    break;
            }
            switch (m_Z_Motion)
            {
                case EnumMotionMode.Free:
                    //Do Nothing
                    break;
                case EnumMotionMode.Limited:
                    var delta = transform.position.z - m_anchor.position.z;
                    delta = Mathf.Clamp(delta, m_Z_RelativeDistance - m_Z_limit, m_Z_RelativeDistance + m_Z_limit);
                    vec += Vector3.forward * delta;
                    break;
                case EnumMotionMode.Locked:
                    vec += Vector3.forward * m_Z_RelativeDistance;
                    break;
            }
            transform.position = m_anchor.position + GetRelativeEuler() * vec;
        }
        private Quaternion GetRelativeEuler()
        {
            float xEuler = 0f, yEuler = 0f, zEuler = 0f;
            var tempQuat = Quaternion.FromToRotation(m_originalAnchorForward, m_anchor.forward).eulerAngles;
            switch (m_X_AngularMotion)
            {
                case EnumMotionMode.Free:

                    break;
                case EnumMotionMode.Limited:
                    xEuler = Mathf.Clamp(tempQuat.x, tempQuat.x - m_X_AngularLimit, tempQuat.x + m_X_AngularLimit);
                    break;
                case EnumMotionMode.Locked:
                    xEuler = tempQuat.x;
                    break;
            }
            switch (m_Y_AngularMotion)
            {
                case EnumMotionMode.Free:

                    break;
                case EnumMotionMode.Limited:
                    yEuler = Mathf.Clamp(tempQuat.y, tempQuat.y - m_Y_AngularLimit, tempQuat.y + m_Y_AngularLimit);
                    break;
                case EnumMotionMode.Locked:
                    yEuler = tempQuat.y;
                    break;
            }
            switch (m_X_AngularMotion)
            {
                case EnumMotionMode.Free:

                    break;
                case EnumMotionMode.Limited:
                    zEuler = Mathf.Clamp(tempQuat.z, tempQuat.z - m_Z_AngularLimit, tempQuat.z + m_Z_AngularLimit);
                    break;
                case EnumMotionMode.Locked:
                    zEuler = tempQuat.z;
                    break;
            }
            return Quaternion.Euler(xEuler, yEuler, zEuler);
        }
    }
}