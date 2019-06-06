using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRig : MonoBehaviour
{
    [SerializeField]
    private float m_x = 1f;
    [SerializeField]
    private float m_y = 1f;
    [SerializeField]
    private float m_x_rotate = 1f;
    [SerializeField]
    private float m_y_rotate = 1f;

    private Camera m_camera;

    private void Awake()
    {
        m_camera = GetComponentInChildren<Camera>();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0)
        {
            transform.position += transform.right * m_x * horizontal;
        }
        if (vertical != 0)
        {
            transform.position += transform.forward * m_y * vertical;
        }
    }
    private void Rotate()
    {
        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");
        transform.rotation = Quaternion.Euler(0, mouse_x * m_x_rotate, 0) * transform.rotation;
        m_camera.transform.rotation = Quaternion.AngleAxis(-mouse_y*m_y_rotate,transform.right)*m_camera.transform.rotation;
    }
    private void FixedUpdate()
    {
        Rotate();
        Move();
    }
}
