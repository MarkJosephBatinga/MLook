using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollegeDatabaseConn : MonoBehaviour
{
    public TextMeshProUGUI CollegeNameText;
    public GameObject ConnError;
    public GameObject ScrollArea;
    public GameObject CollegeBox;
    public GameObject BoxContainer;
    private RawImage RwImage;

    private bool isLoading = true;
    public GameObject SpinImg;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("LoadedData") != null)
        {
            var Colleges = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Colleges;
            var CollegeLogos = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().CollegeLogos;
            StartCoroutine(Spinner());
            StartCoroutine(DisplayColleges(Colleges, CollegeLogos));
        }
        else
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }


    public IEnumerator DisplayColleges(Dictionary<string, object> CollegeInfo, Dictionary<string, Texture> CollegeLogos)
    {
        foreach (var college in CollegeInfo)
        {
            var collegeDes = college.Value as Dictionary<string, object>;
            GameObject collegeBox = Instantiate(CollegeBox, BoxContainer.transform);
            var collegeNameText = collegeBox.transform.Find("College_Name");
            if (CollegeLogos.ContainsKey(college.Key))
            {
                var collegeLogo = collegeBox.transform.Find("Logo");
                RwImage = collegeLogo.GetComponent<RawImage>();
                RwImage.texture = CollegeLogos[college.Key];
            }
            foreach (var description in collegeDes)
            {
                if (description.Key == "name")
                {
                    collegeNameText.GetComponent<TextMeshProUGUI>().text = description.Value.ToString();
                }
            }
            if(CollegeInfo.Last().Key == college.Key)
            {
                SpinImg.SetActive(false);
                isLoading = false;
                ScrollArea.SetActive(true);
            }
        }

        yield return true;
    }

    private IEnumerator Spinner()
    {
        while (isLoading)
        {
            SpinImg.transform.Rotate(0, 0, -2f);
            yield return null;
        }
    }
}

