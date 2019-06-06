using System;
using System.Collections;
using UnityEngine;
using KaiTool.PC.MouseInteraction;
using KaiTool.Utilities;

namespace KaiTool.PC.Absorption
{
    [RequireComponent(typeof(KaiTool_MouseInteractiveObject))]
    public abstract class KaiTool_BasicAbsorbTarget : MonoBehaviour, IAbsorbTarget
    {
        [Header("AbsorbTargetFields", order = 2)]
        [SerializeField]
        protected EnumAbsorbType m_absorbType;
        [SerializeField]
        protected string m_absorbName = "DefaultName";
        [SerializeField]
        protected float m_absorptionRadius = 0.1f;
        [SerializeField]
        protected bool m_isCanbeAbsorbed = true;
        [SerializeField]
        protected bool m_isHovered = false;
        [SerializeField]
        protected bool m_isAbsorbed = false;
        [SerializeField]
        private IAbsorber m_hoveringAbsorber = null;
        [SerializeField]
        protected IAbsorber m_currentAbsorber = null;
        [SerializeField]
        protected float m_intervalTime = 0.05f;
        [Header("GizmosFields", order = 3)]
        [SerializeField]
        private Color m_gizmosColor = Color.green;
        protected KaiTool_MouseInteractiveObject m_mouseInteractiveObject;
        protected KaiTool_MouseDraggingTracker m_mouseDraggingTracker;
        private event Action<UnityEngine.Object, AbsorbTargetEventArgs> m_absorbbed;
        private event Action<UnityEngine.Object, AbsorbTargetEventArgs> m_released;
        private event Action<UnityEngine.Object, AbsorbTargetEventArgs> m_hoveredIn;
        private event Action<UnityEngine.Object, AbsorbTargetEventArgs> m_hoveredOut;
        private Rigidbody m_rigidbody = null;
        private bool m_isKinematic_Origin = false;

        #region PUBLIC_PROPERTIES
        public float AbsorptionRadius
        {
            get
            {
                return m_absorptionRadius;
            }
            set
            {
                m_absorptionRadius = value;
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
                if (m_rigidbody == null)
                {
                    m_rigidbody = transform.GetSurvivalType<Rigidbody>();
                }
                return m_rigidbody;
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
        private void OnAbsorbbed(UnityEngine.Object sender, AbsorbTargetEventArgs e)
        {
            m_isAbsorbed = true;
            m_currentAbsorber = e.m_absorber;
            m_currentAbsorber.OnAbsorbbing(this, new AbsorberEventArgs(this));
            if (m_absorbbed != null)
            {
                m_absorbbed(sender, e);
            }
        }
        private void OnReleased(UnityEngine.Object sender, AbsorbTargetEventArgs e)
        {
            m_isAbsorbed = false;
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
            WaitForSeconds wait = new WaitForSeconds(m_intervalTime);
            while (true)
            {
                if (!m_isAbsorbed)
                {
                    IAbsorber nearestAbsorber;
                    if (IsAnyMatchedAbsorberNearby(out nearestAbsorber))
                    {
                        var args = new AbsorbTargetEventArgs();
                        args.m_absorber = nearestAbsorber;
                        if (!m_isHovered)
                        {
                            OnHoveredIn(this, args);
                        }
                        if (!m_mouseInteractiveObject.IsDragged && !m_isAbsorbed)
                        {
                            OnAbsorbbed(this, args);
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
                            var args = new AbsorbTargetEventArgs();
                            args.m_absorber = m_hoveringAbsorber;
                            OnHoveredOut(this, args);
                        }
                    }
                }
                else
                {//IsAbsorbed
                    if (m_mouseInteractiveObject.IsDragged && m_isAbsorbed)
                    {
                        var args = new AbsorbTargetEventArgs();
                        args.m_absorber = m_currentAbsorber;
                        OnReleased(this, args);
                    }
                }
                yield return wait;
            }
        }
        protected abstract bool IsAnyMatchedAbsorberNearby(out IAbsorber nearestAbsorber);
        private void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {
            InitVar();
            InitEvent();
        }
        private void Start()
        {
            StartCoroutine(DetectAbsorberEnumerator());
        }
        private void InitVar()
        {
            m_mouseInteractiveObject = GetComponent<KaiTool_MouseInteractiveObject>();
            m_mouseDraggingTracker = GetComponent<KaiTool_MouseDraggingTracker>();
            m_rigidbody = transform.GetSurvivalType<Rigidbody>();
            m_isKinematic_Origin = m_rigidbody.isKinematic;
        }
        private void InitEvent()
        {
            m_mouseInteractiveObject.LeftButtonReleased += (sender, e) =>
            {
                m_rigidbody.isKinematic = m_isKinematic_Origin;
            };
        }
        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = m_gizmosColor;
            Gizmos.DrawWireSphere(this.transform.position, m_absorptionRadius);
        }
        #endregion
    }
}