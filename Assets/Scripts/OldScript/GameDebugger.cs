using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===========================================================
// Do not run this script and do not enable the game object
// attached to it called "Debugging" in a production environment
// this is for testing purpose only.
// ===========================================================
public class GameDebugger : MonoBehaviour
{
    GameObject[] pokeballs;
    // Start is called before the first frame update
    void Start() {
        pokeballs = GameObject.FindGameObjectsWithTag("pokeball");
        Debug.Log("pokeballs list size: " + pokeballs.Length);

    }

    // Update is called once per frame
    // Uncomment the function you want (only 1 at the time) to test it.
    void Update() {
        DebugPokeballs(pokeballs);
        //DebugPOVGraph(nodes_go_povg);
        //DebugCloset(nodes_go);
        //ClusterID(nodes_go);
    }

    void DebugPokeballs(GameObject[] pokeballs) {
        Debug.Log("DebugPokeballs functions in Debugger called!");
    
        //foreach (GameObject pokeball in pokeballs) {
        //    pokeball.GetComponent<Renderer>().enabled = true;
        //}
        for(int i = 0; i < pokeballs.Length; i++) {
            for(int j = 0; j < pokeballs.Length; j++) {
                if(i != j) {
                    if(pokeballs[i].transform.position.x == pokeballs[j].transform.position.x && pokeballs[i].transform.position.z == pokeballs[j].transform.position.z)
                    {
                        Debug.Log("name: " + pokeballs[i].gameObject.name);
                    }
                }
            }
        }
    }

    void DebugPOVGraph(GameObject[] nodes) {
        Debug.Log("DebugPOVGraph functions in Debugger called!");

        foreach (GameObject node in nodes) {
            node.GetComponent<Renderer>().enabled = true;
        }
    }

    void DebugCloset(GameObject[] nodes)
    {
        Debug.Log("DebugCloset function in debugger called!");
        foreach (GameObject node in nodes) {
            node.GetComponent<Renderer>().enabled = true;
            Vector3 pos = node.transform.position;
            if (pos.x <= -30 && pos.z <= -32) {
                node.GetComponent<Renderer>().material.color = Color.blue;
            }
            else if (pos.x <= -16 && pos.z >= 28) {
                node.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (pos.x >= 30 && pos.z >= 20) {
                node.GetComponent<Renderer>().material.color = Color.green;

            }
            else if (pos.x >= 28 && pos.z <= -28) {
                node.GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
    }

    void ClusterID(GameObject[] nodes) {
        Debug.Log("ClusterID functions in Debugger called!");
        foreach(GameObject node in nodes) {
            node.GetComponent<Renderer>().enabled = true;
            if(node.layer == 9) {
                node.GetComponent<Renderer>().material.color = new Color(0.95f, 0.0f, 0.0f, 0.35f);
            } else if(node.layer == 10) {
                node.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.95f, 0.35f);
            } else if(node.layer == 11) {
                node.GetComponent<Renderer>().material.color = new Color(0.0f, 0.95f, 0.0f, 0.35f);
            } else if(node.layer == 12) {
                node.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.94f, 0.35f);
            } else if(node.layer == 13) {
                node.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 0.0f, 0.35f);
            } else if(node.layer == 14) {
                node.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 1.0f, 0.35f);
            }
        }
    }
}
