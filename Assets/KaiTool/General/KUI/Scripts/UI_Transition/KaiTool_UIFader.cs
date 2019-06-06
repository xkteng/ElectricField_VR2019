#define Debug
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
namespace KaiTool.UI.Fader
{
    public struct UIFaderEventArgs
    {
        public float m_duration;
        public UIFaderEventArgs(float duration)
        {
            m_duration = duration;
        }
    }
    public class KaiTool_UIFader : MonoBehaviour
    {

        [Header("UIFader", order = 1)]
        [SerializeField]
        private bool m_IsInOnAwake = true;
        [SerializeField]
        private bool m_isAutoPlay = true;
        //[SerializeField]
        //private bool m_isActiveWhenFadeOut = false;
        [SerializeField]
        private float m_fadeInDuration = 1f;
        [SerializeField]
        private float m_fadeInDelay = 0f;
        [SerializeField]
        private AnimationCurve m_fadeInCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField]
        private float m_fadeOutDuration = 1f;
        [SerializeField]
        private float m_fadeOutDelay = 0f;
        [SerializeField]
        private AnimationCurve m_fadeOutCurve = AnimationCurve.Linear(0, 1, 1, 0);


        //[SerializeField]
        private float m_intervalTime = 0.02f;
        //private Coroutine m_fadeInCoroutine = null;
        //private Coroutine m_fadeOutCoroutine = null;

        public Action<System.Object, UIObjectEventArgs> m_fadingIn;
        public Action<System.Object, UIObjectEventArgs> m_fadingOut;
        private bool m_IsIn = false;
        private Coroutine m_fadingCoroutine;

        public void OnFadingIn(UIObjectEventArgs e)
        {
            if (m_fadingCoroutine == null)
            {
                if (!m_IsIn)
                {
                    m_IsIn = true;
                    //StopAllCoroutines();
                    var dur = e.m_duration;
                    if (dur != 0f)
                    {
                        gameObject.SetActive(true);
                        m_fadingCoroutine = StartCoroutine(FadingInEnumerator(dur));
                    }
                    else
                    {
                        gameObject.SetActive(true);
                        m_fadingCoroutine = StartCoroutine(FadingInEnumerator());
                    }
                    if (m_fadingIn != null)
                    {
                        m_fadingIn(this, e);
                    }
                }
            }
        }
        public void OnFadingOut(UIObjectEventArgs e)
        {
            if (m_fadingCoroutine == null)
            {
                if (m_IsIn)
                {
                    m_IsIn = false;
                    StopAllCoroutines();
                    var dur = e.m_duration;
                    if (dur != 0f)
                    {
                        m_fadingCoroutine = StartCoroutine(FadingOutEnumerator(dur));
                    }
                    else
                    {
                        m_fadingCoroutine = StartCoroutine(FadingOutEnumerator());
                    }
                    if (m_fadingOut != null)
                    {
                        m_fadingOut(this, e);
                    }
                }
            }
        }
        public void OnFadingIn()
        {
            OnFadingIn(new UIObjectEventArgs());
        }
        public void OnFadingOut()
        {
            OnFadingOut(new UIObjectEventArgs());
        }

        private void Awake()
        {

        }

        private void OnEnable()
        {
            /*
            if (m_IsInOnAwake)
            {
                In();
                m_IsIn = true;
            }
            else
            {
                Out();
                m_IsIn = false;
            }
            */
            if (m_isAutoPlay && !m_IsInOnAwake)
            {
                OnFadingIn();
            }
        }
        private void OnDisable()
        {
            Out();
            m_fadingCoroutine = null;
        }
        private IEnumerator FadingInEnumerator()
        {
            yield return new WaitForSeconds(m_fadeInDelay);
            var wait = new WaitForSeconds(m_intervalTime);
            var timer = 0f;
            while (timer < m_fadeInDuration)
            {
                SetAllGraphicAlpha(m_fadeInCurve.Evaluate(timer / m_fadeInDuration));
                timer += m_intervalTime;
                yield return wait;
            }
            SetAllGraphicAlpha(m_fadeInCurve.Evaluate(1));
            m_fadingCoroutine = null;
        }
        private IEnumerator FadingInEnumerator(float duration)
        {
            yield return new WaitForSeconds(m_fadeInDelay);
            var wait = new WaitForSeconds(m_intervalTime);
            var timer = 0f;
            while (timer < duration)
            {
                SetAllGraphicAlpha(m_fadeInCurve.Evaluate(timer / duration));
                timer += m_intervalTime;
                yield return wait;
            }
            SetAllGraphicAlpha(m_fadeInCurve.Evaluate(1));
            m_fadingCoroutine = null;
        }
        private IEnumerator FadingOutEnumerator()
        {
            yield return new WaitForSeconds(m_fadeOutDelay);
            var wait = new WaitForSeconds(m_intervalTime);
            var timer = 0f;
            while (timer < m_fadeOutDuration)
            {
                SetAllGraphicAlpha(m_fadeOutCurve.Evaluate(timer / m_fadeOutDuration));
                timer += m_intervalTime;
                yield return wait;
            }
            SetAllGraphicAlpha(m_fadeOutCurve.Evaluate(1));
            m_fadingCoroutine = null;
        }
        private IEnumerator FadingOutEnumerator(float duration)
        {
            yield return new WaitForSeconds(m_fadeOutDelay);
            var wait = new WaitForSeconds(m_intervalTime);
            var timer = 0f;
            while (timer < duration)
            {
                SetAllGraphicAlpha(m_fadeOutCurve.Evaluate(timer / duration));
                timer += m_intervalTime;
                yield return wait;
            }
            SetAllGraphicAlpha(m_fadeOutCurve.Evaluate(1));
            m_fadingCoroutine = null;
        }
        private void SetAllGraphicAlpha(float alpha)
        {
            alpha = Mathf.Clamp(alpha, 0, 1);
            foreach (var item in GetComponentsInChildren<Graphic>())
            {
                var temp = item.color;
                var fader = item.GetComponent<KaiTool_UIFader>();
                //if (fader == null)
                //{
                item.color = new Color(temp.r, temp.g, temp.b, alpha);
                //}
            }
        }
        [ContextMenu("FadeIn")]
        private void In()
        {
            SetAllGraphicAlpha(m_fadeInCurve.Evaluate(1f));
            m_IsIn = true;
        }
        [ContextMenu("FadeOut")]
        private void Out()
        {
            SetAllGraphicAlpha(m_fadeOutCurve.Evaluate(1f));
            m_IsIn = false;
        }
        private void OnValidate()
        {
            if (m_IsInOnAwake)
            {
                In();
            }
            else
            {
                Out();
            }

            m_fadeInCurve.keys[0].value = 0;
            m_fadeInCurve.keys[1].value = 1;
            m_fadeOutCurve.keys[0].value = 0;
            m_fadeOutCurve.keys[1].value = 1;

        }
#if Debug
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                //print("FadingIn");
                var args = new UIObjectEventArgs();
                OnFadingIn(args);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                // print("FadingOut");
                var args = new UIObjectEventArgs();
                OnFadingOut(args);
            }
        }
#endif
    }
}