using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleSystem : MonoBehaviour {


    
    ParticleSystem rend;
    [SerializeField] MeshRenderer player;

    private void Start()
    {
        rend = GetComponent<ParticleSystem>();

    }

    private void Update()
    {
        rend.startColor = Color.white/5 + player.material.color;
    }



}
