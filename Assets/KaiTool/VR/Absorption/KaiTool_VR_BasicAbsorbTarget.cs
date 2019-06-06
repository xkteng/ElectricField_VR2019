#define Debug
using System;
using UnityEngine;
using VRTK;
using KaiTool.Utilities;
using System.Collections;
using KaiTool.PC.Absorption;
using DG.Tweening;

namespace KaiTool.VR.Absorption
{
    /// <summary>
    /// Components inherit from this class will be absorbed by absorber.
    /// </summary>
    [RequireComponent(typeof(VRTK_InteractableObject))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class KaiTool_VR_BasicAbsorbTarget : MonoBehaviour, IAbsorbTarget
    {
        private const float INTERVAL_TIME = 0.05f;
        private const float CANBEABSORBED_DURATION = 1f;

        [Header("AbsorbTargetFields", order = 2)]
        [SerializeField]
        protected EnumAbsorbType m_absorbType;
        [SerializeField]
        protected string m_absorbName = "DefaultName";
        [SerializeField]
        protected float m_distanceLimit = 0.1f;
        [SerializeField]
        protected float m_angularLimit = 20f;
        [SerializeField]
        protected bool m_isCanbeAbsorbed = false;
        [SerializeField]
        protected bool m_isHovered = false;
        [SerializeField]
        protected bool m_isAbsorbed = false;
        [SerializeField]
        private IAbsorber m_hoveringAbsorber = null;
        [SerializeField]
        protected IAbsorber m_currentAbsorber = null;
        //[Header("GizmosFields", order = 3)]
        //[SerializeField]
        //private Color m_gizmosColor = Color.green;

        protected VRTK_InteractableObject m_interObj;
        private event Action<UnityEngine.Object, AbsorbTargetEventArgs> m_absorbbed;
        private event Action<UnityEngine.Object, AbsorbTargetEventArgs> m_released;
        private event Action<UnityEngine.Object, AbsorbTargetEventArgs> m_hoveredIn;
        private event Action<UnityEngine.Object, AbsorbTargetEventArgs> m_hoveredOut;
        private bool m_isKinematic_Origin = false;
        private Rigidbody m_rigidBody = null;
        private Transform m_parent_Origin = null;
        private Tweener m_judgeAbsorbable = null;

        #region PUBLIC_PROPERTIES
        public float AbsorptionRadius
        {
            get
            {
                return m_distanceLimit;
            }
            set
            {
                m_distanceLimit = value;
            }
        }
        public bool IsCanbeAbsorbed
        {
            get
            {
                return m_isCanbeAbsorbed;
            }
            set
            {
                m_isCanbeAbsorbed = value;
            }
        }
        public bool IsHovered
        {
            get
            {
                return m_isHovered;
            }
        }
        public bool IsAbsorbed
        {
            get
            {
                return m_isAbsorbed;
            }
        }
        public IAbsorber HoveringAbsorber
        {
            get
            {
                return m_hoveringAbsorber;
            }
        }
        public IAbsorber CurrentAbsorber
        {
            get
            {
                return m_currentAbsorber;
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
        public Transform Transform
        {
            get
            {
                return transform;
            }
        }
        public GameObject GameObject
        {
            get
            {
                return gameObject;
            }
        }
        public Rigidbody Rigidbody
        {
            get
            {
                if (m_rigidBody == null)
                {
                    m_rigidBody = transform.GetSurvivalType<Rigidbody>();
                }
                return m_rigidBody;
            }
        }
        public Action<UnityEngine.Object, AbsorbTargetEventArgs> Absorbbed
        {
            get
            {
                return m_absorbbed;
            }

            set
            {
                m_absorbbed = value;
            }
        }
        public Action<UnityEngine.Object, AbsorbTargetEventArgs> Released
        {
            get
            {
                return m_released;
            }

            set
            {
                m_released = value;
            }
        }
        public Action<UnityEngine.Object, AbsorbTargetEventArgs> HoveredIn
        {
            get
            {
                return m_hoveredIn;
            }

            set
            {
                m_hoveredIn = value;
            }
        }
        public Action<UnityEngine.Object, AbsorbTargetEventArgs> HoveredOut
        {
            get
            {
                return m_hoveredOut;
            }

            set
            {
                m_hoveredOut = value;
            }
        }

        #endregion
        #region PUBLIC_METHODS
        public void ForcedAbsorbed(AbsorbTargetEventArgs e)
        {
            throw new NotImplementedException();
        }
        public void ForcedReleased(AbsorbTargetEventArgs e)
        {
            throw new NotImplementedException();
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
            InitEvent();
            StartCoroutine(DetectAbsorberEnumerator());
        }
        private void InitVar()
        {
            m_interObj = GetComponent<VRTK_InteractableObject>();
            m_isKinematic_Origin = Rigidbody.isKinematic;
            m_parent_Origin = transform.parent;
        }
        private void InitEvent()
        {
            m_interObj.InteractableObjectUngrabbed += (sender, e) =>
            {
                Rigidbody.isKinematic = m_isKinematic_Origin;
            };
        }
        private void OnAbsorbbed(UnityEngine.Object sender, AbsorbTargetEventArgs e)
        {
            m_isAbsorbed = true;
            m_isCanbeAbsorbed = false;
            m_currentAbsorber = e.m_absorber;
            transform.SetParent(m_currentAbsorber.Anchor);
            m_currentAbsorber.OnAbsorbbing(this, new AbsorberEventArgs(this));

            if (m_absorbbed != null)
            {
                m_absorbbed(sender, e);
            }
        }
        private void OnReleased(UnityEngine.Object sender, AbsorbTargetEventArgs e)
        {
            m_isAbsorbed = false;
            m_isCanbeAbsorbed = true;
            var x = 0f;
            if (m_judgeAbsorbable != null)
            {
                m_judgeAbsorbable.Kill();
            }
            m_judgeAbsorbable = DOTween.To(() => x, (value) => x = value, 0, CANBEABSORBED_DURATION).OnComplete(() =>
            {
                m_isCanbeAbsorbed = false;
            });
            transform.SetParent(m_parent_Origin);
            m_currentAbsorber.OnReleasing(this, new AbsorberEventArgs(this));
            m_currentAbsorber = null;
            if (m_released != null)
            {
                m_released(sender, e);
            }
        }
        private void OnHoveredIn(UnityEngine.Object sender, AbsorbTargetEventArgs e)
        {
            m_isHovered = true;
            m_hoveringAbsorber = e.m_absorber;
            m_hoveringAbsorber.OnHoveringIn(this, new AbsorberEventArgs(this));
            if (m_hoveredIn != null)
            {
                m_hoveredIn(sender, e);
            }
        }
        private void OnHoveredOut(UnityEngine.Object sender, AbsorbTargetEventArgs e)
        {
            m_isHovered = false;
            m_hoveringAbsorber.OnHoveringOut(this, new AbsorberEventArgs(this));
            m_hoveringAbsorber = null;
            if (m_hoveredOut != null)
            {
                m_hoveredOut(sender, e);
            }
        }
        private IEnumerator DetectAbsorberEnumerator()
        {
            WaitForSeconds wait = new WaitForSeconds(INTERVAL_TIME);
            while (true)
            {
                if (!m_isAbsorbed)
                {
                    IAbsorber nearestAbsorber;
                    if (IsAnyMatchedAbsorberNearby(out nearestAbsorber))
                    {
                        if (!m_isHovered)
                        {
                            OnHoveredIn(this, new AbsorbTargetEventArgs(m_interObj.GetGrabbingObject(), nearestAbsorber));
                        }
                        if (!m_interObj.IsGrabbed() && !m_isAbsorbed)
                        {
                            OnAbsorbbed(this, new AbsorbTargetEventArgs(m_interObj.GetGrabbingObject(), nearestAbsorber));
                        }
                        else
                        {
                            //isn't matched so do nothing.
                        }
                    }
                    else
                    {
                        if (m_isHovered)
                        {
                            OnHoveredOut(this, new AbsorbTargetEventArgs(m_interObj.GetGrabbingObject(), m_hoveringAbsorber));
                        }
                    }
                }
                else
                {//IsAbsorbed
                    if (m_interObj.IsGrabbed() && m_isAbsorbed)
                    {
                        OnReleased(this, new AbsorbTargetEventArgs(m_interObj.GetGrabbingObject(), m_currentAbsorber));
                    }
                }
                yield return wait;
            }
        }
        protected abstract bool IsAnyMatchedAbsorberNearby(out IAbsorber nearestAbsorber);
        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, m_distanceLimit);
        }
        #endregion
    }
}