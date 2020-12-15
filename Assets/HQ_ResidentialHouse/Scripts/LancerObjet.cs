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
                    Debug.Log(objectHit);
                    objectHit.tag = "Untagged";
                    objectHit.SetParent(Parent1, true);
                    Rigidbody RB = objectHit.GetComponent<Rigidbody>();
                    RB.useGravity = false;
                    RB.isKinematic = true;
                    Collider col = objectHit.GetComponentInChildren<Collider>();
                    col.enabled = false;

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
                SelectObject();
            }
            else
            {
                TrowObject();
            }
        }
    }
}     