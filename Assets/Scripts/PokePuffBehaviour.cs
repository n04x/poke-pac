using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokePuffBehaviour : MonoBehaviour
{
    SkinnedMeshRenderer[] meshes;
    public float spawn_timer = 30.0f;   // spawn the pokebuff every 30 seconds.
    // Start is called before the first frame update
    void Start()
    {
        meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer sm in meshes)
        {
            sm.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Spawner();
    }
    void Spawner() {
        spawn_timer -= Time.deltaTime;
        if(spawn_timer <= 0) {
            foreach (SkinnedMeshRenderer sm in meshes)
            {
                sm.enabled = true;
            }
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") {
            GameSetup.GS.pokepuff_eaten = true;
            foreach (SkinnedMeshRenderer sm in meshes)
            {
                sm.enabled = false;
            }
        } else
        {
            return;
        }
    }
}
