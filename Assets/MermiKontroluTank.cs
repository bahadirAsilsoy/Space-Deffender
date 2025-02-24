using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermiKontroluTank : MonoBehaviour
{
    public float verdigiZarar = 100f;
    private GemimizinKontrol�Tank gemimizinKontrolu;

    public void SetGemimizinKontrolu(GemimizinKontrol�Tank kontrol)
    {
        gemimizinKontrolu = kontrol;
    }

    public void CarptigindaYokOl()
    {
        // Mermi yok olmadan �nce gemi kontrol�ne bilgi ver
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
        // E�er mermi ba�ka bir �eye �arpt�ysa yok et
     //   if (collision.gameObject.CompareTag("Dusmanlar"))
     //   {
     //       CarptigindaYokOl();
    //    }
    //}
}
