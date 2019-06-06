using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.Utilities
{
    public class SteppedObject : MonoBehaviour, ISteppedObject
    {
        [SerializeField]
        private bool m_isStarted = false;
        [SerializeField]
        private bool m_isFinished = false;
        [Header("Events")]
        [SerializeField]
        private UnityEvent m_start;
        [SerializeField]
        private UnityEvent m_finish;
        [SerializeField]
        private UnityEvent m_undo;

        private bool m_isClean = true;
        protected Vector3 m_originalPos;
        protected Quaternion m_originalWorldRot;
        protected Quaternion m_originalLocalRot;

        public delegate void SteppedObjectEventHandle(UnityEngine.Object sender, SteppedObjectEventArgs e);
        public SteppedObjectEventHandle m_steppedObjectStartEventHandle;
        public SteppedObjectEventHandle m_steppedObjectFinishedEventHandle;
        public SteppedObjectEventHandle m_steppedObjectUndoneEventHandle;


        #region PUBLIC_PROPERTIES
        public bool IsFinished
        {
            get
            {
                return m_isFinished;
            }
        }
        public bool IsClean
        {
            get
            {
                return m_isClean;
            }
        }
        public bool IsStarted
        {
            get
            {
                return m_isStarted;
            }
        }

        bool ISteppedObject.IsStarted { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        bool ISteppedObject.IsFinished { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        bool ISteppedObject.IsClean { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        #endregion
        #region PUBLIC_METHODS
        public void OnStartSteppedObject(SteppedObjectEventArgs e)
        {
            if (m_steppedObjectStartEventHandle != null)
            {
                m_steppedObjectStartEventHandle(this, e);
            }
            m_isStarted = true;
        }
        public void OnStartSteppedObject()
        {
            var args = new SteppedObjectEventArgs();
            OnStartSteppedObject(args);
        }
        public void OnFinishSteppedObject(SteppedObjectEventArgs e)
        {
            if (m_steppedObjectFinishedEventHandle != null)
            {
                m_steppedObjectFinishedEventHandle(this, e);
            }
            m_isFinished = true;
        }
        public void OnFinishSteppedObject()
        {
            var args = new SteppedObjectEventArgs();
            OnFinishSteppedObject(args);
        }
        public void OnUndoSteppedObject(SteppedObjectEventArgs e)
        {
            if (m_steppedObjectUndoneEventHandle != null)
            {
                m_steppedObjectUndoneEventHandle(this, e);
            }
            m_isFinished = false;
        }
        public void OnUndoSteppedObject()
        {
            var args = new SteppedObjectEventArgs();
            OnUndoSteppedObject(args);
        }
        #endregion
        #region PRIVATE & PROTECTED_METHODS
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
            m_originalPos = transform.position;
            m_originalWorldRot = transform.rotation;
        }
        private void InitEvent()
        {
        }

        public void ToggleMarked(bool toggle)
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public void OnSteppedObjectStart(Object sender, SteppedObjectEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public void OnSteppedObjectFinished(Object sender, SteppedObjectEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public void OnSteppedObjectUnFinished(Object sender, SteppedObjectEventArgs e)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }

}

