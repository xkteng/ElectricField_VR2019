using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.VR
{
    public class GazedPointEvent : UnityEvent<KaiTool_BasicGazer> { }
    public abstract class KaiTool_BasicGazedPoint : MonoBehaviour
    {
        #region SERIALIZED_FIELDS
        [SerializeField]
        private float m_triggerTime = 2.5f;
        [SerializeField]
        private float m_invalidTime = 3f;
        #endregion
        #region NONSERIALIZED_FIELDS
        public GazedPointEvent m_gazerEnter = new GazedPointEvent();
        public GazedPointEvent m_gazerStay = new GazedPointEvent();
        public GazedPointEvent m_gazerExit = new GazedPointEvent();
        public GazedPointEvent m_triggered = new GazedPointEvent();
        //[SerializeField]
        protected bool m_isValid = true;
        protected bool m_isGazed = false;
        protected KaiTool_BasicGazer m_gazer = null;
        private float m_gazedTime = 0f;
        #endregion
        #region PROPERTIES
        public bool IsValid { get => m_isValid; }
        #endregion
        #region PUBLIC_METHODS
        public virtual void OnGazerEnter(KaiTool_BasicGazer gazer)
        {
            m_isGazed = true;
            m_gazer = gazer;
            m_gazerEnter.Invoke(gazer);
        }
        public virtual void OnGazerStay(KaiTool_BasicGazer gazer)
        {
            m_gazerStay.Invoke(gazer);
        }
        public virtual void OnGazerExit(KaiTool_BasicGazer gazer)
        {
            m_isGazed = false;
            m_gazer = null;
            m_gazedTime = 0f;
            m_gazerExit.Invoke(gazer);
            var x = 0f;
            DOTween.To(() => x, (value) => x = value, 0, m_invalidTime).OnComplete(() =>
            {
                OnRestored();
            });
        }
        public virtual void OnTriggered(KaiTool_BasicGazer gazer)
        {
            if (m_isValid)
            {
                m_isValid = false;
                m_triggered.Invoke(gazer);
            }
        }
        #endregion
        #region PRIVATE_METHODS
        protected virtual void OnRestored()
        {
            m_isValid = true;
            m_gazedTime = 0f;
        }
        protected virtual void Awake()
        {

        }
        private void FixedUpdate()
        {
            if (m_isGazed && m_isValid)
            {
                if (m_gazedTime < m_triggerTime)
                {
                    m_gazedTime += Time.fixedDeltaTime;
                }
                else if (m_isValid)
                {
                    OnTriggered(m_gazer);
                }
            }
        }
        #endregion
    }
}