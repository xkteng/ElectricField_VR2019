
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.KaiTween
{
    public class KaiTool_ObjectActiveToggler : KaiTool_ObjectTransitor
    {

        public override void OnStartIn(ObjectTransitorEventArgs e)
        {
            base.OnStartIn(e);
            if (m_TransitionCoroutine != null)
            {
                StopCoroutine(m_TransitionCoroutine);
            }
            m_TransitionCoroutine = StartCoroutine(ToggleActiveEnumerator(true, m_delay));
        }
        public override void OnEndIn(ObjectTransitorEventArgs e)
        {
            base.OnEndIn(e);

        }
        public override void OnStartOut(ObjectTransitorEventArgs e)
        {
            base.OnStartOut(e);
            if (m_TransitionCoroutine != null)
            {
                StopCoroutine(m_TransitionCoroutine);
            }
            m_TransitionCoroutine = StartCoroutine(ToggleActiveEnumerator(false, m_delay));
        }
        public override void OnEndOut(ObjectTransitorEventArgs e)
        {
            base.OnEndOut(e);

        }

        private IEnumerator ToggleActiveEnumerator(bool toggle, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (toggle)
            {
                var args = new ObjectTransitorEventArgs();
                OnEndIn(args);
            }
            else
            {
                var args = new ObjectTransitorEventArgs();
                OnEndOut(args);
            }
            gameObject.SetActive(toggle);
        }

    }
}