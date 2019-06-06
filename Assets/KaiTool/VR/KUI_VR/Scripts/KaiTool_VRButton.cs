using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
using KaiTool.UI;
using UnityEngine.Events;
using KaiTool.KaiTween;
using KaiTool.VR.VRUtilities;

namespace KaiTool.VR.UI
{
    public struct KaiTool_VRButtonEventArgs { }

    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Collider))]

    public class KaiTool_VRButton : MonoBehaviour
    {
        [SerializeField]
        private float m_invalidTime = 1f;
        [SerializeField]
        private float m_pressedThresholds = 0.2f;
        [SerializeField]
        private Color m_normalColor = Color.white;
        [SerializeField]
        private Color m_highlightColor = Color.red;
        [SerializeField]
        private Color m_clickedColor = Color.blue;
        private float m_pressedTimer = 0f;

        [SerializeField]
        private bool m_isPressed = false;
        [SerializeField]
        private bool m_isClicked = false;
        [Header("Events")]
        [SerializeField]
        private UnityEvent m_pressed;
        [SerializeField]
        private UnityEvent m_clicked;
        [SerializeField]
        private UnityEvent m_released;

        private Button m_button;
        private KaiTool_BasicUIObject m_basicUIObject;
        private KaiTool_ObjectTransitor m_objectTransitor;
        private Collider m_collider;
        private Coroutine m_changeColorCoroutine;
        private GameObject m_interactController = null;

        #region PRIVATE_METHOD
        private void OnPressed()
        {
            if (m_pressed != null)
            {
                m_pressed.Invoke();
            }
            ChangeColor(m_highlightColor);
            m_isPressed = true;
        }
        private void OnClicked()
        {
            if (m_clicked != null)
            {
                m_clicked.Invoke();
            }
        }
        private void OnReleased()
        {
            if (m_released != null)
            {
                m_released.Invoke();
            }
            ResetStatus();
        }
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
            m_button = GetComponent<Button>();
            m_collider = GetComponent<Collider>();
            m_objectTransitor = GetComponent<KaiTool_ObjectTransitor>();
        }
        private void InitEvent()
        {
            if (m_objectTransitor != null)
            {
                m_objectTransitor.EndIn += (sender, e) =>
                {
                    ToggleCollider(true);
                };
                m_objectTransitor.StartOut += (sender, e) =>
                {
                    ToggleCollider(false);
                };
                m_objectTransitor.EndOut += (sender, e) =>
                {
                    ResetStatus();
                };
            }
            m_button.onClick.AddListener(() =>
            {
                OnClicked();
            });
        }
        private void OnTriggerEnter(Collider other)
        {
            var controllerEvent = other.GetComponentInParent<VRTK_ControllerEvents>();
            if (controllerEvent != null)
            {
                m_interactController = controllerEvent.gameObject;
                OnPressed();
            }
        }
        private void OnTriggerStay(Collider other)
        {
            var controllerEvent = other.GetComponentInParent<VRTK_ControllerEvents>();
            if (controllerEvent != null)
            {
                m_pressedTimer += Time.deltaTime;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            var controllerEvent = other.GetComponentInParent<VRTK_ControllerEvents>();
            if (controllerEvent != null)
            {
                OnReleased();
            }

        }
        private void ChangeColorForTime(Color color, float duration)
        {
            if (m_changeColorCoroutine != null)
            {
                StopCoroutine(m_changeColorCoroutine);
            }
            m_changeColorCoroutine = StartCoroutine(ChangeColorForTimeEnumerator(color, duration));
        }
        private void ChangeColor(Color color)
        {
            var image = GetComponent<Image>();
            image.color = color;
        }
        private IEnumerator ChangeColorForTimeEnumerator(Color color, float duration)
        {
            var image = GetComponent<Image>();
            image.color = color;
            yield return new WaitForSeconds(duration);
            image.color = color;
        }
        private void FixedUpdate()
        {

            if (m_pressedTimer > m_pressedThresholds && !m_isClicked)
            {
                m_isClicked = true;
                // ChangeColorForTime(m_clickedColor, m_invalidTime);
                if (KaiTool_VRPlayerRig.Instance.IsHaptic)
                {
                    Haptic();
                }
                m_button.onClick.Invoke();
            }
        }
        private void Haptic()
        {
            if (m_interactController)
            {
                var controllerReference = VRTK_ControllerReference.GetControllerReference(m_interactController);
                VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, 1f, 0.2f, 0.1f);
            }
        }
        private void OnValidate()
        {
            var image = GetComponent<Image>();
            image.color = m_normalColor;
        }
        private void Reset()
        {
            var collider = GetComponent<Collider>();
            var objectTransitor = GetComponent<KaiTool_ObjectTransitor>();
            if (collider != null && objectTransitor != null)
            {
                collider.enabled = false;
            }
        }
        private void ResetStatus()
        {
            ChangeColor(m_normalColor);
            m_pressedTimer = 0f;
            m_isPressed = false;
            m_isClicked = false;
        }
        private void ToggleCollider(bool toggle)
        {
            m_collider.enabled = toggle;
        }
        private void ToggleColliderInTime(bool toggle, float delay)
        {
            StartCoroutine(ToggleColliderIEnumerator(toggle, delay));
        }
        private IEnumerator ToggleColliderIEnumerator(bool toggle, float delay)
        {
            yield return delay;
            m_collider.enabled = toggle;
        }
        #endregion
    }
}