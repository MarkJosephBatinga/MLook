using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollegeArPrefab : MonoBehaviour
{
    public string CollegeKey;

    [SerializeField]
    RawImage Logo;

    [SerializeField]
    TextMeshProUGUI CollegeNameText;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("LoadedData") != null)
        {
            var Colleges = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Colleges;
            var CollegeLogos = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().CollegeLogos;

            if (CollegeKey != null)
            {
                if (Colleges.ContainsKey(CollegeKey))
                {
                    Texture CollegeLogo = CollegeLogos[CollegeKey];
                    Dictionary<string, object> CollegeDes = Colleges[CollegeKey] as Dictionary<string, object>;
                    StartCoroutine(DisplayCollege(CollegeDes, CollegeLogo));
                }
            }
            else
            {
                Debug.Log("No College Key");
            }
        }
        else
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }

    IEnumerator DisplayCollege(Dictionary<string, object> CollegeDes, Texture CollegeLogo)
    {
        foreach (var college in CollegeDes)
        {
            if (college.Key == "name")
            {
                CollegeNameText.text = college.Value.ToString();
            }
        }

        //Set Image Texture;
        Logo.texture = CollegeLogo;

        yield return null;
    }


    public void FacultyClick()
    {
        if(CollegeKey!=null)
        {

            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().CollegeKey = CollegeKey;
            SceneManager.LoadScene("OfficeDescriptionScene");
        }
    }


    public void ProgramClick()
    {
        if (CollegeKey != null)
        {

            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().CollegeKey = CollegeKey;
            SceneManager.LoadScene("ProgramDescriptionScene");
        }
    }

    public void CollegeClick()
    {
        if (CollegeKey != null)
        {

            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().CollegeKey = CollegeKey;
            SceneManager.LoadScene("CollegeDescriptionScene");
        }
    }

    public void StaffClick()
    {
        if (CollegeKey != null)
        {

            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().CollegeKey = CollegeKey;
            SceneManager.LoadScene("StaffDescriptionScene");
        }
    }
}


