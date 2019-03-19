using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PokemonBehaviour : NetworkBehaviour
{
    #region Variables.
    public float speed;
    private Text score_text;
    private bool should_die = false;
    public bool is_dead = false;
    private bool tagged = false;
    public delegate void DieDelegate();
    public event DieDelegate EventDie;

    [SyncVar(hook = "OnScoreChange")] private int score;
    [SyncVar] private Vector3 syncPos;
    [SyncVar] private float syncRot;

    [SerializeField] Transform myTransform;
    [SerializeField] float lerpRate = 15;
    #endregion

    #region Standard MonoBehiavour fucntions
    // Start is called before the first frame update
    void Start()
    {
        score_text = GameObject.Find("Score Text").GetComponent<Text>();
        SetScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            if(!tagged)
            {
                InputMovement();
            }
            TransmitPosition();
        }
        else
        {
            LerpPosition();
        }
    }

    #endregion

    #region MonoBehaviour functions

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
        if (transform.position.z > 9.0f)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
        }
        if (transform.position.z < -10.5f)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, 8.5f);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "pokeball")
        {
            collision.gameObject.SetActive(false);
            //avatar_pokemon_setup.scores++;
            IncreaseScore();
        } else if(collision.gameObject.tag == "Gengar")
        {
            //tagged = true;
            //GetComponent<BoxCollider>().enabled = false;
            //GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            return;
        }
    }

    void SetScoreText()
    {
        if (isLocalPlayer)
        {
            score_text.text = "Score " + score.ToString();
        }
    }

    public void IncreaseScore()
    {
        score++;
    }

    void OnScoreChange(int s)
    {
        score = s;
        SetScoreText();
    }

    void LerpPosition()
    {
        Vector3 newRotation = new Vector3(0, syncRot, 0);
        myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, Quaternion.Euler(newRotation), Time.deltaTime * lerpRate);
    }
    
    void CheckDeath()
    {
        if(tagged && !should_die && !is_dead)
        {
            should_die = true;
        }

        if (tagged && should_die)
        {
            if(EventDie != null)
            {
                EventDie();
            }
            should_die = false;
        }
    }
    
    #endregion

    #region NetworkBehaviour functions
    [Command]
    void CmdProvidePositionToServer(Vector3 pos, float rot)
    {
        syncPos = pos;
        syncRot = rot;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        CmdProvidePositionToServer(myTransform.position, myTransform.localEulerAngles.y);
    }
    #endregion
}
