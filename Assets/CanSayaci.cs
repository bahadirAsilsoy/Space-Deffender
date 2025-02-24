using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CanSayaci : MonoBehaviour
{

    public int can = 3;
    public TextMeshProUGUI text;
    public GameObject gemi;
    public GameObject GameOver;
    public GameObject[] gemiler;


    public void OyuncuOldu()
    {
        can--;
        text.text = "Life: " + can;
        if (can == 0)
        {
            GameOver.SetActive(true);
            return;
        }
        else
        {
            StartCoroutine(Respawn());
        }
    }

    public void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this.transform.parent.gameObject);
        Instantiate(gemiler[PlayerPrefs.GetInt("Gemi")], new Vector3(0, -4, 0), Quaternion.identity);
    }

    public void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Instantiate(gemiler[PlayerPrefs.GetInt("Gemi")], new Vector3(0, -4, 0), Quaternion.identity);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(gemiler[PlayerPrefs.GetInt("Gemi")], new Vector3(0, -4, 0), Quaternion.identity);
    }

    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("menu");
        Destroy(transform.parent.gameObject);

    }

    public void yokEt()
    {
        Destroy(GameObject.FindGameObjectWithTag("sayac").transform.parent.gameObject);
    }

}
