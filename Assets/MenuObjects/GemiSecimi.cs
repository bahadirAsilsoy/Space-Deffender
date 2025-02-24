using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GemiSecimi : MonoBehaviour
{
    public void GemiSec(int index)
    {
        PlayerPrefs.SetInt("Gemi", index);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
