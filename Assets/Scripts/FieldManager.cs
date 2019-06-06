using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    private const float DELAY = 1f;

    public delegate void StatusChangedEventHandle();
    public event StatusChangedEventHandle StatusChanged;
    public bool IsStatusChanged()
    {
        //Do something
        return true;
    }

    private void Start()
    {
        StartCoroutine(CheckEnumerator());
    }

    private IEnumerator CheckEnumerator()
    {
        var wait = new WaitForSeconds(DELAY);
        while (true)
        {
            if (IsStatusChanged())
            {
                if (StatusChanged != null)
                {
                    StatusChanged();
                }
            }
            yield return wait;
        }
    }
}
