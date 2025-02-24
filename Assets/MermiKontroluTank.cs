using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermiKontroluTank : MonoBehaviour
{
    public float verdigiZarar = 100f;
    private GemimizinKontrolüTank gemimizinKontrolu;

    public void SetGemimizinKontrolu(GemimizinKontrolüTank kontrol)
    {
        gemimizinKontrolu = kontrol;
    }

    public void CarptigindaYokOl()
    {
        // Mermi yok olmadan önce gemi kontrolüne bilgi ver
        if (gemimizinKontrolu != null)
        {
            gemimizinKontrolu.MermiYokOldu();
        }

        Destroy(gameObject);
    }

    public float ZararVerme()
    {
        return verdigiZarar;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
        // Eðer mermi baþka bir þeye çarptýysa yok et
     //   if (collision.gameObject.CompareTag("Dusmanlar"))
     //   {
     //       CarptigindaYokOl();
    //    }
    //}
}
