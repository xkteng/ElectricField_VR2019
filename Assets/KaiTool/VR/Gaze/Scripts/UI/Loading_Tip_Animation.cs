using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool
{
    public class Loading_Tip_Animation : MonoBehaviour
    {
        [SerializeField]
        private float m_duration_0 = 1f;
        [SerializeField]
        private float m_delay_0 = 1f;
        [SerializeField]
        private float m_delay_1 = 0.1f;
        [SerializeField]
        private DOTweenAnimation[] m_animations;


        private void Start()
        {
            Play();
            InvokeRepeating("Play", 0f, m_delay_0);
        }
        private void Play()
        {
            for (int i = 0; i < m_animations.Length; i++)
            {
                PlayOneShot(i);
            }
        }

        private void PlayOneShot(int index)
        {
            var item = m_animations[index];
            item.duration = m_duration_0;
            item.delay = m_delay_1 * index;
            item.DOKill();
            item.CreateTween();
            item.DOPlay();
        }
    }
}