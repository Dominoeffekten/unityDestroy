using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeExpolosion : MonoBehaviour {
    //timer for explosion
    float timer = 2;
    float countdown;
    //default false
    bool hasExploded;

    //GameObject
    public GameObject explosionParticle;

    // Start is called before the first frame update
    void Start() {
        countdown = timer;
    }

    // Update is called once per frame
    void Update() {
        //lowering the countdown
        countdown -= Time.deltaTime;

        
        if(countdown <= 0 && !hasExploded){
            Explode();
        }
    }
    //Make it explode 
    void Explode() {
        Instantiate(explosionParticle, transform.position, transform.rotation);
        hasExploded = true;
    }
}
