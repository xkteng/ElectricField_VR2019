using HighlightingSystem;
using KaiTool.PC.MouseInteraction;
using System;
using System.Collections;
using UnityEngine;
namespace KaiTool.RTS_GameTool
{
    public struct PlacedOnPlatformEventArgs
    {
        public KaiTool_BasicPlatform m_platform;
    }
    [RequireComponent(typeof(KaiTool_MouseInteractiveObject))]
    public class KaiTool_MouseDragger_OnPlatform : MonoBehaviour
    {
        [SerializeField]
        private LayerMask m_platformLayer;
        [SerializeField]
        private int m_size_x = 1;
        [SerializeField]
        private int m_size_y = 1;
        [SerializeField]
        private float m_offset = 0f;

        private KaiTool_MouseInteractiveObject m_interactiveObject;
        private Vector3 m_startGridPoint;
        private Coroutine m_dragCoroutine = null;

        private KaiTool_BasicPlatform m_lastPlatform;
        private Vector3 m_startDragPosition;
        private GameObject m_bottomMark;
        private KaiTool_BasicPlatform m_lastPlacedPlatform;

        public float Offset
        {
            get
            {
                return m_offset;
            }

            set
            {
                var delta = value - m_offset;
                if (m_bottomMark)
                {
                    m_bottomMark.transform.position -= transform.up * delta;
                }
                m_offset = value;
            }
        }

        public event Action<System.Object, PlacedOnPlatformEventArgs> m_placedEventHandle;
        public event Action<System.Object, PlacedOnPlatformEventArgs> m_unplacedEventHandle;

        #region Initialization
        private void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            m_interactiveObject = GetComponent<KaiTool_MouseInteractiveObject>();
        }
        private void InitEvent()
        {
            m_interactiveObject.LeftButtonPressed += (sender, e) =>
            {
                m_dragCoroutine = StartCoroutine(DragEnumerator());
                var args = new PlacedOnPlatformEventArgs();
                args.m_platform = m_lastPlacedPlatform;
                OnUnplaced(args);
            };
            m_interactiveObject.LeftButtonReleased += (sender, e) =>
            {
                if (m_dragCoroutine != null)
                {
                    StopCoroutine(m_dragCoroutine);
                }
                if (m_lastPlatform)
                {
                    m_lastPlatform.OnUnselected();
                    var args = new PlacedOnPlatformEventArgs();
                    args.m_platform = m_lastPlatform;
                    OnPlaced(args);
                    m_lastPlatform = null;
                    DestroyBottomMark();
                }

            };
        }
        #endregion
        private IEnumerator DragEnumerator()
        {
            m_startDragPosition = transform.position;
            KaiTool_BasicPlatform platform;
            if (GetHitGridPoint(out m_startGridPoint, out platform))
            {
                var startPosition = transform.position;
                while (true)
                {
                    {
                        Vector3 currentGridPoint;
                        if (GetHitGridPoint(out currentGridPoint, out platform))
                        {
                            var taregtPosition = startPosition + (currentGridPoint - m_startGridPoint);
                            if (IsDraggable(taregtPosition))
                            {
                                transform.position = taregtPosition;
                                if (!m_bottomMark)
                                {
                                    CreateBottomMark(transform.position - transform.up * m_offset, platform.transform.rotation);
                                }
                                PlaceToNeareastGridPoint();
                            }
                        }
                        yield return null;
                    }
                }
            }
        }
        private bool GetHitGridPoint(out Vector3 gridPoint, out KaiTool_BasicPlatform platform)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //var ray = new Ray(transform.position + transform.up * 100f, -transform.up);
            var hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_platformLayer))
            {
                platform = hit.transform.GetComponent<KaiTool_BasicPlatform>();

                if (platform)
                {
                    if (m_lastPlatform != platform && m_lastPlatform)
                    {
                        m_lastPlatform.OnUnselected();
                        DestroyBottomMark();
                    }
                    platform.OnSelected();
                    gridPoint = platform.GetFloorGridPoint(hit.point);
                    m_lastPlatform = platform;
                    return true;
                }
                else
                {
                    if (m_lastPlatform)
                    {
                        m_lastPlatform.OnUnselected();
                        m_lastPlatform = null;
                        DestroyBottomMark();
                    }
                    gridPoint = Vector3.one;
                    return false;
                }
            }
            else
            {
                if (m_lastPlatform)
                {
                    m_lastPlatform.OnUnselected();
                    m_lastPlatform = null;
                    DestroyBottomMark();
                }
                gridPoint = Vector3.one;
                platform = null;
                return false;
            }
        }
        protected virtual bool IsDraggable(Vector3 targetPosition)
        {
            return true;
        }
        private void ResetHeight()
        {
            var platform = GetValidPlatform();
            var origin = platform.Origin;
            var delta = transform.position - origin.position;
            var projectOnPlane = Vector3.ProjectOnPlane(delta, origin.up);
            var projectOnNormal = Vector3.Project(delta, origin.up);
            transform.position = origin.position + projectOnPlane + projectOnNormal.normalized * m_offset;
        }
        private void PlaceToNeareastGridPoint()
        {
            var platform = GetValidPlatform();
            if (platform)
            {
                transform.position = platform.GetNearestGridPoint(transform.position) + platform.transform.up * m_offset;
            }
        }
        private KaiTool_BasicPlatform GetValidPlatform()
        {
            var ray_down = new Ray(transform.position + transform.up * 100f, -transform.up);
            RaycastHit hit;
            if (Physics.Raycast(ray_down, out hit, Mathf.Infinity, m_platformLayer))
            {
                return hit.transform.GetComponent<KaiTool_BasicPlatform>();
            }
            else
            {
                var ray_up = new Ray(transform.position + transform.up * 100f, transform.up);
                if (Physics.Raycast(ray_up, out hit, Mathf.Infinity, m_platformLayer))
                {
                    return hit.transform.GetComponent<KaiTool_BasicPlatform>();
                }
            }
            return null;
        }
        private void OnValidate()
        {
            PlaceToNeareastGridPoint();
        }
        private void OnEnable()
        {
            PlaceToNeareastGridPoint();
        }
        private void OnPlaced(PlacedOnPlatformEventArgs e)
        {
            if (m_placedEventHandle != null)
            {
                m_placedEventHandle(this, e);
            }
            m_lastPlacedPlatform = e.m_platform;
        }
        private void OnUnplaced(PlacedOnPlatformEventArgs e)
        {
            if (m_unplacedEventHandle != null)
            {
                m_unplacedEventHandle(this, e);
            }
        }
        private void CreateBottomMark(Vector3 position, Quaternion rotation)
        {
            if (m_bottomMark)
            {
                DestroyImmediate(m_bottomMark);
            }
            m_bottomMark = Instantiate(Resources.Load<GameObject>("Prefabs/BottomMark"), position, rotation, transform);
        }
        private void DestroyBottomMark()
        {
            DestroyImmediate(m_bottomMark);
        }
    }
}