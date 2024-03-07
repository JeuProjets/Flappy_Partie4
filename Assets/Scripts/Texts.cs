using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Texts : MonoBehaviour
{
    public TextMeshProUGUI leTitre;
    public TextMeshProUGUI leCompteur;

    int compte;

    Color laCouleur;

    // Start is called before the first frame update
    void Start()
    {
        laCouleur = leTitre.color;
        laCouleur.a = 0;
        leTitre.color = laCouleur;

        InvokeRepeating("Chrono", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(compte == 3)
        {
            CancelInvoke();

            leTitre.enabled = true;
            leCompteur.enabled = false;

            laCouleur.a += 0.001f;
            leTitre.color = laCouleur;


            if(leTitre.fontSize < 225)
            {
                leTitre.fontSize++;
            }
        }
    }

    void Chrono()
    {
        leCompteur.text = "" + compte; // ou   compte.ToString();
        compte++;
        print(compte);
    }
}
