using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkorKontrolu : MonoBehaviour {
    
    public int skor;

    private Text skorMetni;

    private void Start ()
    {
        skorMetni = GetComponent<Text>();
        skoruSifirla();
    }

    public void SkoruArttir(int puanlar)
    {
        skor += puanlar;
        skorMetni.text = skor.ToString();

    }

    public void skoruSifirla()
    {
        skor = 0;
        skorMetni.text = skor.ToString();
    }
}
