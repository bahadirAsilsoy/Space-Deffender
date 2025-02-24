using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemimizinKontrolü : MonoBehaviour
{

    public float hiz = 10f;
    public float inceAyar = 1f;
    public GameObject Mermi;
    public float mermininHizi = 8f;
    public float atesEtmeAraligi = 2f;
    public float can = 30f;
    float xmin;
    float xmax;

    public AudioClip AtesSesi;
    public AudioClip OlumSesi;
    public AudioClip AfterOlumSesi;

    public GameObject patlama;
    // Start is called before the first frame update
    void Start()
    {
        float uzaklik = transform.position.z - Camera.main.transform.position.z;
        Vector3 solUc = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, uzaklik));
        Vector3 sagUc = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, uzaklik));
        xmin = solUc.x + inceAyar;
        xmax = sagUc.x - inceAyar;
    }

    void AtesEtme()
    {
        GameObject gemimizinMermisi = Instantiate(Mermi, transform.position, Quaternion.identity) as GameObject;
        gemimizinMermisi.GetComponent<Rigidbody2D>().velocity = new Vector3(0, mermininHizi, 0);
        AudioSource.PlayClipAtPoint(AtesSesi,transform.position);
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("AtesEtme",0.000001f,atesEtmeAraligi);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("AtesEtme");
        }

        // gemimizin x eksenindeki konumu eğer 8 ile -8 arasındaysa yeniX e ata.
        float yeniX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(yeniX, transform.position.y, transform.position.z);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // transform.position += new Vector3(-hiz * Time.deltaTime, 0, 0);
            // Vector3.left -> (-1,0,0)
            transform.position += Vector3.left * hiz * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.RightArrow))
            {
            // transform.position += new Vector3(+hiz * Time.deltaTime, 0, 0);
            // Vector3.left -> (1,0,0)
            transform.position += Vector3.right * hiz * Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MermiKontrolu carpanMermi = collision.gameObject.GetComponent<MermiKontrolu>();
        DusmanKontrolu dusman = collision.gameObject.GetComponent<DusmanKontrolu>();
        if (carpanMermi)
        {
            StartCoroutine(FlashRedCoroutine());
            can -= carpanMermi.ZararVerme();
            if (can <= 0)
            {
                GameObject.FindWithTag("sayac").GetComponent<CanSayaci>().OyuncuOldu();
                Instantiate(patlama, transform.position, Quaternion.identity);
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(OlumSesi, transform.position);
                AudioSource.PlayClipAtPoint(AfterOlumSesi, transform.position);
            }
            carpanMermi.CarptigindaYokOl();
        }
        else if (dusman)
        {
            StartCoroutine(FlashRedCoroutine());
            can -= 30f;
            Destroy(dusman.gameObject);
            if (can <= 0)
            {
                GameObject.FindWithTag("sayac").GetComponent<CanSayaci>().OyuncuOldu();
                Instantiate(patlama,transform.position, Quaternion.identity);
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(OlumSesi, transform.position);
                AudioSource.PlayClipAtPoint(AfterOlumSesi, transform.position);
            }
        }
    }

    private IEnumerator FlashRedCoroutine()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.07f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

}
