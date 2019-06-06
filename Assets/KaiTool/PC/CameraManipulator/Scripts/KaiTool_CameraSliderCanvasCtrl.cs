using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.CameraManipulator
{
    [RequireComponent(typeof(KaiTool_CameraEdgeSlider))]
    public class KaiTool_CameraSliderCanvasCtrl : MonoBehaviour
    {
        [SerializeField]
        private Transform m_upCue;
        [SerializeField]
        private Transform m_downCue;
        [SerializeField]
        private Transform m_leftCue;
        [SerializeField]
        private Transform m_rightCue;

        private DOTweenAnimation m_upDot;
        private DOTweenAnimation m_downDot;
        private DOTweenAnimation m_leftDot;
        private DOTweenAnimation m_rightDot;

        private KaiTool_CameraEdgeSlider m_cameraSlider;

        private void Start()
        {
            StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
            yield return InitVar();
            yield return InitEvent();
        }

        private IEnumerator InitVar()
        {
            m_cameraSlider = GetComponent<KaiTool_CameraEdgeSlider>();
            m_upDot = m_upCue.GetComponent<DOTweenAnimation>();
            m_downDot = m_downCue.GetComponent<DOTweenAnimation>();
            m_leftDot = m_leftCue.GetComponent<DOTweenAnimation>();
            m_rightDot = m_rightCue.GetComponent<DOTweenAnimation>();
            yield return null;
        }

        private IEnumerator InitEvent()
        {
            m_cameraSlider.m_startSlide += (sender, e) =>
            {
                var args = (CameraEdgeSliderEventArgs)e;
                var direction = args.m_direction;
                if ((direction & Direction.Up) != 0)
                {
                    ToggleCue(true, Direction.Up);
                }
                if ((direction & Direction.Down) != 0)
                {
                    ToggleCue(true, Direction.Down);
                }
                if ((direction & Direction.Left) != 0)
                {
                    ToggleCue(true, Direction.Left);
                }
                if ((direction & Direction.Right) != 0)
                {
                    ToggleCue(true, Direction.Right);
                }
            };
            m_cameraSlider.m_endSlide += (sender, e) =>
            {
                var args = (CameraEdgeSliderEventArgs)e;
                var direction = args.m_direction;
                if ((direction & Direction.Up) != 0)
                {
                    ToggleCue(false, Direction.Up);
                }
                if ((direction & Direction.Down) != 0)
                {
                    ToggleCue(false, Direction.Down);
                }
                if ((direction & Direction.Left) != 0)
                {
                    ToggleCue(false, Direction.Left);
                }
                if ((direction & Direction.Right) != 0)
                {
                    ToggleCue(false, Direction.Right);
                }
            };
            yield return null;
        }
        private void ToggleCue(bool toggle, Direction direction)
        {
            if (toggle)
            {
                switch (direction)
                {
                    case Direction.Up:
                        m_upDot.DOPlayForward();
                        break;
                    case Direction.Down:
                        m_downDot.DOPlayForward();
                        break;
                    case Direction.Left:
                        m_leftDot.DOPlayForward();
                        break;
                    case Direction.Right:
                        m_rightDot.DOPlayForward();
                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case Direction.Up:
                        m_upDot.DOPause();
                        m_upDot.DORewind();
                        break;
                    case Direction.Down:
                        m_downDot.DOPause();
                        m_downDot.DORewind();
                        break;
                    case Direction.Left:
                        m_leftDot.DOPause();
                        m_leftDot.DORewind();
                        break;
                    case Direction.Right:
                        m_rightDot.DOPause();
                        m_rightDot.DORewind();
                        break;
                }
            }

        }
    }
}