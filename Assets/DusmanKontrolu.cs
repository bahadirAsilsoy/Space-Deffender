using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanKontrolu : MonoBehaviour
{
    public GameObject mermi;
    public float mermiHizi = 5f;
    public float can = 150f;
    public int skorDegeri = 200;
    private SkorKontrolu skorKontrolu;

    public AudioClip AtesSesi;
    public AudioClip OlumSesi;

    public GameObject patlama;

    private void Start()
    {
        skorKontrolu = GameObject.Find("Skor").GetComponent<SkorKontrolu>();
    }

    // public float saniyeBasinaMermiAtma = 0.6f; //dusmanmermi atma sýklýðý

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        MermiKontrolu carpanMermi = collision.gameObject.GetComponent<MermiKontrolu>();
        MermiKontroluTank carpanMermiTank = collision.gameObject.GetComponent<MermiKontroluTank>();
        if (carpanMermi)
        {
            StartCoroutine(FlashRedCoroutine());
            carpanMermi.CarptigindaYokOl();
            can -= carpanMermi.ZararVerme();
            if (can <= 0)
            {
                Instantiate(patlama, transform.position, Quaternion.identity);
                Destroy(gameObject);
                if(OlumSesi != null)
                {
                    AudioSource.PlayClipAtPoint(OlumSesi, transform.position);
                }
                skorKontrolu.SkoruArttir(skorDegeri);
            }
            
        }
        else if (carpanMermiTank)
        {
            StartCoroutine(FlashRedCoroutine());
            carpanMermiTank.CarptigindaYokOl();
            can -= carpanMermiTank.ZararVerme();
            if (can <= 0)
            {
                Instantiate(patlama, transform.position, Quaternion.identity);
                Destroy(gameObject);
                if (OlumSesi != null)
                {
                    AudioSource.PlayClipAtPoint(OlumSesi, transform.position);
                }
                skorKontrolu.SkoruArttir(skorDegeri);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        // float atmaOlasiligi = Time.deltaTime * saniyeBasinaMermiAtma; // dusman mermi atma sýklýgý 
        // if (Random.value < atmaOlasiligi )
        if (Random.value < 0.00025f )
        {
            AtesEt();

        }
    }

    void AtesEt()
    {
        if (mermi == null)
        {
            return;
        }
        Vector3 baslangicPozisyonu = transform.position + new Vector3(0, -0.3f, 0);
        GameObject dusmaninMermisi = Instantiate(mermi, baslangicPozisyonu, Quaternion.identity) as GameObject;
        dusmaninMermisi.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -mermiHizi);
        AudioSource.PlayClipAtPoint(AtesSesi, transform.position);

    }

    private IEnumerator FlashRedCoroutine()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.07f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
