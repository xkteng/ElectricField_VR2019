//#define Debug
using UnityEngine;
using UnityEngine.Events;
using KaiTool.GizmosUtilities;

namespace KaiTool.UI
{
    public class KaiTool_BasicUIObject : MonoBehaviour
    {
#if UNITY_EDITOR
        public int lastTab = 0;
        public bool useSubUIObjects = true;
        public bool useEvents = true;
        public bool useGizmos = true;
#endif
        [SerializeField]
        protected bool m_isShownOnEnable = false;
#if KaiTool_Editor
        [SerializeField]
#endif
        private bool m_isShown = false;
        [SerializeField]
        protected KaiTool_BasicUIObject[] m_subUIObjectsToShow;
        [SerializeField]
        protected KaiTool_BasicUIObject[] m_subUIObjectsToHide;


        [SerializeField]
        protected UnityEvent m_show;
        [SerializeField]
        protected UnityEvent m_hide;




        [SerializeField]
        protected bool m_isDrawGizmos = true;
        [SerializeField]
        protected float m_gizmosSize = 1f;
        [SerializeField]
        protected Color m_gizmosColor = Color.white;


        public UnityEvent ShowUnityEvent
        {
            get
            {
                return m_show;
            }

            set
            {
                m_show = value;
            }
        }
        public UnityEvent HideUnityEvent
        {
            get
            {
                return m_hide;
            }

            set
            {
                m_hide = value;
            }
        }





        public bool IsShown
        {
            get
            {
                return m_isShown;
            }
        }

        public bool IsShownOnEnable
        {
            get
            {
                return m_isShownOnEnable;
            }

            set
            {
                m_isShownOnEnable = value;
            }
        }

        public bool IsDrawGizmos
        {
            get
            {
                return m_isDrawGizmos;
            }

            set
            {
                m_isDrawGizmos = value;
            }
        }

        public float GizmosSize
        {
            get
            {
                return m_gizmosSize;
            }

            set
            {
                m_gizmosSize = value;
            }
        }

        public Color GizmosColor
        {
            get
            {
                return m_gizmosColor;
            }
        }

        private void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {

        }



        private void Start()
        {
            if (m_isShownOnEnable)
            {
                Show();
            }
        }
        [ContextMenu("Show")]
        public virtual void Show()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
            if (!m_isShown)
            {
                m_isShown = true;
                ShowSubUIObject();
                if (m_show != null)
                {
                    m_show.Invoke();
                }
            }
        }
        [ContextMenu("Hide")]
        public virtual void Hide()
        {
            if (m_isShown)
            {
                m_isShown = false;
                HideSubUIObject();
                if (m_hide != null)
                {
                    m_hide.Invoke();
                }
            }
        }
        public void ToggleShow()
        {
            if (m_isShown)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
        public void ShowSubUIObject()
        {
            for (int i = 0; i < m_subUIObjectsToShow.Length; i++)
            {
                if (m_subUIObjectsToShow[i] != null)
                {
                    m_subUIObjectsToShow[i].Show();
                }
            }
        }
        public void HideSubUIObject()
        {
            for (int i = 0; i < m_subUIObjectsToHide.Length; i++)
            {
                if (m_subUIObjectsToHide[i] != null)
                {
                    m_subUIObjectsToHide[i].Hide();
                }
            }
        }
        public void HideAllChildrenUIObject()
        {
            foreach (var item in GetComponentsInChildren<KaiTool_BasicUIObject>())
            {
                if (item.IsShown)
                {
                    item.Hide();
                }
            }
        }
        public void ToggleActive(bool toggle)
        {
            gameObject.SetActive(toggle);
        }
        protected virtual void OnDrawGizmos()
        {
        }
        public virtual void Reset()
        {
            m_isShownOnEnable = false;
            m_isShown = false;
            m_subUIObjectsToShow = null;
            m_subUIObjectsToHide = null;
            m_show = null;
            m_hide = null;
        }
        protected virtual void OnTransformParentChanged()
        {

        }
        protected virtual void OnValidate()
        {

        }

    }
}