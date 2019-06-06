using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.Utilities
{
    public class StepManagerEvent : UnityEvent<int>
    {

    }
    public sealed class StepManager : Singleton<StepManager>, IEnumerator
    {
        #region FIELDS
        public Step[] m_steps;
        private int position = -1;
        public StepManagerEvent m_moveNext = new StepManagerEvent();
        public StepManagerEvent m_moveBack = new StepManagerEvent();
        #endregion
        #region PROPERTIES
        public object Current
        {
            get
            {
                try
                {
                    return m_steps[position];
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        #endregion
        #region PUBLIC_MEHTODS
        public bool MoveNext()
        {
            position++;
            if (position < m_steps.Length)
            {
                m_moveNext.Invoke(position);
                return true;
            }
            else
            {
                position--;
                return false;
            }
        }
        public bool MoveBack()
        {
            position--;
            if (position > -1)
            {
                m_moveBack.Invoke(position);
                return true;
            }
            else
            {
                position++;
                return false;
            }
        }
        public void Reset()
        {
            position = -1;
        }
        public void StartFirstStep()
        {
            if (MoveNext())
            {
                ((Step)Current).OnStepStart(new StepEventArgs());
            }
        }
        public void UndoCurrentStep()
        {
            if (!((Step)Current).IsDirty)
            {
                MoveBack();
                ((Step)Current).Undo();
            }
            else
            {// Current isStained 
                ((Step)Current).Undo();
            }
        }
        #endregion
        #region PRIVATE&PROTECTED_METHODS
        // Use this for initialization
        private void Start()
        {
            //StartFirstStep();
        }

        // Update is called once per frame
        private void Update()
        {

        }
        protected override void Awake()
        {
            base.Awake();
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            // stepArr = this.GetComponentsInChildren<Step>();
        }
        private void InitEvent()
        {
            for (int i = 0; i < m_steps.Length; i++)
            {
                m_steps[i].m_stepEndEventHandle += (sender, e) =>
                {
                    if (MoveNext())
                    {
                        ((Step)Current).OnStepStart(new StepEventArgs());
                    }
                };
            }
        }
        #endregion
    }
}
