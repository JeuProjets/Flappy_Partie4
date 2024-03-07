using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controleBus : MonoBehaviour {

    public float vitesseX;
    public float vitesseY;
    public GameObject laBombe;
    public Sprite busExplose;

    public AudioClip sonBombe;
    public AudioClip tombe;

    //public GameObject musique;
    public AudioSource musique;



	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/

	 void Update (){

        //NDC, game object musique, jouer ou pause
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Si il joue
            //if (musique.GetComponent<AudioSource>().isPlaying)
            if (musique.isPlaying)
            {
                //On met sur pause
                //musique.GetComponent<AudioSource>().Pause();
                musique.Pause();
            }
            else
            {
                //musique.GetComponent<AudioSource>().Play();
                musique.Play();
            }
        }

        if (Input.GetKey("right") || Input.GetKey("d")) {

            vitesseX = 3;
        }
        
        else if (Input.GetKey("left") || Input.GetKey("a")) {
            vitesseX = -3;
         } else {
            vitesseX = GetComponent<Rigidbody2D>().velocity.x; //vitesse horizontale actuelle
        }

        if (Input.GetKeyDown("up") || Input.GetKeyDown("w")) {
            vitesseY = 6;
         } else {
            vitesseY = GetComponent<Rigidbody2D>().velocity.y; //vitesse verticale actuelle
        }

         GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);



	}


    private void OnCollisionEnter2D(Collision2D infoObjetTouche) {
        
        //print(infoObjetTouche.gameObject.name + "\n");
       
        if(infoObjetTouche.gameObject.name == "Boom"){

            GetComponent<SpriteRenderer>().sprite = busExplose;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
            GetComponent<Rigidbody2D>().angularVelocity = 100;

            //NDC, le son joue une fois, 3 fois plus fort
            GetComponent<AudioSource>().PlayOneShot(sonBombe, 3f);


            infoObjetTouche.gameObject.SetActive(false);
   

            Invoke("reapparaitreBombe", 2f);

        }
        if(infoObjetTouche.gameObject.name == "ZoneMortel")
        {
            GetComponent<AudioSource>().PlayOneShot(tombe);
            infoObjetTouche.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            Invoke("Rejouer", 2f);
        }
                              
        
    }



    void reapparaitreBombe(){

        laBombe.SetActive(true);
    }

    void Rejouer()
    {
        SceneManager.LoadScene(0); //La scène 0, ou on peut mettre le nom de la scène entre ""
    }
}

