using UnityEngine;
using KaiTool.VR.VRTK_Assistant;
using VRTK;
using KaiTool.VR.Absorption;
/// <summary>
/// Kai write it .
/// </summary>
namespace KaiTool.VR
{
    [RequireComponent(typeof(VRTK_InteractableObject))]

    public class VRTK_InteractiveAssistant : MonoBehaviour
    {
        private VRTK_InteractableObject m_interObject;
        private KaiTool_VR_BasicAbsorbTarget m_absorberTarget;
        private Rigidbody _rigidBody;
        public VRTK_InteractableObject InterObject
        {
            get
            {
                if (m_interObject == null)
                {
                    m_interObject = GetComponent<VRTK_InteractableObject>();
                }
                if (m_interObject == null)
                {
                    m_interObject = gameObject.AddComponent<VRTK_InteractableObject>();
                }
                return m_interObject;
            }
        }
        public KaiTool_VR_BasicAbsorbTarget AbsorbTarget
        {
            get
            {
                if (m_absorberTarget == null)
                {
                    m_absorberTarget = GetComponent<KaiTool_VR_BasicAbsorbTarget>();
                }
                return m_absorberTarget;
            }
        }
        public Rigidbody RigidBody
        {
            get
            {
                if (_rigidBody == null)
                {
                    _rigidBody = GetComponent<Rigidbody>();
                }
                return _rigidBody;
            }
        }
        private void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {
            InitVar();
            InitEvent();
        }
        // Use this for initialization
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }
        private void InitVar() { }
        private void InitEvent()
        {

            if (this.InterObject)
            {
                InterObject.InteractableObjectUngrabbed += (sender, e) =>
                {
                    if (this.RigidBody)
                    {
                        this.RigidBody.velocity = Vector3.zero;
                    }
                };
            }
        }
    }
}
