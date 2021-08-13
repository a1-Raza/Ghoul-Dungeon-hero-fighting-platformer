using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] float xSpeed = 1f;
    [SerializeField] float ySpeed = 1f;

    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(Time.deltaTime * xSpeed, Time.deltaTime * ySpeed);

        meshRenderer.material.mainTextureOffset += offset;
    }
}
