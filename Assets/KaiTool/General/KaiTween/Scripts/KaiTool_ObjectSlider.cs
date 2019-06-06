#define Debug
using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;

namespace KaiTool.KaiTween
{

    public class KaiTool_ObjectSlider : KaiTool_ObjectTransitor
    {
        [Header("Slider")]
        [SerializeField]
        private float m_speed = 1f;
        [SerializeField]
        private Vector3 m_relativePos = Vector3.right;
        private Tween m_tweener = null;

        #region POSITION
        [SerializeField]
        [HideInInspector]
        private Vector3 m_originWorldPos;
        [SerializeField]
        [HideInInspector]
        private Vector3 m_originLocalPos;
        [SerializeField]
        [HideInInspector]
        private Vector3 m_targetWorldPos;
        [SerializeField]
        [HideInInspector]
        private Vector3 m_targetLocalPos;

        private Vector3 m_localPosition;
        #endregion
        #region PUBLIC_METHODS
        [ContextMenu("SlideIn")]
        public void OnSlideIn()
        {
            OnStartIn(new ObjectTransitorEventArgs());
        }
        [ContextMenu("SlideOut")]
        public void OnSlideOut()
        {
            OnStartOut(new ObjectTransitorEventArgs());
        }
        public override void OnStartIn(ObjectTransitorEventArgs e)
        {
            base.OnStartIn(e);
            if (m_tweener != null)
            {
                m_tweener.Kill();
            }
            float duration = 0f;
            if (transform.parent)
            {
                duration = ((Vector3)(transform.parent.localToWorldMatrix * (Vector4)(m_targetLocalPos - transform.localPosition))).magnitude / m_speed;
            }
            else
            {
                duration = (m_targetLocalPos - transform.localPosition).magnitude / m_speed;
            }
            m_tweener = DOTween.To(() => transform.localPosition, (value) => transform.localPosition = value, m_targetLocalPos, duration).OnComplete(() =>
            {
                var args = new ObjectTransitorEventArgs();
                OnEndIn(args);
            }).SetDelay(m_delay);
        }
        public override void OnEndIn(ObjectTransitorEventArgs e)
        {
            base.OnEndIn(e);
        }
        public override void OnStartOut(ObjectTransitorEventArgs e)
        {
            base.OnStartOut(e);
            if (m_tweener != null)
            {
                m_tweener.Kill();
            }
            float duration = 0f;
            if (transform.parent)
            {
                duration = ((Vector3)(transform.parent.localToWorldMatrix * (Vector4)(m_originLocalPos - transform.localPosition))).magnitude / m_speed;
            }
            else
            {
                duration = (m_originLocalPos - transform.localPosition).magnitude / m_speed;
            }
            m_tweener = DOTween.To(() => transform.localPosition, (value) => transform.localPosition = value, m_originLocalPos, duration).OnComplete(() =>
             {
                 var args = new ObjectTransitorEventArgs();
                 OnEndOut(args);
             }).SetDelay(m_delay);


        }
        public override void OnEndOut(ObjectTransitorEventArgs e)
        {
            base.OnEndOut(e);
        }
        #endregion
        #region PRIVATE_METHODS
        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            SetOriginalPos();
            SetTargetPosition();
        }
        private void SetOriginalPos()
        {
            m_originWorldPos = transform.position;
            m_originLocalPos = transform.localPosition;
        }
        private void SetTargetPosition()
        {
            var increment = transform.right * m_relativePos.x + transform.up * m_relativePos.y + transform.forward * m_relativePos.z;
            m_targetWorldPos = transform.position + increment;
            if (transform.parent)
            {
                m_targetLocalPos = (Vector3)(transform.parent.worldToLocalMatrix * new Vector4(m_targetWorldPos.x, m_targetWorldPos.y, m_targetWorldPos.z, 1));
            }
            else
            {
                m_targetLocalPos = m_targetWorldPos;
            }

        }
        private void InitEvent() { }
        private void OnValidate()
        {
            SetOriginalPos();
            SetTargetPosition();
        }
        private void Reset()
        {

        }
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            if (m_isDrawGizmos)
            {
                if (transform.parent)
                {
                    var origin = transform.parent.localToWorldMatrix * ((Vector4)(m_originLocalPos) + new Vector4(0, 0, 0, 1));
                    var target = transform.parent.localToWorldMatrix * ((Vector4)(m_targetLocalPos) + new Vector4(0, 0, 0, 1));
                    Gizmos.DrawWireSphere(origin, 0.01f * m_gizmosSize);
                    Gizmos.DrawWireSphere(target, 0.01f * m_gizmosSize);
                    Gizmos.DrawLine(origin, target);
                }
                else
                {
                    var origin = m_originLocalPos;
                    var target = m_targetLocalPos;
                    Gizmos.DrawWireSphere(origin, 0.01f * m_gizmosSize);
                    Gizmos.DrawWireSphere(target, 0.01f * m_gizmosSize);
                    Gizmos.DrawLine(origin, target);
                }
            }
        }
        private void OnDrawGizmosSelected()
        {
            SetOriginalPos();
            SetTargetPosition();
        }

        #endregion
#if Debug
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                print("Slide In!");
                OnSlideIn();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                print("Slide Out!");
                OnSlideOut();
            }
        }
#endif
    }
}