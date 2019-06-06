using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.MeshCombination
{
    public class KaiTool_MeshCombiner
    {
        public static Mesh GetCombinedMesh(Transform origin, MeshFilter[] mesheFilters, out Material[] mats, string name = "Default", bool mergeSubMeshes = false)
        {
            var combines = new CombineInstance[mesheFilters.Length];
            mats = new Material[mesheFilters.Length];
            var matrix = origin.worldToLocalMatrix;
            for (int i = 0; i < mesheFilters.Length; i++)
            {
                var meshrenderer = mesheFilters[i].GetComponent<MeshRenderer>();
                if (meshrenderer == null)
                {
                    continue;
                }

                combines[i].mesh = mesheFilters[i].sharedMesh;
                combines[i].transform = matrix * mesheFilters[i].transform.localToWorldMatrix;
                mats[i] = meshrenderer.material;
            }

            var mesh = new Mesh();
            mesh.name = name;
            mesh.CombineMeshes(combines, mergeSubMeshes);
            return mesh;
        }
    }
}