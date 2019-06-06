using UnityEngine;
using KaiTool.UI;
using System.Collections;
using UnityEngine.Events;

namespace KaiTool.UI
{
    [RequireComponent(typeof(KaiTool_BasicUIObject))]
    public class UISectionEvent : UnityEvent<int> { }
    public class KaiTool_BasicUISection : MonoBehaviour
    {
        [Header("Section")]
        //[SerializeField]
        //private bool m_isShowLastContentDefaultly = true;
        [SerializeField]
        private KaiTool_BasicUIObject[] m_contents;
        [SerializeField]
        private float m_waitTime = 0f;
        private KaiTool_BasicUIObject m_UIObject;
        [SerializeField]
        private int m_currentIndex = -1;
        //[SerializeField]
        private int m_lastIndex = -1;
        private Coroutine m_ShowContentCoroutine;

        private UISectionEvent m_ShowContentEvent = new UISectionEvent();
        private UISectionEvent m_hideAllContentsEvent = new UISectionEvent();

        public UISectionEvent ShowContentEvent
        {
            get
            {
                return m_ShowContentEvent;
            }

            set
            {
                m_ShowContentEvent = value;
            }
        }

        public UISectionEvent HideAllContentsEvent
        {
            get
            {
                return m_hideAllContentsEvent;
            }

            set
            {
                m_hideAllContentsEvent = value;
            }
        }

        public void ShowContent(int index, bool resetLastIndex = false)
        {
            if (m_ShowContentCoroutine != null)
            {
                StopCoroutine(m_ShowContentCoroutine);
                if (m_currentIndex != -1)
                {
                    m_contents[m_currentIndex].Show();
                }
            }
            if (m_currentIndex != index && index != -1)
            {
                if (m_ShowContentEvent != null)
                {
                    m_ShowContentEvent.Invoke(index);
                }
            }

            m_ShowContentCoroutine = StartCoroutine(ShowContentEnumerator(index, resetLastIndex));
        }
        public void HideContent(int index, bool resetLastIndex = false)
        {
            if (m_contents[index].IsShown)
            {
                if (!resetLastIndex)
                {
                    m_lastIndex = m_currentIndex;
                }
                else
                {
                    m_lastIndex = -1;
                }
                m_currentIndex = -1;
                m_contents[index].Hide();
            }
        }
        public void HideAllContents(bool resetLastIndex = false)
        {
            if (m_hideAllContentsEvent != null)
            {
                m_hideAllContentsEvent.Invoke(-1);
            }
            ShowContent(-1, resetLastIndex);
            if (resetLastIndex)
            {
                m_lastIndex = -1;
            }
            // m_isDirty = false;
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

        private void InitVar()
        {
            m_UIObject = GetComponent<KaiTool_BasicUIObject>();
        }
        private void InitEvent()
        {
            m_UIObject.ShowUnityEvent.AddListener(() =>
            {
                if (m_lastIndex != -1)
                {
                    //if (m_isShowLastContentDefaultly)
                    //{
                    ShowContent(m_lastIndex);
                    // }
                }

            });
            m_UIObject.HideUnityEvent.AddListener(() =>
            {
                HideAllContents();
            });
            m_ShowContentEvent.AddListener((index) =>
            {
                if (!m_UIObject.IsShown)
                {
                    m_UIObject.Show();
                }
            });


        }
        /// <summary>
        /// if index equals -1,then hide current content.
        /// </summary>
        /// <param name="index"></param>

        private IEnumerator ShowContentEnumerator(int index, bool resetLastIndex)
        {
            if (m_currentIndex != index)
            {
                if (m_currentIndex != -1)
                {
                    m_contents[m_currentIndex].Hide();
                    yield return new WaitForSeconds(m_waitTime);
                }
                if (!resetLastIndex)
                {
                    m_lastIndex = m_currentIndex;
                }
                else
                {
                    m_lastIndex = -1;
                }
                if (index != -1)
                {
                    m_currentIndex = index;
                    if (!m_contents[m_currentIndex].gameObject.activeSelf)
                    {
                        m_contents[m_currentIndex].gameObject.SetActive(true);
                    }
                    m_contents[m_currentIndex].Show();
                }
                else
                {
                    m_currentIndex = -1;
                }
            }
        }
    }
}