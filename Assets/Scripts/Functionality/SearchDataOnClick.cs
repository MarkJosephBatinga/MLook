using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SearchDataOnClick : MonoBehaviour
{
    // Start is called before the first frame update

    public string DataKey;
    public string DictKeys;

   public void OnDataClick()
    {
        if(DictKeys == "Buildings")
        {
            BuildFindKey();
        }
        else if (DictKeys == "Staffs")
        {
            StaffKeyFind();
        }
        else if (DictKeys == "Colleges")
        {
            CollegeKeyFind();
        }
       
    }

    public void StaffKeyFind()
    {
        var Staffs = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Staffs;
        foreach (var offices in Staffs)
        {
            var office = offices.Value as Dictionary<string, object>;
            foreach (var staff in office)
            {
                var description = staff.Value as Dictionary<string, object>;
                foreach (var des in description)
                {
                    if (des.Key == "Name" && des.Value.ToString() == DataKey)
                    {
                        GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedStaff = staff.Key;
                        SceneManager.LoadScene("SearchMapScene");
                    }
                }

            }
        }
    }

    public void  BuildFindKey()
    {
        var Buildings = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Buildings;
        foreach (var building in Buildings)
        {
            var descript = building.Value as Dictionary<string, object>;
            foreach (var des in descript)
            {
                if (des.Key == "name" && des.Value.ToString() == DataKey)
                {
                    GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedBuilding = building.Key;
                    SceneManager.LoadScene("SearchMapScene");
                }
            }
        }
    }

    public void CollegeKeyFind()
    {
        var Colleges = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Colleges;
        foreach (var college in Colleges)
        {
            var descript = college.Value as Dictionary<string, object>;
            foreach (var des in descript)
            {
                if (des.Key == "name" && des.Value.ToString() == DataKey)
                {
                    GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedCollege = college.Key;
                    SceneManager.LoadScene("SearchMapScene");
                }
            }
        }
    }
}
