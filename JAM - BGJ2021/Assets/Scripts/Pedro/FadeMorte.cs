using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMorte : MonoBehaviour
{
    public GameObject fadeMorte;

    public void ativa(){
		StartCoroutine("espera");
	}

    public void ativaRapido(){
		fadeMorte.active = true;
	}

    public void desativa(){
		fadeMorte.active = false;
	}

    IEnumerator espera()
    {
        yield return new WaitForSeconds(1);
        fadeMorte.active = true;
    }


}
