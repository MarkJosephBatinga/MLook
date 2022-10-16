using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ExploreConnection : MonoBehaviour
{

    public GameObject ConnError;
    public TextMeshProUGUI ExploreText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ConnectionTest());
    }

    IEnumerator ConnectionTest()
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();

        if(request.error != null)
        {
            ConnError.SetActive(true);
        }
        else
        {
            ExploreText.text = " ";
        }
    }
}
