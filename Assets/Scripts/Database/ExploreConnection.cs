using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExploreConnection : MonoBehaviour
{

    public GameObject ConnError;
    public GameObject ExploreText;

    private bool isLoading = true;
    public GameObject SpinImg;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("LoadedData") != null)
        {
            StartCoroutine(Spinner());
            StartCoroutine(ConnectionTest());
        }
        else
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }

    IEnumerator ConnectionTest()
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();

        if(request.error != null)
        {
            SpinImg.SetActive(false);
            isLoading = false;
            

            ConnError.SetActive(true);
            
        }
        else
        {
            SpinImg.SetActive(false);
            isLoading = false;


            ExploreText.SetActive(true);
        }
    }

    private IEnumerator Spinner()
    {
        while(isLoading)
        {
            SpinImg.transform.Rotate(0, 0, -2);
            yield return null;
        }
    }
}
