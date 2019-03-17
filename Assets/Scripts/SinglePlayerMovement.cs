using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerMovement : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        //CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMovement();
    }

    void InputMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            this.GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.forward * speed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
            this.GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
            this.GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
            this.GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.right * speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "pokeball")
        {
            collision.gameObject.SetActive(false);
        }
        else
        {
            return;
        }
    }
}
