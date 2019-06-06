//#define Debug
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Utilities
{
    public struct StepEventArgs
    {
        public string message;
        public StepEventArgs(string message)
        {
            this.message = message;
        }
    }
    public class Step : MonoBehaviour
    {
        private const float JUDGE_INTERVAL_TIME = 0.1f;
        #region FIELDS

        [Tooltip("The array of gamoObjects has the component which inherits from the IsteppedObject.")]
        public GameObject[] m_stepObjects;
        [Tooltip("The array of gameObjects needed to be manipulated in the step.")]
        public List<ISteppedObject> m_steppedObjectList = new List<ISteppedObject>();
        [Tooltip("The array of gameObjects needed to be shown up at the beginning of the step.")]
        public GameObject[] m_gameObjectsNeedActiveAtBeginning;
        [Tooltip("The array of gameObjects needed to be hidden at the end of the step.")]
        public GameObject[] m_gameObjectsNeedInActiveAtEnd;
        [Tooltip("The message carried by this step.")]
        public string m_stepMessage = "Default";

        public delegate void StepEventHandle(Object sender, StepEventArgs e);
        public event StepEventHandle m_stepStartEventHandle;
        public event StepEventHandle m_stepEndEventHandle;
        public event StepEventHandle m_stepUndoEventHandle;
        private bool m_isComplete = false;//Whether the step is completed
        private bool m_isDirty = false;//Whether the step is dirty or not,dirty means it has been changed.
        private Coroutine m_judeCoroutine;
        #endregion
        #region PUBLIC_PROPERTIES
        /// <summary>
        /// whether this step is completed or not.
        /// </summary>
        public bool IsStepCompleted
        {
            get
            {
                return m_isComplete;
            }
            set
            {
                m_isComplete = value;
            }
        }
        /// <summary>
        /// Whether the step is dirty or not,dirty means it has been changed.
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return m_isDirty;
            }

            set
            {
                m_isDirty = value;
            }
        }
        #endregion
        #region PUBLIC_METHODS
        public void OnStepStart(StepEventArgs e)
        {
            if (this.m_stepStartEventHandle != null)
            {
                this.m_stepStartEventHandle(this, e);
            }
            foreach (var item in m_steppedObjectList)
            {
                var args = new SteppedObjectEventArgs();
                item.OnSteppedObjectStart(this,args);
            }
            m_judeCoroutine = StartCoroutine(JudgeEnumerator());

        }
        public void OnStepEnd(StepEventArgs e)
        {
            if (this.m_stepEndEventHandle != null)
            {
                this.m_stepEndEventHandle(this, e);
            }
        }
        public void OnStepUndo(StepEventArgs e)
        {
            if (this.m_stepUndoEventHandle != null)
            {
                m_stepUndoEventHandle(this, e);
            }
        }
        /// <summary>
        /// Undo this step,reset all stepped Object to the original state.
        /// </summary>
        public void Undo()
        {
            var args = new StepEventArgs();
            OnStepUndo(args);
        }
        #endregion
        #region PRIVATE&PROTECTED_METHODS
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
            foreach (GameObject obj in m_stepObjects)
            {
                ISteppedObject steppedObject = obj.GetComponent<ISteppedObject>();
                if (steppedObject != null)
                {
                    m_steppedObjectList.Add(steppedObject);
                }
            }
        }
        private void InitEvent()
        {
            this.m_stepStartEventHandle += (sender, e) =>
            {
                StopAllCoroutines();
            };
            this.m_stepEndEventHandle += (sender, e) =>
            {
                StopAllCoroutines();//Very Important!
            };
            this.m_stepUndoEventHandle += (sender, e) =>
            {
                StopAllCoroutines();
            };
        }
        private IEnumerator JudgeEnumerator()
        {
            var wait = new WaitForSeconds(JUDGE_INTERVAL_TIME);
            while (true)
            {
                var isAllFinished = true;
                foreach (var item in m_steppedObjectList)
                {
                    if (!item.IsFinished)
                    {
                        isAllFinished = false;
                        break;
                    }
                }
                if (isAllFinished)
                {
                    var args = new StepEventArgs();
                    OnStepEnd(args);
                    break;
                }
                yield return wait;
            }
            m_judeCoroutine = null;
        }
        #endregion
    }
}
