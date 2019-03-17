using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonMovement : MonoBehaviour
{
    private PhotonView PV;
    private CharacterController CC;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
        {
            InputMovement();
        }   
    }

    void InputMovement()
    {
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            CC.Move(transform.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
            CC.Move(transform.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
            CC.Move(transform.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
            CC.Move(transform.forward * Time.deltaTime * speed);
        }

    }
}
