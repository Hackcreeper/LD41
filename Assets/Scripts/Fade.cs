using UnityEngine;

public class Fade : MonoBehaviour
{
    private MeshRenderer[] _renderers;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<MeshRenderer>();
    }
    
    private void Update()
    {
        const int near = 40;
        const int far = 6400;
        var diff = far - near;

        var distance = Vector3.Distance(transform.position, Level.Instance.GetPlayer().position);
        var alpha = Mathf.Clamp01(1 - 100f/diff * (distance-near));

        foreach (var mesh in _renderers)
        {
            var color = mesh.material.color;
            mesh.material.color = new Color(color.r, color.g, color.b, alpha);   
        }
    }
}