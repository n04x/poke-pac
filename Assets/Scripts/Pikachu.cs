using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class Pikachu : MonoBehaviourPun
{
    public float speed;
    private Rigidbody rb;

    void Start() {
    }

    void Update() {
        if(photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
            return;
        }
        InputMovement();
    }

    void InputMovement() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // move up
        if(h > 0) {
            this.GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.right * speed * Time.deltaTime);
			this.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
        } 
        else if(h < 0) {
            this.GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - Vector3.right * speed * Time.deltaTime);
            this.transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
        }
        else if(v > 0) {
            this.GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.forward * speed * Time.deltaTime);
            this.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        }
        else if(v < 0) {
            this.GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - Vector3.forward * speed * Time.deltaTime);
            this.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "pokeball") {
            GameObject.Destroy(other.gameObject);
            gameObject.GetComponent<PikachuManager>().scores++;

            
        }    
    }
}
