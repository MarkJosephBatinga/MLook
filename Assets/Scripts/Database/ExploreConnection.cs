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

    private bool isLoading = true;
    public GameObject SpinImg;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("LoadedData") != null)
        {
            StartCoroutine(Spinner());
            StartCoroutine(ExploreLoad());
        }
        else
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }

    IEnumerator ExploreLoad()
    {
        isLoading = false;
        SpinImg.SetActive(false);

        yield return null;
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
