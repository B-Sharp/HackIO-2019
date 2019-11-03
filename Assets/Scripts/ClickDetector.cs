﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickDetector : MonoBehaviour
{

    public GameObject UI;
    public float rotationSpeed = 2.0f;
    public float orbitSpeed = 10000.0f;
    public float flyInsSpeed = 1000f;
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
           float planarDistance = (transform.position.x - this.target.position.x) * (transform.position.x - this.target.position.x) + (transform.position.x - this.target.position.x) * (transform.position.z - this.target.position.z);
           planarDistance = Mathf.Sqrt(planarDistance);
           this.flyInsSpeed = 1000f * planarDistance * 100f;
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
                UI.transform.GetChild(0).GetComponent<Text>().enabled = true;
                UI.transform.GetChild(1).GetComponent<Image>().enabled = true;
                UI.transform.GetChild(2).GetComponent<Text>().enabled = true;
            }
         } else if (Input.GetMouseButtonDown(1)) {
             target = null;
             this.rotateCamera = false;
             transform.eulerAngles = this.originalAngles;
            UI.transform.GetChild(0).GetComponent<Text>().enabled = false;
            UI.transform.GetChild(1).GetComponent<Image>().enabled = false;
            UI.transform.GetChild(2).GetComponent<Text>().enabled = false;
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
