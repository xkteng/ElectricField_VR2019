using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.ObjectManipulator
{
    public class ObjectManipulator 
    {
        #region   Motion Method
        public static void ResetObject(GameObject obj,Vector3 originalPos,Quaternion originalRot) {
            obj.transform.position = originalPos;
            obj.transform.rotation = originalRot;
            foreach (Rigidbody rig in obj.GetComponentsInChildren<Rigidbody>()) {
                rig.velocity = Vector3.zero;
                rig.angularVelocity = Vector3.zero;
            }
        }

        public static void StopAllRigidbodiesMotion(GameObject obj) {
            foreach (Rigidbody rigid in obj.GetComponentsInChildren<Rigidbody>()) {
                rigid.velocity = Vector3.zero;
                rigid.angularVelocity = Vector3.zero;
            }
        }

        #endregion
        #region Object Researcher Method
        /// <summary>
        /// Find the nearest T that inherits from monobehaviour.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="originalPos">original Vector3 point</param>
        /// <param name="list"> List of T type</param>
        /// <returns></returns>
        public static T GetNearestOne<T>(Vector3 originalPos,List<T> list) where T:MonoBehaviour  {
            float minDistance;
            return GetNearestOne<T>(originalPos, list, out minDistance);
        }
        /// <summary>
        /// Find the nearest T that inherits from monobehaviour.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="originalPos">original Vector3 point</param>
        /// <param name="list"> List of T type</param>
        /// <param name="minDistance">minDistance between originalPos and nearesrt T.</param>
        /// <returns></returns>
        public static T GetNearestOne<T>(Vector3 originalPos, List<T> list,out float minDistance) where T : MonoBehaviour
        {
            minDistance = Mathf.Infinity;
            T nearstOne = null;
            foreach (T obj in list)
            {
                var tempDistance = Vector3.Magnitude(obj.transform.position - originalPos);
                if (minDistance > tempDistance)
                {
                    nearstOne = obj;
                    minDistance = tempDistance;
                }
            }
            return nearstOne;
        }
        #endregion
    }
}