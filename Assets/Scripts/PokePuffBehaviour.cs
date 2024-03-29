﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokePuffBehaviour : MonoBehaviour
{
    public SkinnedMeshRenderer[] meshes;
    public float spawn_timer = 35.0f;   // spawn the pokebuff every 35 seconds.
    // Start is called before the first frame update
    void Start()
    {
        meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        GetComponent<SphereCollider>().enabled = false;
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
        if(spawn_timer < 0) {
            GetComponent<SphereCollider>().enabled = true;
            foreach (SkinnedMeshRenderer sm in meshes)
            {
                sm.enabled = true;
                spawn_timer = 30.0f;
            }
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") {
            GameSetup.GS.pokepuff_eaten = true;
            GetComponent<SphereCollider>().enabled = false;
            foreach (SkinnedMeshRenderer sm in meshes)
            {
                sm.enabled = false;
            }
            spawn_timer = 30.0f;
        }
        else
        {
            return;
        }
    }
}
