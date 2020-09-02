using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make a grenade explode after 2 sec.
public class grenadeExplosion : MonoBehaviour {
    //timer for explosion
    float timer = 2;
    float countdown;
    //radius for the elements flying
    float radius = 3;
    float force = 10;
    //default false
    bool hasExploded;

    //cubes
    public float cubeSize = 0.2f;
    public int cubesInRow = 5;
    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;


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
        //the bombe effect
         //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++) {
            for (int y = 0; y < cubesInRow; y++) {
                for (int z = 0; z < cubesInRow; z++) {
                    createPiece(x, y, z);
                }
            }
        }

        //the elements around it
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        
        foreach (Collider nearbyObject in colliders){
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody> ();
            if (rb != null){
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        //check if the elements has exploded or not 
        hasExploded = true;
        Destroy(gameObject);
    }

    void createPiece(int x, int y, int z) {

        //create piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //change the..
        var pieceRender = piece.GetComponent<Renderer>();

        //Color
        pieceRender.material.SetColor("_Color", Color.black);
        //Shader
        //pieceRender.material.shader = Shader.Find("Diffuse"); 

        //add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
    }
   
}


