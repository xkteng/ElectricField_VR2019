using System;
using System.Collections;
using UnityEngine;

namespace KaiTool.Utilities
{
    public class SteppedObjectIEnumerator : SteppedObject, IEnumerator
    {
        [SerializeField]
        private SteppedObject[] steppedObjectArr;
        private int m_index = -1;
        #region PUBLIC_PROPERTIES
        public object Current
        {
            get
            {
                try
                {
                    return steppedObjectArr[m_index];
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        public bool MoveNext()
        {
            m_index++;
            if (m_index < steppedObjectArr.Length)
            {
                return true;
            }
            else
            {
                m_index--;
                return false;
            }
        }
        public void Reset()
        {
            m_index = -1;
        }
        #endregion
        #region PRIVATE_METHODS
        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {

        }
        private void InitEvent()
        {
            this.m_steppedObjectStartEventHandle += (sender, e) =>
            {
                StartFirst();
            };
            foreach (SteppedObject steppedObj in steppedObjectArr)
            {
                steppedObj.m_steppedObjectFinishedEventHandle += (sender, e) =>
                {
                    // print("one screw finished");
                    if (MoveNext())
                    {
                        ((SteppedObject)Current).OnStartSteppedObject(new SteppedObjectEventArgs());
                    }
                    else
                    {
                        // print("IEnumerator Finished");
                        OnFinishSteppedObject(new SteppedObjectEventArgs());
                    }
                };
            }
        }
        /// <summary>
        /// Start first steppedObj which is belong to this IEnumerator.
        /// </summary>
        private void StartFirst()
        {

            if (MoveNext())
            {
                ((SteppedObject)Current).OnStartSteppedObject( new SteppedObjectEventArgs());
            }
        }
        #endregion
    }
}