using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCtrl : MonoBehaviour
{
    [SerializeField]
    private float m_potential = 1f;
    private MeshRenderer m_meshRenderer;

    private void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        SetPotential(m_potential);   
    }
    private void SetPotential(float p) {
        m_meshRenderer.material.SetFloat("_Potential",p);
    }

}
