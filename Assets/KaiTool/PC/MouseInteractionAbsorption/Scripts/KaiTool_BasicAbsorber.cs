using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace KaiTool.PC.Absorption
{
    public abstract class KaiTool_BasicAbsorber : MonoBehaviour, IAbsorber
    {
        public static List<KaiTool_BasicAbsorber> s_absorberList = new List<KaiTool_BasicAbsorber>();
        [Header("AbsorberFields", order = 2)]
        [SerializeField]
        protected EnumAbsorbType m_absorbType;
        [SerializeField]
        private string m_absorbName = "DefaultName";
        [SerializeField]
        protected bool m_isCanAbsorb = true;
        [SerializeField]
        protected bool m_isHovering = false;
        [SerializeField]
        protected bool m_isAbsorbing = false;
        [SerializeField]
        protected IAbsorbTarget m_hoveringTarget;
        [SerializeField]
        protected IAbsorbTarget m_absorbingTarget;
        [SerializeField]
        protected Transform m_anchor;
        [Header("Events")]
        [SerializeField]
        private UnityEvent m_absorbing;
        [SerializeField]
        private UnityEvent m_releasing;
        [SerializeField]
        private UnityEvent m_hoveringIn;
        [SerializeField]
        private UnityEvent m_hoveringOut;


        private event Action<UnityEngine.Object, AbsorberEventArgs> m_absorbingEventHandle;
        private event Action<UnityEngine.Object, AbsorberEventArgs> m_releasingEventHandle;
        private event Action<UnityEngine.Object, AbsorberEventArgs> m_hoveringInEventHandle;
        private event Action<UnityEngine.Object, AbsorberEventArgs> m_hoveringOutEventHandle;

        #region PUBLIC_PROPERTIES
        public bool IsCanAbsorb
        {
            get
            {
                return m_isCanAbsorb;
            }

            set
            {
                m_isCanAbsorb = value;
            }
        }
        public bool IsHovering { get { return m_isHovering; } }
        public bool IsAbsorbing { get { return m_isAbsorbing; } }
        public IAbsorbTarget HoveringTarget
        {
            get
            {
                return m_hoveringTarget;
            }
        }
        public IAbsorbTarget AbsorbingTarget
        {
            get
            {
                return m_absorbingTarget;
            }
        }
        public Transform Anchor
        {
            get
            {
                if (m_anchor == null)
                {
                    GameObject anchorObj = new GameObject("Anchor");
                    anchorObj.transform.SetParent(transform);
                    anchorObj.transform.position = transform.position;
                    anchorObj.transform.rotation = transform.rotation;
                    m_anchor = anchorObj.transform;
                }
                return m_anchor;
            }
        }
        public EnumAbsorbType AbsorbType
        {
            get
            {
                return m_absorbType;
            }
        }
        public string AbsorbName
        {
            get
            {
                return m_absorbName;
            }
        }
        public Action<UnityEngine.Object, AbsorberEventArgs> Absorbing
        {
            get
            {
                return m_absorbingEventHandle;
            }

            set
            {
                m_absorbingEventHandle = value;
            }
        }
        public Action<UnityEngine.Object, AbsorberEventArgs> Releasing
        {
            get
            {
                return m_releasingEventHandle;
            }

            set
            {
                m_releasingEventHandle = value;
            }
        }
        public Action<UnityEngine.Object, AbsorberEventArgs> HoveringIn
        {
            get
            {
                return m_hoveringInEventHandle;
            }

            set
            {
                m_hoveringInEventHandle = value;
            }
        }
        public Action<UnityEngine.Object, AbsorberEventArgs> HoveringOut
        {
            get
            {
                return m_hoveringOutEventHandle;
            }

            set
            {
                m_hoveringOutEventHandle = value;
            }
        }
        #endregion
        #region PUBLIC_METHODS
        public virtual void OnAbsorbbing(UnityEngine.Object sender, AbsorberEventArgs e)
        {
            m_isAbsorbing = true;
            m_absorbingTarget = e.m_absorbTarget;
            m_absorbingTarget.Transform.position = Anchor.transform.position;
            m_absorbingTarget.Transform.rotation = Anchor.transform.rotation;
            Fix();
            //m_absorbingTarget.Rigidbody.isKinematic = true;
            if (m_absorbing != null)
            {
                m_absorbing.Invoke();
            }

            if (m_absorbingEventHandle != null)
            {
                m_absorbingEventHandle(sender, e);
            }
        }
        public virtual void OnReleasing(UnityEngine.Object sender, AbsorberEventArgs e)
        {
            m_isAbsorbing = false;
            UnFix();
            // print("OnReleasing");
            //e.m_absorbTarget.Rigidbody.isKinematic = false;
            m_absorbingTarget = null;
            if (m_releasing != null)
            {
                m_releasing.Invoke();
            }
            if (m_releasingEventHandle != null)
            {
                m_releasingEventHandle(sender, e);
            }
        }
        public virtual void OnHoveringIn(UnityEngine.Object sender, AbsorberEventArgs e)
        {
            m_isHovering = true;
            m_hoveringTarget = e.m_absorbTarget;
            if (m_hoveringIn != null)
            {
                m_hoveringIn.Invoke();
            }
            if (m_hoveringInEventHandle != null)
            {
                m_hoveringInEventHandle(sender, e);
            }
        }
        public virtual void OnHoveringOut(UnityEngine.Object sender, AbsorberEventArgs e)
        {
            m_isHovering = false;
            m_hoveringTarget = null;
            if (m_hoveringOut != null)
            {
                m_hoveringOut.Invoke();
            }
            if (m_hoveringOutEventHandle != null)
            {
                m_hoveringOutEventHandle(sender, e);
            }
        }
        #endregion
        #region PRIVATE & PROTECTED
        private void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {
            InitVar();
        }
        private void InitVar()
        {
        }
        private void OnEnable()
        {
            s_absorberList.Add(this);
        }
        private void OnDisable()
        {
            s_absorberList.Remove(this);
        }
        private void OnDestroy()
        {
            s_absorberList.Remove(this);
        }
        protected abstract void Fix();
        protected abstract void UnFix();
        #endregion
    }
}