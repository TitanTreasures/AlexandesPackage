using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProjectorController : MonoBehaviour
{
    public Shader shader;
    public Material material;
    public float size = 3f;

    Projector proj;

    private void Start()
    {
        proj = GetComponent<Projector>();
        proj.material = material;
        proj.enabled = true;
    }
}
