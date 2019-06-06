using KaiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Audio
{
    public class KaiTool_AudioPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] m_clips;
        [SerializeField]
        [Range(0, 256)]
        private int m_priority = 128;
        [SerializeField]
        [Range(0, 1f)]
        private float m_volume = 0.5f;
        [SerializeField]
        [Range(0, 1f)]
        private float m_spatialBlend = 0f;

        private AudioSource m_audioSource;
        protected virtual void Awake()
        {
            m_audioSource = transform.GetSurvivalType<AudioSource>();
            m_audioSource.priority = m_priority;
            m_audioSource.volume = m_volume;
            m_audioSource.spatialBlend = m_spatialBlend;
        }
        public void PlayOneShot()
        {
            if (m_clips != null && m_clips.Length > 0)
            {
                var index = Random.Range(0, m_clips.Length);
                m_audioSource.PlayOneShot(m_clips[index]);
            }
        }
    }
}