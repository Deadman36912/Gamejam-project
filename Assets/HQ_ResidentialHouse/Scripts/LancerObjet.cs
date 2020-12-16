using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LancerObjet : MonoBehaviour
{
    public Camera camera;
    public Transform parent1;
    Transform parent2;
    public Transform joueur;
    public float thrust;
    public GameObject curseur;
    // Start is called before the first frame update
    void Start()
    {
    }
    void SelectObject()
    {
        RaycastHit hit;
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Object"))
            {
                if(parent1.childCount <= 0)
                {
                    Transform objectHit = hit.transform;
                    //objectHit.gameObject.SetActive(false);
                    //Ceci indique le nom de l'objet selectionné
                    Debug.Log(objectHit);
                    //On enleve le tag Object car sinon cette fonction pourrait se re-appliquer surl'objet selectionné
                    // /!\ Je pense que l'on pourrait l'enlever
                    //objectHit.tag = "Untagged";
                    //On sauvegarde le parent original de l'objet selectionné
                    parent2 = objectHit.parent;
                    //On déplace l'objet en tant que Child de GameObject
                    objectHit.SetParent(parent1, true);
                    //On recupére le Rigidbody et on désactive la gravitée et on active isKinematic pour qu'il ne soit plus sujet a la gravité
                    Rigidbody RB = objectHit.GetComponent<Rigidbody>();
                    RB.useGravity = false;
                    RB.isKinematic = true;
                    //On désactive le collider de l'objet sélectionné pour qu'il ne puisse plus intéragir avec notre personnage
                    //Collider col = objectHit.GetComponentInChildren<Collider>();
                    //col.enabled = false;
                    Collider[] allColliders = objectHit.GetComponentsInChildren<Collider>(true);
                    foreach (Collider c in allColliders)
                    {
                        c.enabled = false;
                    }

                    //On récupere la position du Parent et on utilise c'est coordonnées pour déplacer l'objet sélectionné
                    //Vector3 position = Parent1.position;
                    Quaternion rotation = joueur.rotation;
                    objectHit.localRotation = Quaternion.Euler(-10f, 0f, 0f);
                    objectHit.localPosition = Vector3.zero;
                    //objectHit.SetPositionAndRotation(position, rotation);
                }
            }
        }
    }
    void TrowObject()
    {
        Transform objectLaunch = parent1.GetChild(0);

        objectLaunch.SetParent(parent2, true);

        Rigidbody RB = objectLaunch.GetComponent<Rigidbody>();
        RB.useGravity = true;
        RB.isKinematic = false;

        //On désactive le collider de l'objet sélectionné pour qu'il ne puisse plus intéragir avec notre personnage
        //Collider col = objectLaunch.GetComponentInChildren<Collider>();
        //col.enabled = true;
        Collider[] allColliders = objectLaunch.GetComponentsInChildren<Collider>(true);
        foreach (Collider c in allColliders)
        {
            c.enabled = true;
        }

        RB.AddForce(parent1.forward * thrust, ForceMode.Impulse);


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if((parent1.childCount <= 0))
            {
                //Cette fonction permet au joueur de prendre un objet
                SelectObject();
            }
            else
            {
                //Cette fonction permet au joueur de lancer un objet qui se trouve dans leur mains
                TrowObject();
            }
        }
        else
        {
            RaycastHit hit;
            Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Object"))
                {
                    curseur.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                }
                else
                {
                    curseur.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                }
            }
        }
        
    }
}     