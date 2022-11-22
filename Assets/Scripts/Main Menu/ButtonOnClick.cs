using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonOnClick : MonoBehaviour
{

    public void OnArClick()
    {
        SceneManager.LoadScene("ArScene");
    }

    public void OnExploreClick()
    {
        SceneManager.LoadScene("ExploreScene");
    }

    public void OnGoClick()
    {
        SceneManager.LoadScene("GoScene");
    }

    public void OnCollegeClick()
    {
        SceneManager.LoadScene("CollegeScene");
    }

    public void OnAboutClick()
    {
        SceneManager.LoadScene("AboutScene");
    }

    public void OnSearchClick()
    {
        SceneManager.LoadScene("SearchScene");
    }

    public void OnStaffDesClick()
    {
        SceneManager.LoadScene("StaffDescriptionScene");
    }

    public void OnOfficeDesClick()
    {
        SceneManager.LoadScene("OfficeDescriptionScene");
    }

    public void OnProgramDesClick()
    {
        SceneManager.LoadScene("ProgramDescriptionScene");
    }

    public void OnCollegeDesBack()
    {
        SceneManager.LoadScene("CollegeDescriptionScene");
    }
}
