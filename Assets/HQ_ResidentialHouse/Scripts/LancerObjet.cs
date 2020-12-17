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
    public Image curseur;
    public float radius;
    public float maxDistance;
    public Text texte;

    [HideInInspector]
    public bool canThrow;
    // Start is called before the first frame update
    void Start()
    {
        canThrow = true;
    }
    //isThrowing == False quand on prend l'objet, et True quand on le lance
    void ActivatePhysicObject(Transform objectHit, bool isThrowing)
    {
        //Ceci indique le nom de l'objet selectionné
        Debug.Log(objectHit);


        //On met soit le parent1 quand on sélectionne, sinon le parent2 quand on lance
        if (isThrowing)
            objectHit.SetParent(parent2, true);
        else
            objectHit.SetParent(parent1, true);

        //On active la gravité et désactive kinematic quand on lance
        Rigidbody RB = objectHit.GetComponent<Rigidbody>();
        RB.useGravity = isThrowing;
        RB.isKinematic = !isThrowing;

        //On active les colliders quand on lance
        Collider[] allColliders = objectHit.GetComponentsInChildren<Collider>(true);
        foreach (Collider c in allColliders)
        {
            c.enabled = isThrowing;
        }
    }

    void SelectObject(Transform objectHit)
    {
 
        //On sauvegarde le parent original de l'objet selectionné
        parent2 = objectHit.parent;
        //On déplace l'objet en tant que Child de GameObject

        ActivatePhysicObject(objectHit, false);

        //On récupere la position du Parent et on utilise c'est coordonnées pour déplacer l'objet sélectionné
        Quaternion rotation = joueur.rotation;
        objectHit.localRotation = Quaternion.Euler(-10f, 0f, 0f);
        objectHit.localPosition = Vector3.zero;
          
    }
    void TrowObject()
    {
        Transform objectLaunch = parent1.GetChild(0);
        ActivatePhysicObject(objectLaunch, true);
        
        objectLaunch.GetComponent<Rigidbody>().AddForce(parent1.forward * thrust, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
      


        if ((parent1.childCount > 0))
        {
            curseur.color = Color.white;
            if (Input.GetMouseButtonDown(0) && canThrow)
            {
                TrowObject();
                parent1.GetComponent<AudioSource>().Play();
            }
        }
        else
        { 
            RaycastHit hit;
            Vector3 origin = camera.transform.position;
            if (Physics.SphereCast(origin, radius, camera.transform.forward, out hit, maxDistance))
            {
                if (hit.transform.CompareTag("Object"))
                {
                    curseur.color = Color.red;
                    texte.text = hit.transform.name;
                    if (Input.GetMouseButtonDown(0) && canThrow)
                    {
                        //Cette fonction permet au joueur de prendre un objet
                        SelectObject(hit.transform);
                    }
                }
                else
                {
                    texte.text = "";
                    curseur.color = Color.white;
                }
            }
            else
            {
                curseur.color = Color.white;
            }
        }
        
    }
}     