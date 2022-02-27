using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScritPrincipal : MonoBehaviour
{

    public GameObject objetosCanos;
    int pontuacao;
    public Text textoPontuacao;
    public Text textoMensagem;
    bool terminouJogo=false;
    // Start is called before the first frame update
    void Start()
    {
        textoMensagem.gameObject.SetActive(true);
        textoPontuacao.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")&& terminouJogo){
            Application.LoadLevel("Jogo");
        }
    }

    public void ComecouJogo(){
        InvokeRepeating("CriaCanos", 0f, 1.5f);
        textoMensagem.gameObject.SetActive(false);
        textoPontuacao.gameObject.SetActive(true);
    }

    void CriaCanos(){
        float randomPos = (3.0f * Random.value) - 1.5f;
        GameObject cano = Instantiate(objetosCanos);
        cano.transform.localScale = new Vector3(1.65f, 1.65f, 1.65f);
        cano.transform.position = new Vector3(20, randomPos, 0);
    }

    public void FimDeJogo(){
        CancelInvoke("CriaCanos");
        foreach(GameObject objeto in GameObject.FindGameObjectsWithTag("ImagemFundo")){
            objeto.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        foreach(GameObject objeto in GameObject.FindGameObjectsWithTag("AreaVao")){
            objeto.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        Invoke("setLabelsFinais", 2);
    }

    public void marcaPonto()
    {
        pontuacao++;
        textoPontuacao.text = pontuacao.ToString();
    }

    void setLabelsFinais(){
        textoMensagem.gameObject.SetActive(true);
        textoMensagem.text = "Toque para reiniciar!";
        textoMensagem.color = new Color(0.15f,0.35f,0.55f,1);
        terminouJogo = true;
    }
}
