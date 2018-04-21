using UnityEngine;

namespace Utilities
{
    public class RandomMaterial : MonoBehaviour
    {
        [SerializeField] private Material[] _materials;

        private void Start()
        {
            foreach (var mesh in GetComponentsInChildren<MeshRenderer>())
            {
                mesh.material = _materials[Random.Range(0, _materials.Length - 1)];
            }
        }
    }
}