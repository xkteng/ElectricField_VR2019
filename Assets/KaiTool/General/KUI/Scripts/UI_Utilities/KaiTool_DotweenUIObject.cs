using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KaiTool.UI
{
    [RequireComponent(typeof(DOTweenAnimation))]
    public class KaiTool_DotweenUIObject : KaiTool_BasicUIObject
    {
        [SerializeField]
        private float m_duration = 0f;
        [SerializeField]
        private float m_showDelay = 0f;
        [SerializeField]
        private float m_hideDelay = 0f;

        [SerializeField]
        private DOTweenAnimation[] m_dotweenAnimations;

        private Coroutine m_animationCoroutine;
        public float ShowDelay
        {
            get
            {
                return m_showDelay;
            }

            set
            {
                m_showDelay = value;
            }
        }

        public float HideDelay
        {
            get
            {
                return m_hideDelay;
            }

            set
            {
                m_hideDelay = value;
            }
        }

        public float Duration
        {
            get
            {
                return m_duration;
            }

            set
            {
                m_duration = value;
            }
        }

        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            /*
            m_button.enabled = false;
            if (m_transition == Selectable.Transition.ColorTint)
            {
                m_button.image.color = m_button.colors.normalColor;
            }
            */
            m_dotweenAnimations = GetComponents<DOTweenAnimation>();
        }
        private void InitEvent()
        {
            m_dotweenAnimations[0].onComplete.AddListener(() =>
            {

            });
            m_show.AddListener(() =>
            {
                PlayShowAnimation();
            });
            m_hide.AddListener(() =>
            {
                PlayHideAnimation();
            });
        }

        private void PlayShowAnimation()
        {
            if (m_animationCoroutine != null)
            {
                StopCoroutine(m_animationCoroutine);
            }
            m_animationCoroutine = StartCoroutine(PlayShowAnimationEnumerator());

        }
        private IEnumerator PlayShowAnimationEnumerator()
        {
            yield return new WaitForSeconds(m_showDelay);
            foreach (var dot in m_dotweenAnimations)
            {
                dot.DOPlayForward();
            }
        }
        private void PlayHideAnimation()
        {
            if (m_animationCoroutine != null)
            {
                StopCoroutine(m_animationCoroutine);
            }
            m_animationCoroutine = StartCoroutine(PlayHideAnimationEnumerator());
        }
        private IEnumerator PlayHideAnimationEnumerator()
        {
            yield return new WaitForSeconds(m_hideDelay);
            foreach (var dot in m_dotweenAnimations)
            {
                dot.DOPlayBackwards();
            }
        }
        protected override void OnValidate()
        {
            base.OnValidate();
            var dotAnimations = GetComponents<DOTweenAnimation>();
            foreach (var item in dotAnimations)
            {
                item.duration = m_duration;
            }
        }
        public override void Reset()
        {
            base.Reset();
            var dotAnimations = GetComponents<DOTweenAnimation>();
            foreach (var item in dotAnimations)
            {
                item.duration = m_duration;
            }
        }
    }
}