using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    [SerializeField]
    private float m_intervalTime = 0.02f;
    [SerializeField]
    private float m_step = 0.1f;

    private Coroutine m_walkCoroutine;

    public void StartWalk()
    {
        m_walkCoroutine = StartCoroutine(WalkCoroutine());
    }
    public void StopWalk()
    {
        if (m_walkCoroutine != null)
        {
            StopCoroutine(m_walkCoroutine);
        }
    }
    private IEnumerator WalkCoroutine()
    {
        var wait = new WaitForSeconds(m_intervalTime);
        while (true)
        {
            Walk();
            yield return wait;
        }
    }
    private void Walk() {
        var q = Quaternion.Euler(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360));
        var vec = q * Vector3.forward*m_step;
        transform.position += vec;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartWalk();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            StopWalk();
        }
    }
}
