using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace KaiTool.UI
{
    [RequireComponent(typeof(Text))]
    public sealed class KaiTool_UITyperEffect : MonoBehaviour
    {
      
        [SerializeField]
        private float m_duration = 1f;
        [SerializeField]
        private float m_delay = 0f;
        [SerializeField]
        private bool m_isShownAtAwake;
        [SerializeField]
        private bool m_isAutoPlay = true;

        private Text m_text;
        private string m_originalText;
        private Coroutine m_playCoroutine;
        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            if (m_isAutoPlay)
            {
                PlayForward();
            }
        }
        private void Init()
        {
            InitVar();
        }
        private void InitVar()
        {
            m_text = GetComponent<Text>();
            m_originalText = m_text.text;
            if (!m_isShownAtAwake)
            {
                m_text.text = null;
            }
        }
        [ContextMenu("PlayForward")]
        private void PlayForward()
        {
            if (m_playCoroutine != null)
            {
                StopCoroutine(m_playCoroutine);
            }
            m_playCoroutine = StartCoroutine(PlayForwardEnumerator());
        }
        [ContextMenu("PlayBackward")]
        private void PlayBackward()
        {
            if (m_playCoroutine != null)
            {
                StopCoroutine(m_playCoroutine);
            }
            m_playCoroutine = StartCoroutine(PlayBackwardEnumerator());
        }
        private IEnumerator PlayForwardEnumerator()
        {
            var length = m_originalText.Length;
            var startIndex = m_text.text.Length;
            var deltaTime = m_duration / length;
            yield return new WaitForSeconds(m_delay);
            var wait = new WaitForSeconds(deltaTime);
            for (int i = startIndex; i < length + 1; i++)
            {
                m_text.text = m_originalText.Substring(0, i);
                yield return wait;
            }
        }
        private IEnumerator PlayBackwardEnumerator()
        {
            var length = m_originalText.Length;
            var deltaTime = m_duration / length;
            var startIndex = m_text.text.Length;
            yield return new WaitForSeconds(m_delay);
            var wait = new WaitForSeconds(deltaTime);
            for (int i = 0; i < startIndex; i++)
            {
                m_text.text = m_originalText.Substring(0, startIndex - i - 1);
                yield return wait;
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PlayForward();
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                PlayBackward();
            }
        }



    }
}