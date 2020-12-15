using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerObjet : MonoBehaviour
{
    public Camera camera;
    public Transform Parent1;
    public Transform Parent2;
    public Transform Joueur;
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
                if(Parent1.childCount <= 0)
                {
                    Transform objectHit = hit.transform;
                    //objectHit.gameObject.SetActive(false);
                    //Ceci indique le nom de l'objet selectionné
                    Debug.Log(objectHit);
                    //On enleve le tag Object car sinon cette fonction pourrait se re-appliquer surl'objet selectionné
                    // /!\ Je pense que l'on pourrait l'enlever
                    objectHit.tag = "Untagged";
                    //On déplace l'objet en tant que Child de GameObject
                    objectHit.SetParent(Parent1, true);
                    //On recupére le Rigidbody et on désactive la gravitée et on active isKinematic pour qu'il ne soit plus sujet a la gravité
                    Rigidbody RB = objectHit.GetComponent<Rigidbody>();
                    RB.useGravity = false;
                    RB.isKinematic = true;
                    //On désactive le collider de l'objet sélectionné pour qu'il ne puisse plus intéragir avec notre personnage
                    Collider col = objectHit.GetComponentInChildren<Collider>();
                    col.enabled = false;

                    //On récupere la position du Parent et on utilise c'est coordonnées pour déplacer l'objet sélectionné
                    //Vector3 position = Parent1.position;
                    Quaternion rotation = Joueur.rotation;
                    objectHit.rotation = rotation;
                    objectHit.localPosition = Vector3.zero;
                    //objectHit.SetPositionAndRotation(position, rotation);
                }
            }
        }
    }
    void TrowObject()
    {

    }
        // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if((Parent1.childCount <= 0))
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
    }
}     