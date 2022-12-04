using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public Dictionary<string, object> Colleges = new Dictionary<string, object>();
    
    public Dictionary<string, Texture> CollegeLogos = new Dictionary<string, Texture>();

    public Dictionary<string, object> Buildings = new Dictionary<string, object>();

    public Dictionary<string, Texture> BuildingImages = new Dictionary<string, Texture>();

    public Dictionary<string, object> Staffs = new Dictionary<string, object>();


    //Session data
    public string BuildingKey;


    public string CollegeKey;



    //Searched Data

    public string SearchedStaff;
    public string SearchedCollege;
    public string SearchedBuilding;
}
