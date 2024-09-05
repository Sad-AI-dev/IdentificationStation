using System.Collections.Generic;
using UnityEngine;

public class RandomMaterialAssigner : MonoBehaviour
{
    [SerializeField] private int indexToAssign;
    [SerializeField] private Material[] materials;

    [Space]
    [SerializeField] private MeshRenderer meshRenderer;

    private void Start()
    {
        Material mat = materials[Random.Range(0, materials.Length)];

        List<Material> mats = new();
        meshRenderer.GetMaterials(mats);

        mats[indexToAssign] = mat;

        meshRenderer.SetMaterials(mats);
    }
}
