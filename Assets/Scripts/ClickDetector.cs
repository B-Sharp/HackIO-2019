using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{

    public GameObject UI;
    public float rotationSpeed = 2.0f;
    public float orbitSpeed = 10000.0f;
    public float flySpeed = 1000f;
    public float radius = 5.0f;

    private Transform target;
    private Vector3 targetOrbit;
    private Vector3 originalAngles;
    private bool rotateCamera = false;

    void Start () {
        this.originalAngles = transform.eulerAngles;
    }

    public void setTarget (GameObject obj) {
        if (obj.tag == "Building") {
           this.target = obj.transform;
           this.targetOrbit = new Vector3(this.target.position.x, this.target.position.y + 1f * this.target.lossyScale.y, this.target.position.z);
        }
    } 

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown(0) ) {
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            RaycastHit hit;
            
            if( Physics.Raycast( ray, out hit, 100 ) )
            {   
                setTarget(hit.transform.gameObject);
                this.rotateCamera = true;
                UI.GetComponent<UIController>().updateUI(hit.transform.gameObject);
            }
         } else if (Input.GetMouseButtonDown(1)) {
             target = null;
             this.rotateCamera = false;
             transform.eulerAngles = this.originalAngles;
         }

         if (rotateCamera) {
             rotateTowardsAngleSmooth();
             rotateAroundTarget();
         }
    }

    private void rotateTowardsAngleSmooth () {

        Quaternion targetRotation = Quaternion.LookRotation(this.target.transform.position - transform.position);
        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, this.rotationSpeed * Time.deltaTime);
    }

    private void rotateAroundTarget () {
        transform.RotateAround(this.targetOrbit, Vector3.up, 20f * Time.deltaTime);
        Vector3 desiredPosition = (transform.position - this.targetOrbit).normalized * this.radius + this.targetOrbit;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * 10f);
    }
}
