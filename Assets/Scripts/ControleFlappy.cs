using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ControleFlappy : MonoBehaviour
    
{
    //variables pour choisir la vitesse des colonnes, decors... 
    public float vitesseX;
    public float vitesseY;

    //variables pour enregistrer le Sprite de Flappy apr�s les collisions
    public Sprite mortHaut;
    public Sprite mortBas;
    public Sprite vieHaut;
    public Sprite vieBas;

    //variables pour cr�er les GameObject 
    public GameObject pieceOr;
    public GameObject packVie;
    public GameObject leChampignon;
    public GameObject laGrille;


    //variables pour d�clencher les sons
    public AudioClip sonColonnes;
    public AudioClip sonPieceOr;
    public AudioClip sonPackVie;
    public AudioClip sonChampignon;

    public AudioClip sonFinPartie;

    //Valeurs bool�ennes pour d�termin�e si le jeu est termin�
    public bool flappyBlesse;
    public bool partieTerminee;

    //Cr�ation des titres
    public TextMeshProUGUI texteFin;
    public TextMeshProUGUI lePointage;
   


    int score = 0;


    private void Start()
    {
        
    }

    void Update()
    {
        if (partieTerminee == false)
        {
            //Conditions pour d�placer Flappy
            //Droite
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);
            }
            //Gauche
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-vitesseX, vitesseY);
            }

            //Haut
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);
                if (flappyBlesse == false)
                {
                    GetComponent<SpriteRenderer>().sprite = vieBas;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = mortBas;
                }
            }
            //Controle du Sprite de Flappu quand il monte selon s'il est bless�
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                if(flappyBlesse == false)
                {
                    GetComponent<SpriteRenderer>().sprite = vieHaut;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = mortHaut;
                }
            }
            //Le texte de la fin n'apparait pas
            texteFin.enabled = false;
        }
        if(partieTerminee == true)
        {
            //On fait apparaitre le texte de la fin
            texteFin.enabled = true;
            
        }

    }
    
    //Fonction pour d�tecter les collisions entre Flappy et les gameObjects
    void OnCollisionEnter2D(Collision2D collisionsFlappy)
    {

        if (collisionsFlappy.gameObject.name == "Colonne" || collisionsFlappy.gameObject.name == "Decor")
        {
            if (flappyBlesse == false)
            {
                //On change l'image de Flappy
                GetComponent<SpriteRenderer>().sprite = mortHaut;
                //On met la variable de flappy bless� � true
                flappyBlesse = true;
            }
            else
            {
                //La partie est termin�e
                partieTerminee = true;

                //il peut tourner
                GetComponent<Rigidbody2D>().freezeRotation = false;

                //Ajout d'une vitesse angulaire
                GetComponent<Rigidbody2D>().angularVelocity = 100;

                //D�sactive le collider de flappy
                GetComponent<Collider2D>().enabled = false;

                //Son fin de partie
                GetComponent<AudioSource>().PlayOneShot(sonFinPartie, 2f);
                Invoke("RecommencerJeu", 3f);

                //- 10 dans le pointage
                score -= 5;

                // Mettre � jour l'affichage du score
                lePointage.text = "Pointage: " + score.ToString();

            }

            //Son qui joue une seule fois
            GetComponent<AudioSource>().PlayOneShot(sonColonnes);
        }
        if (collisionsFlappy.gameObject.name == "PieceOr")
        {
            print(collisionsFlappy.gameObject);

            //On d�sactive l'objet
            collisionsFlappy.gameObject.SetActive(false);

            //Appel de la fonction qui le r�active
            Invoke("ReactiverPieceOr", 3f);

            //Son qui joue une seule fois
            GetComponent<AudioSource>().PlayOneShot(sonPieceOr);

            //+ 5 dans le pointage
            score +=5;

            // Mettre � jour l'affichage du score
            lePointage.text = "Pointage: " + score.ToString();

            //Animation de la grille
            laGrille.GetComponent<Animator>().enabled = true;
            Invoke("DesactiverGrille", 4f);

        }
        if (collisionsFlappy.gameObject.name == "PackVie")
        {
            print(collisionsFlappy.gameObject);

            //On d�sactive l'objet 
            collisionsFlappy.gameObject.SetActive(false);

            //Appel de la fonction qui le r�active
            Invoke("ReactiverPackVie",3f);

            //On change l'image de Flappy
            GetComponent<SpriteRenderer>().sprite = vieHaut;

            //Son qui joue une seule fois
            GetComponent<AudioSource>().PlayOneShot(sonPackVie);

            //+ 5 dans le pointage
            score += 5;

            // Mettre � jour l'affichage du score
            lePointage.text = "Pointage: " + score.ToString();
        }
        if (collisionsFlappy.gameObject.name == "Champignon")
        {
            print(collisionsFlappy.gameObject);

            //On d�sactive l'objet
            collisionsFlappy.gameObject.SetActive(false);

            //Appel de la fonction qui le r�active et qui r�duit sa taille
            Invoke("ReactiverChampignon", 4f);

            //Grossissement la taille de Flappy pendant quelques secondes
            transform.localScale *= 1.3f;

            //Son qui joue une seule fois
            GetComponent<AudioSource>().PlayOneShot(sonChampignon);
            
            //+ 10 dans le pointage
            score += 10;

            // Mettre � jour l'affichage du score
            lePointage.text = "Pointage: " + score.ToString();
        }

    }
    //Fonction pour r�activer la pi�ce d'or
    private void ReactiverPieceOr()
    {
        pieceOr.SetActive(true);

        //On met une position al�atoire � son apparition
        float positionPieceOr = Random.Range(-1, 1);
        pieceOr.transform.position = new Vector2(pieceOr.transform.position.x, positionPieceOr);
    }

    //Fonction pour r�activer le pack de vie
    private void ReactiverPackVie()
    {
        packVie.SetActive(true);

    }
    //Fonction pour r�activer le champignon
    private void ReactiverChampignon()
    {
        leChampignon.SetActive(true);

        //on r�duit sa taille quand il apparait
        transform.localScale /= 1.3f;
    }
    //Fonction pour recommencer le jeu, on charge la sc�ne qui controle flappy
    private void RecommencerJeu()
    {
        SceneManager.LoadScene("ControleFlappy");
    }
    //Fonction qui d�sactive la grille
    private void DesactiverGrille()
    {
        laGrille.GetComponent<Animator>().enabled = false;
    }
}
