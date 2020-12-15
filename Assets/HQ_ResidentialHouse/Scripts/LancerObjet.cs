using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerObjet : MonoBehaviour
{
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
    }
        // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Object"))
                {
                Transform objectHit = hit.transform;
                objectHit.gameObject.SetActive(false);
                Debug.Log(objectHit);
                 }
            }
    }
}     