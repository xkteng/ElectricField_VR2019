using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine_Test : MonoBehaviour
{
    private Coroutine m_coroutine;
    private void Start()
    {
        m_coroutine = StartCoroutine(DelayOneSecond());
    }

    private IEnumerator DelayOneSecond()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        int i = 0;
        while (true)
        {
            
            print(Time.time);
            if (i>15)
            {
                StopCoroutine(m_coroutine);
            }
            i++;
            yield return wait;
        }
    }
}
