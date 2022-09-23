using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonOnClick : MonoBehaviour
{
    public GameObject buttonImage;

    public void OnArClick()
    {
        Image Graphic = buttonImage.GetComponent<Image>();
        Graphic.color = Color.black;
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
}
