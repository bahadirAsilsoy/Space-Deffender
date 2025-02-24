using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeteorKontrolu : MonoBehaviour
{
    public List<GameObject> objs;
    public float genislik;
    public float yukseklik;
    private float xmax;
    private float xmin;
    public float yaratmayiGeciktirmeSuresi = 0.6f;

    //kemaliden sonrasý
    public List<Transform> positions;
    public int spawnCount = 0;

    // keremden sonrasý
    bool checkEnemies = false;
    public Transform meteorPozisyonu;

    public GameObject SelectObj()
    {
        return objs[Random.Range(0, objs.Count)];
    }

    // Start is called before the first frame update
    void Start()
    {
        float objeIleKameraninZsininFarki = transform.position.z - Camera.main.transform.position.z;
        Vector3 kameraninSolTarafi = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, objeIleKameraninZsininFarki));
        Vector3 kameraninSagTarafi = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, objeIleKameraninZsininFarki));
        xmax = kameraninSagTarafi.x;
        xmin = kameraninSolTarafi.x;
        DusmanlarinTekTekYaratilmasi();
        StartCoroutine(delay());
    }

    void DusmanlarinTekTekYaratilmasi()
    {
        if (spawnCount > 50)
        {
            // instance
        }
        else
        {
            GameObject dusman = Instantiate(SelectObj(), positions[Random.Range(0, positions.Count)]);
            dusman.transform.position = new Vector3(dusman.transform.position.x, dusman.transform.position.y, 0);
            dusman.transform.parent = meteorPozisyonu.transform;
            dusman.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1, 1), -Random.Range(4, 8));

            Invoke("DusmanlarinTekTekYaratilmasi", 0.3f);

            spawnCount++;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(genislik, yukseklik));
    }

    // Update is called once per frame
    void Update()
    {
        if (ButunDusmanlarOlduMu())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        Debug.Log(checkEnemies);
    }

    bool ButunDusmanlarOlduMu()
    {
        if(checkEnemies == false)
        {
            return false;
        }
        return meteorPozisyonu.childCount <= 0;
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        checkEnemies = true;
    }
}
