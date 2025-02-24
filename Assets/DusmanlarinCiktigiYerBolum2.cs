using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DusmanlarinCiktigiYerBolum2 : MonoBehaviour
{
    public GameObject dusmanPrefabi;
    public float genislik;
    public float yukseklik;
    private float hiz = 4f;
    private bool SagaHareket = true;
    private float xmax;
    private float xmin;
    public float yaratmayiGeciktirmeSuresi = 0.3f;

    // Start is called before the first frame update
    void Start()
    {

        float objeIleKameraninZsininFarki = transform.position.z - Camera.main.transform.position.z;
        Vector3 kameraninSolTarafi = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, objeIleKameraninZsininFarki));
        Vector3 kameraninSagTarafi = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, objeIleKameraninZsininFarki));
        xmax = kameraninSagTarafi.x;
        xmin = kameraninSolTarafi.x;
        DusmanlarinTekTekYaratilmasi();

    }

    void DusmanlarinYaratilmasi()
    {
        foreach (Transform cocuk in transform)
        {
            GameObject dusman = Instantiate(dusmanPrefabi, cocuk.transform.position, Quaternion.identity) as GameObject;
            dusman.transform.parent = cocuk;
        }
    }

    void DusmanlarinTekTekYaratilmasi()
    {
        Transform uygunPozisyon = SonrakiUygunPozisyon();
        if (uygunPozisyon)
        {
            GameObject dusman = Instantiate(dusmanPrefabi, uygunPozisyon.transform.position, Quaternion.identity) as GameObject;
            dusman.transform.parent = uygunPozisyon;
        }

        if (SonrakiUygunPozisyon())
        {                              // 1f = yaratmayiGeciktirmeSuresi //
            Invoke("DusmanlarinTekTekYaratilmasi", 0.3f);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(genislik, yukseklik));
    }

    // Update is called once per frame
    void Update()
    {
        if (SagaHareket)
        {
            //transform.position += new Vector3(hiz * Time.deltaTime, );
            transform.position += hiz * Vector3.right * Time.deltaTime;
        }
        else
        {
            transform.position += hiz * Vector3.left * Time.deltaTime;
        }

        float sagSinir = transform.position.x + genislik / 2;
        float solSinir = transform.position.x - genislik / 2;

        if (sagSinir > xmax)
        {
            SagaHareket = false;
        }

        else if (solSinir < xmin)
        {
            SagaHareket = true;
        }

        //if (ButunDusmanlarOlduMu())
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}

    }

    Transform SonrakiUygunPozisyon()
    {
        foreach (Transform CocuklarinPozisyonu in transform)
        {
            if (CocuklarinPozisyonu.childCount == 0)
            {
                return CocuklarinPozisyonu;
            }
        }

        return null;
    }

    bool ButunDusmanlarOlduMu()
    {
        foreach (Transform CocuklarinPozisyonu in transform)
        {
            if (CocuklarinPozisyonu.childCount > 0)
            {
                return false;
            }
        }

        return true;
    }


}
