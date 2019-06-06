//#define Debug
using System.Collections.Generic;
using UnityEngine;
using System;
using KaiTool.PC.Absorption;

namespace KaiTool.VR.Absorption
{

    /// <summary>
    /// This component is used to absorb objects with component of VRTK_AbsorbTarget.
    /// </summary>
    public abstract class KaiTool_VR_BasicAbsorber : MonoBehaviour, IAbsorber
    {
        public static List<KaiTool_VR_BasicAbsorber> s_absorberList = new List<KaiTool_VR_BasicAbsorber>();
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
        private event Action<UnityEngine.Object, AbsorberEventArgs> m_absorbing;
        private event Action<UnityEngine.Object, AbsorberEventArgs> m_releasing;
        private event Action<UnityEngine.Object, AbsorberEventArgs> m_hoveringIn;
        private event Action<UnityEngine.Object, AbsorberEventArgs> m_hoveringOut;

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
                return m_absorbing;
            }

            set
            {
                m_absorbing = value;
            }
        }
        public Action<UnityEngine.Object, AbsorberEventArgs> Releasing
        {
            get
            {
                return m_releasing;
            }

            set
            {
                m_releasing = value;
            }
        }
        public Action<UnityEngine.Object, AbsorberEventArgs> HoveringIn
        {
            get
            {
                return m_hoveringIn;
            }

            set
            {
                m_hoveringIn = value;
            }
        }
        public Action<UnityEngine.Object, AbsorberEventArgs> HoveringOut
        {
            get
            {
                return m_hoveringOut;
            }

            set
            {
                m_hoveringOut = value;
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
                m_absorbing(sender, e);
            }
        }
        public virtual void OnReleasing(UnityEngine.Object sender, AbsorberEventArgs e)
        {
            m_isAbsorbing = false;
            UnFix();
            //e.m_absorbTarget.Rigidbody.isKinematic = false;
            m_absorbingTarget = null;

            if (m_releasing != null)
            {
                m_releasing(sender, e);
            }
        }
        public virtual void OnHoveringIn(UnityEngine.Object sender, AbsorberEventArgs e)
        {
            m_isHovering = true;
            m_hoveringTarget = e.m_absorbTarget;
            if (m_hoveringIn != null)
            {
                m_hoveringIn(sender, e);
            }
        }
        public virtual void OnHoveringOut(UnityEngine.Object sender, AbsorberEventArgs e)
        {
            m_isHovering = false;
            m_hoveringTarget = null;
            if (m_hoveringOut != null)
            {
                m_hoveringOut(sender, e);
            }
        }
        #endregion
        #region PRIVATE_METHODS
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
