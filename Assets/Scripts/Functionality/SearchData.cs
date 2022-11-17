using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SearchData : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TMP_InputField inputText;

    [SerializeField]
    GameObject SearchValue;

    [SerializeField]
    GameObject SearchDataBox;


    void Start()
    {
        if (GameObject.FindGameObjectWithTag("LoadedData") != null)
        {
            inputText.onValueChanged.AddListener(delegate {
                DestroySearchData();
                if(inputText.text.Length >= 3)
                {
                    SearchDictionary();
                }
                else
                {
                    SearchValue.SetActive(false);
                }
            });
        }
        else
        {
            SceneManager.LoadScene("LoadingScene");
        }
       
    }

    void DestroySearchData()
    {
        var SearchDatas = GameObject.FindGameObjectsWithTag("SearchData");

        if(SearchDatas.Count() != 0)
        {
            foreach (var searchData in SearchDatas)
            {
                GameObject.Destroy(searchData);
            }
        }
    }

    private void SearchDictionary()
    {
        StartCoroutine(SearchBuilding(inputText.text));
        StartCoroutine(SearchCollege(inputText.text));
        StartCoroutine(SearchStaffs(inputText.text));
    }

    IEnumerator SearchBuilding(string textInp)
    {
        var Buildings = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Buildings;
        foreach (var building in Buildings)
        {
            var buildingDes = building.Value as Dictionary<string, object>;
            var searchValue = buildingDes.Where(inp => inp.Value.ToString().Contains(textInp)).ToList();
            if(searchValue != null)
            {
                foreach (var searchData in searchValue)
                {
                    if(searchData.Key == "name")
                    {
                        SearchValue.SetActive(true);
                        GameObject BuildingData = Instantiate(SearchDataBox, SearchValue.transform);
                        var SearchText = BuildingData.transform.Find("SearchText");
                        SearchText.GetComponent<TextMeshProUGUI>().text = searchData.Value.ToString();
                    }                   
                }
            }
        }

        yield return null;
    }

    IEnumerator SearchCollege(string textInp)
    {
        var Colleges = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Colleges;
        foreach (var college in Colleges)
        {
            var collegeDes = college.Value as Dictionary<string, object>;
            var searchValue = collegeDes.Where(inp => inp.Value.ToString().Contains(textInp)).ToList();
            if (searchValue != null)
            {
                foreach (var searchData in searchValue)
                {
                    if (searchData.Key == "name")
                    {
                        SearchValue.SetActive(true);
                        GameObject CollegeData = Instantiate(SearchDataBox, SearchValue.transform);
                        var SearchText = CollegeData.transform.Find("SearchText");
                        SearchText.GetComponent<TextMeshProUGUI>().text = searchData.Value.ToString();
                    }
                }
            }
        }
        yield return null;
    }

    IEnumerator SearchStaffs(string textInp)
    {
        var Staffs = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Staffs;
        foreach (var office in Staffs)
        {
            var officeVal = office.Value as Dictionary<string, object>;
            foreach (var staffs in officeVal)
            {
                var staffDes = staffs.Value as Dictionary<string, object>;
                var searchValue = staffDes.Where(inp => inp.Value.ToString().Contains(textInp)).ToList();
                if (searchValue != null)
                {
                    foreach (var searchData in searchValue)
                    {
                        if (searchData.Key == "Name")
                        {
                            SearchValue.SetActive(true);
                            GameObject CollegeData = Instantiate(SearchDataBox, SearchValue.transform);
                            var SearchText = CollegeData.transform.Find("SearchText");
                            SearchText.GetComponent<TextMeshProUGUI>().text = searchData.Value.ToString();
                        }
                    }
                }
            }
           
        }
        yield return null;
    }
}
