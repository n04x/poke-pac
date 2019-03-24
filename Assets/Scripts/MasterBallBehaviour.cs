using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterBallBehaviour : MonoBehaviour
{
    const float FLASH_TIMER_VALUE = 0.5f;
    const float SPAWN_TIMER_VALUE = 10.0f;
    private float flashing_timer = FLASH_TIMER_VALUE;
    private float spawn_timer = SPAWN_TIMER_VALUE;
    public bool spawned = false;
    private SkinnedMeshRenderer mesh;
    private SphereCollider sphere_collider;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        sphere_collider =  GetComponent<SphereCollider>();

        mesh.enabled = false;
        sphere_collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawned)
        {
            Flashing();
        }
        else
        {
            Spawner();
        }
    }

    void Spawner()
    {
        spawn_timer -= Time.deltaTime;
        if(spawn_timer < 0.0f)
        {
            mesh.enabled = true;
            sphere_collider.enabled = true;
            spawned = true;
            spawn_timer = 45.0f;
        }
    }
    void Flashing()
    {
        flashing_timer -= Time.deltaTime;
        if(flashing_timer < 0.0 && mesh.enabled)
        {
            mesh.enabled = false;
            flashing_timer = FLASH_TIMER_VALUE;
        }
        else if(flashing_timer < 0.0 && !mesh.enabled)
        {
            mesh.enabled = true;
            flashing_timer = FLASH_TIMER_VALUE;

        }
        spawn_timer = SPAWN_TIMER_VALUE;
    }
}
