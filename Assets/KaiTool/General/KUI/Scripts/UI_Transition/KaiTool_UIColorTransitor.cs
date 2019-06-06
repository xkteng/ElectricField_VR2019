using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KaiTool.UI
{
    [RequireComponent(typeof(KaiTool_BasicUIObject))]
    public class KaiTool_UIColorTransitor : MonoBehaviour
    {
        [Header("Color")]
        [SerializeField]
        protected Color m_TargetColor;
        [SerializeField]
        protected float m_duration = 1f;
        [SerializeField]
        protected float m_delay = 0f;
        [SerializeField]
        protected AnimationCurve m_transitCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        //[SerializeField]
        protected bool m_isTransited = false;
        private Coroutine m_transitCoroutine;
        private Color m_originalColor;

        protected void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {
            InitVar();
        }
        private void InitVar()
        {
            m_originalColor = GetComponent<Graphic>().color;
        }

        public void PlayForward()
        {
            if (!m_isTransited)
            {
                m_isTransited = true;
                if (m_transitCoroutine != null)
                {
                    StopCoroutine(m_transitCoroutine);
                }
                m_transitCoroutine = StartCoroutine(TransitColorEnumerator(true, 30));
            }
        }
        public void PlayBackWard()
        {
            if (m_isTransited)
            {
                m_isTransited = false;
                if (m_transitCoroutine != null)
                {
                    StopCoroutine(m_transitCoroutine);
                }
                m_transitCoroutine = StartCoroutine(TransitColorEnumerator(false, 30));
            }
        }
        private IEnumerator TransitColorEnumerator(bool isForward, int subSteps)
        {
            var intervalTime = m_duration / subSteps;
            var wait = new WaitForSeconds(intervalTime);
            var graphic = GetComponent<Graphic>();
            var startColor = graphic.color;

            if (m_duration != 0)
            {
                if (isForward)
                {
                    for (int i = 0; i < subSteps; i++)
                    {
                        graphic.color = Color.Lerp(startColor, m_TargetColor, m_transitCurve.Evaluate((float)(i + 1) / subSteps));
                        yield return wait;
                    }
                }
                else
                {
                    for (int i = 0; i < subSteps; i++)
                    {
                        graphic.color = Color.Lerp(startColor, m_originalColor, m_transitCurve.Evaluate((float)(i + 1) / subSteps));
                        yield return wait;
                    }
                }
            }
            else
            {
                if (isForward)
                {
                    graphic.color = m_TargetColor;
                    yield return null;
                }
                else
                {
                    graphic.color = m_originalColor;
                    yield return null;
                }
            }
        }
    }
}