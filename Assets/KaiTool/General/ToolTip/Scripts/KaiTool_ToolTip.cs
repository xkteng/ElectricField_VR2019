#define PC
//#define VR
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace KaiTool.ToolTip
{
    [RequireComponent(typeof(LineRenderer))]
    public class KaiTool_ToolTip : MonoBehaviour
    {
        private const float SCALE_FACTOR = 3.5f;
        #region SERIALIZED_FIELDS
        [Header("Info")]
        [SerializeField]
        private string m_message;
        [SerializeField]
        private float m_width = 0.1f;
        [SerializeField]
        private Material m_line_mat;
        [SerializeField]
        private Transform m_start;
        [SerializeField]
        private Transform m_end;
        [SerializeField]
        private Color m_text_color = Color.white;
        [SerializeField]
        private Color m_image_color = Color.black;
        [SerializeField]
        private bool m_isAlwayFaceToPlayer = true;
        [Header("Fade")]
        [SerializeField]
        private bool m_isAutoShow = true;
        [SerializeField]
        private float m_fadingDuration = 0.2f;
        [Header("Gizmos")]
        [SerializeField]
        private bool m_isDrawGizmos = false;
        [SerializeField]
        private Color m_gizmosColor = Color.blue;
        #endregion
        #region NONSERIALIZED_FIELDS
        private LineRenderer m_lineRenderer;
        private Canvas m_canvas;
        private Transform m_camera;
        private float m_lineRenderValue = 0f;
        private Image m_image;
        private Text m_text;
        private Tweener m_tweener_LineRenderer;
        #endregion
        #region PROPERTIES
        private Transform CameraTransform
        {
            get
            {
                if (m_camera == null)
                {
#if PC
                    m_camera = Camera.main.transform;
#elif VR
                    m_camera = VRTK.VRTK_DeviceFinder.HeadsetCamera();
#endif
                }
                return m_camera;
            }
        }
        public float FadingDuration
        {
            get
            {
                return m_fadingDuration;
            }
        }
        public string Message
        {
            get
            {
                return m_message;
            }

            set
            {
                m_text.text = value;
                m_message = value;
            }
        }

        public Text Text
        {
            get
            {
                return m_text;
            }
        }
        #endregion
        #region PUBLIC_METHODS
        public void Show(string text, int fontSize = 50)
        {
            m_text.text = text;
            m_text.fontSize = fontSize;
            if (m_tweener_LineRenderer != null)
            {
                m_tweener_LineRenderer.Kill();
            }
            m_tweener_LineRenderer = DOTween.To(() => m_lineRenderValue, (value) => m_lineRenderValue = value, 1, m_fadingDuration).OnComplete(() =>
            {
                m_canvas.gameObject.SetActive(true);
            });
        }
        [ContextMenu("Show")]
        public void Show()
        {
            Show(m_message);
        }
        [ContextMenu("Hide")]
        public void Hide()
        {
            if (m_tweener_LineRenderer != null)
            {
                m_tweener_LineRenderer.Kill();
            }
            m_canvas.gameObject.SetActive(false);
            m_tweener_LineRenderer = DOTween.To(() => m_lineRenderValue, (value) => m_lineRenderValue = value, 0, m_fadingDuration);
        }
        #endregion
        #region PRIVATE_METHODS
        private void Awake()
        {
            Init();
        }
        private void Start()
        {
        }
        protected virtual void Init()
        {
            InitVar();
            InitEvent();
            m_image.transform.localScale = Vector3.one * (m_text.text.Length / SCALE_FACTOR);
            m_canvas.gameObject.SetActive(false);
            if (m_isAutoShow)
            {
                Show();
            }
        }
        private void InitVar()
        {
            m_image = GetComponentInChildren<Image>();
            m_text = GetComponentInChildren<Text>();
            m_lineRenderer = GetComponent<LineRenderer>();
            m_canvas = GetComponentInChildren<Canvas>();
            m_start.transform.localPosition = new Vector3(m_start.transform.localPosition.x * m_text.text.Length / SCALE_FACTOR, m_start.transform.localPosition.y, 0);
            if (m_line_mat)
            {
                m_lineRenderer.material = m_line_mat;
            }
        }
        private void InitEvent()
        {
            m_lineRenderer.enabled = true;
            m_lineRenderer.positionCount = 2;
            var positions = new Vector3[] {
                        m_start.position,
                        m_end.position
                    };
            m_lineRenderer.SetPositions(positions);
        }
        private void Update()
        {
            if (m_isAlwayFaceToPlayer)
            {
                FaceToPlayer();
            }
            UpdateLineRender();

            if (Input.GetKeyDown(KeyCode.S))
            {
                Show("Text", 50);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                Hide();
            }
        }
        private void Reset()
        {
            try
            {
                GetComponent<LineRenderer>().enabled = false;
            }
            catch { }
        }
        private void UpdateLineRender()
        {
            m_lineRenderer.SetPositions(new Vector3[] {
                m_end.position,Vector3.Lerp(m_end.position,m_start.position,m_lineRenderValue)
            });
        }
        private void FaceToPlayer()
        {
            if (CameraTransform)
            {
                m_canvas.transform.LookAt(transform.position - CameraTransform.position + transform.position);
                var euler = CameraTransform.eulerAngles;
                m_camera.transform.eulerAngles = new Vector3(euler.x, euler.y, 0);
            }
        }
        private void OnValidate()
        {
            try
            {
                GetComponent<LineRenderer>().startWidth = m_width;
                GetComponent<LineRenderer>().endWidth = m_width;
            }
            catch { }

            try
            {
                var text = GetComponentInChildren<Text>();
                text.text = m_message;
                text.color = m_text_color;
            }
            catch { }

            try
            {
                var image = GetComponentInChildren<Image>();
                image.color = m_image_color;
            }
            catch { }

            try
            {
                UpdateLineRender();
            }
            catch { }
            try
            {
                var lr = GetComponent<LineRenderer>();
                lr.material = m_line_mat;
            }
            catch
            {

            }
        }
        private void OnDrawGizmos()
        {
            if (m_isDrawGizmos)
            {
                Gizmos.color = m_gizmosColor;
                Gizmos.DrawWireSphere(m_start.position, m_width);
                Gizmos.DrawWireSphere(m_end.position, m_width);
                Gizmos.DrawLine(m_start.position, m_end.position);
            }
        }
        #endregion
    }
}