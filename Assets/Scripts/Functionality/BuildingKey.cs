using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingKey : MonoBehaviour
{

    public Material defaultMaterial;

    public string Section;

    public string SearchKey;

    // Start is called before the first frame update
    void Start()
    {
        defaultMaterial = transform.GetComponent<MeshRenderer>().material;

        Section = transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
