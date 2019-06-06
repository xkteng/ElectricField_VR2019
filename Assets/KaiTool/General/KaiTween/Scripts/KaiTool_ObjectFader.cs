#define Debug
using System.Collections;
using UnityEngine;
using System;

namespace KaiTool.KaiTween
{
    public struct ObjectFaderEventArgs
    {
        public float m_duration;
        public ObjectFaderEventArgs(float duration)
        {
            m_duration = duration;
        }
    }
    public class KaiTool_ObjectFader : MonoBehaviour
    {

        [Header("ObjectFader", order = 1)]
        [SerializeField]
        private bool m_IsInOnAwake = true;
        [SerializeField]
        private float m_fadeInDuration = 1f;
        [SerializeField]
        private AnimationCurve m_fadeInCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField]
        private float m_fadeOutDuration = 1f;
        [SerializeField]
        private AnimationCurve m_fadeOutCurve = AnimationCurve.Linear(0, 1, 1, 0);
        [SerializeField]
        private float m_intervalTime = 0.03f;

        private Coroutine m_fadeInCoroutine = null;
        private Coroutine m_fadeOutCoroutine = null;
        public Action<UnityEngine.Object, ObjectFaderEventArgs> m_fadingIn;
        public Action<UnityEngine.Object, ObjectFaderEventArgs> m_fadingOut;

        private void Awake()
        {
            Init();
            if (m_IsInOnAwake)
            {
                In();
            }
            else
            {
                Out();
            }
        }
        protected virtual void Init()
        {
        }
        public void OnFadingIn(ObjectFaderEventArgs e)
        {
            StopAllCoroutines();
            var dur = e.m_duration;
            if (dur != 0f)
            {
                m_fadeInDuration = dur;
            }
            m_fadeInCoroutine = StartCoroutine(FadingInEnumerator());
            if (m_fadingIn != null)
            {
                m_fadingIn(this, e);
            }
        }
        public void OnFadingOut(ObjectFaderEventArgs e)
        {
            StopAllCoroutines();
            var dur = e.m_duration;
            if (dur != 0f)
            {
                m_fadeOutDuration = dur;
            }
            m_fadeOutCoroutine = StartCoroutine(FadingOutEnumerator());
            if (m_fadingOut != null)
            {
                m_fadingOut(this, e);
            }
        }
        private IEnumerator FadingInEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            var timer = 0f;
            while (timer < m_fadeInDuration)
            {

                SetAlphaOfMeshRenderers(m_fadeInCurve.Evaluate(timer / m_fadeInDuration));
                timer += m_intervalTime;
                yield return wait;
            }
        }
        private IEnumerator FadingOutEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            var timer = 0f;
            while (timer < m_fadeOutDuration)
            {

                SetAlphaOfMeshRenderers(m_fadeOutCurve.Evaluate(timer / m_fadeOutDuration));
                timer += m_intervalTime;
                yield return wait;
            }
        }

        private void In()
        {
            SetAlphaOfMeshRenderers(m_fadeInCurve.Evaluate(1));
        }
        private void Out()
        {
            SetAlphaOfMeshRenderers(m_fadeOutCurve.Evaluate(1));
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
        }
        private void SetAlphaOfMeshRenderers(float value)
        {
            value = Mathf.Clamp(value, 0f, 1f);
            foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>())
            {
                foreach (var mat in meshRenderer.sharedMaterials)
                {
                    mat.SetFloat("_Transparent", value);
                }
            }
        }
#if Debug
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                print("In");
                //In();
                OnFadingIn(new ObjectFaderEventArgs(2f));
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                print("Out");
                //Out();
                OnFadingOut(new ObjectFaderEventArgs(2f));
            }
        }
#endif
    }
}