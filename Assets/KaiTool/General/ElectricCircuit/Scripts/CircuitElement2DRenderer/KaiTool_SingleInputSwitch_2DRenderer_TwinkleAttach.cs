using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace KaiTool.ElectricCircuit
{
    [RequireComponent(typeof(KaiTool_SingleInputSwitch_2DRenderer))]
    [RequireComponent(typeof(Image))]
    public class KaiTool_SingleInputSwitch_2DRenderer_TwinkleAttach : MonoBehaviour
    {
        [SerializeField]
        private float m_duration = 1f;
        [SerializeField]
        private int m_twinkleTimes = 1;
        [SerializeField]
        private Color m_turnedOnColor = Color.red;
        [SerializeField]
        private Color m_turnedOffColor = Color.red;

        private const float INTERVAL_TIME = 0.02f;

        private Image m_image;
        private KaiTool_SingleInputSwitch_2DRenderer m_2DRenderer;
        private Color m_originalColor;
        private Coroutine m_TwinkleCoroutine;

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            m_2DRenderer = GetComponent<KaiTool_SingleInputSwitch_2DRenderer>();
            m_image = GetComponent<Image>();
            m_originalColor = m_image.color;
        }
        private void InitEvent()
        {
            m_2DRenderer.m_turnedOnEventHandle += (sender, e) =>
            {
                Twinkle(true);
            };
            m_2DRenderer.m_turnedOffEventHandle += (sender, e) =>
            {
                Twinkle(false);
            };
        }

        private void Twinkle(bool toggle)
        {
            if (m_TwinkleCoroutine == null)
            {
                m_TwinkleCoroutine = StartCoroutine(TwinkleEnumerator(toggle));
            }
        }

        private IEnumerator TwinkleEnumerator(bool toggle)
        {
            var times = m_duration / 2 / INTERVAL_TIME;
            var wait = new WaitForSeconds(INTERVAL_TIME);
            Color targetColor;
            if (toggle)
            {
                targetColor = m_turnedOnColor;
            }
            else
            {
                targetColor = m_turnedOffColor;
            }
            for (int i = 0; i < m_twinkleTimes; i++)
            {
                for (int j = 0; j < times; j++)
                {
                    m_image.color = Color.Lerp(m_originalColor, targetColor, (((float)(j + 1)) / times));
                    yield return wait;
                }
                for (int j = 0; j < times; j++)
                {
                    m_image.color = Color.Lerp(targetColor, m_originalColor, (((float)(j + 1)) / times));
                    yield return wait;
                }
            }
            m_TwinkleCoroutine = null;
        }

        private void OnDisable()
        {
            Restore();
        }

        private void Restore()
        {
            m_TwinkleCoroutine = null;
            m_image.color = m_originalColor;
        }
    }
}