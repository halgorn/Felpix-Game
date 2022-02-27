using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassarinhoJogador : MonoBehaviour
{
    
    bool iniciouJogo;
    bool acabouJogo;
    Vector2 forcaImpulso = new Vector2(0,250);
    public GameObject ParticulasPenas;

    GameObject gameEngine;

    //variaveis para o som
    public AudioClip somVoa;
    public AudioClip somHit;
    public AudioClip somScore;

    gameEngine.GetComponent<AudioSource>().PlayOneShot(somVoa);
    gameEngine.GetComponent<AudioSource>().PlayOneShot(somHit);
    gameEngine.GetComponent<AudioSource>().PlayOneShot(somScore)
    // Start is called before the first frame update
    void Start()
    {
        gameEngine = GameObject.FindGameObjectWithTag("GameEngine");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !acabouJogo){
            if(!iniciouJogo){
                iniciouJogo = true;
                GetComponent<Rigidbody2D>().isKinematic = false;
                gameEngine.SendMessage("ComecouJogo");
            }
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(forcaImpulso);
            GameObject particula = Instantiate(ParticulasPenas);
            particula.transform.position = gameObject.transform.position;
        }

        transform.rotation = Quaternion.Euler(0,0, GetComponent<Rigidbody2D>().velocity.y * 5f);

        Vector2 stagePos = Camera.main.WorldToScreenPoint(transform.position);
        if(stagePos.y > Screen.height || stagePos.y < 0){
            acabouJogo = true;
            gameEngine.SendMessage("FimDeJogo");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(){

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        GetComponent<Rigidbody2D>().AddForce(new Vector2(-500,0));

        acabouJogo = true;
        gameEngine.SendMessage("FimDeJogo");
    }

     void OnTriggerExit2D(Collider2D col) {
        if(col.CompareTag("AreaVao")){
            gameEngine.SendMessage("marcaPonto");
        }
    }
}
