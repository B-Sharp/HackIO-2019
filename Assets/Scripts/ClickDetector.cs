using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickDetector : MonoBehaviour
{

    public GameObject UI;
    public float rotationSpeed = 2.0f;
    public float flyInsSpeed = 1000f;
    public float flySpeed = 10f;
    public float radius = 5.0f;

    private Transform target;
    private Vector3 targetOrbit;
    private float X;
    private float Y;
    private bool rotateCamera = false;

    void Start () {
    }

    public void setTarget (GameObject obj) {
        if (obj.tag == "Building") {
           this.target = obj.transform;
           this.targetOrbit = new Vector3(this.target.position.x, this.target.position.y + 1f * this.target.lossyScale.y, this.target.position.z);
           float planarDistance = (transform.position.x - this.target.position.x) * (transform.position.x - this.target.position.x) + (transform.position.x - this.target.position.x) * (transform.position.z - this.target.position.z);
           planarDistance = Mathf.Sqrt(planarDistance);
           this.flyInsSpeed = 1000f * planarDistance * 100f;
            this.rotateCamera = true;
            UI.GetComponent<UIController>().updateUI(obj.transform.gameObject);
            UI.transform.GetChild(0).GetComponent<Text>().enabled = true;
            UI.transform.GetChild(1).GetComponent<Image>().enabled = true;
            UI.transform.GetChild(2).GetComponent<Text>().enabled = true;
            UI.transform.GetChild(4).GetComponent<Text>().enabled = true;
            UI.transform.GetChild(5).GetComponent<Text>().enabled = true;
            UI.transform.GetChild(6).GetComponent<Text>().enabled = true;
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
            }
         } else if (Input.GetMouseButtonDown(1)) {
             target = null;
             this.rotateCamera = false;
            UI.transform.GetChild(0).GetComponent<Text>().enabled = false;
            UI.transform.GetChild(1).GetComponent<Image>().enabled = false;
            UI.transform.GetChild(2).GetComponent<Text>().enabled = false;
            UI.transform.GetChild(4).GetComponent<Text>().enabled = false;
            UI.transform.GetChild(5).GetComponent<Text>().enabled = false;
            UI.transform.GetChild(6).GetComponent<Text>().enabled = false;
         } else if (Input.GetMouseButton(2) && !this.rotateCamera) {
             look();
         }

         if (rotateCamera) {
             rotateTowardsAngleSmooth();
             rotateAroundTarget();
         } else {
            getInput();
         }

        UI.GetComponent<UIController>().updateCompass(transform);
    }

    private void look(){
        transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * 3.0f, Input.GetAxis("Mouse X") * 3.0f, 0));
        X = transform.rotation.eulerAngles.x;
        Y = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(X, Y, 0);
    }

    private void getInput () {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            flySpeed *= 2.0f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            flySpeed /= 2.0f;
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(Vector3.forward * this.flySpeed * Time.deltaTime * Input.GetAxis("Vertical"));
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(Vector3.right * this.flySpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        }
    }

    private void rotateTowardsAngleSmooth () {

        Quaternion targetRotation = Quaternion.LookRotation(this.target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, this.rotationSpeed * Time.deltaTime);
        
    }

    private void rotateAroundTarget () {
        transform.RotateAround(this.targetOrbit, Vector3.up, 20f * Time.deltaTime);
        Vector3 desiredPosition = (transform.position - this.targetOrbit).normalized * this.radius + this.targetOrbit;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * 10f);
    }
}
