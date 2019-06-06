using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour {

    public Material targetMat;
    [ContextMenu("ChangeAllMeshRenderMat")]
    public void ChangeAllMeshRenderMat() {
        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>()) {
            mr.material = targetMat;
        }
    }
}
