using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make a grenade explode after 2 sec.
public class bombe : MonoBehaviour {

    //cubes
    public float cubeSize = 0.2f;
    public int cubesInRow = 5;
    float cubesPivotDistance;
    Vector3 cubesPivot;

    //is it exploxed? default false
    bool hasExploded;

    //radius for the elements flying
    public float radius = 3;
    public float force = 10;

    // Use this for initialization
    void Start() {
        //calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        //use this value to create pivot vector)
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Floor" && !hasExploded) {
            explode();
        }

    }

    public void explode() {
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
        pieceRender.material.color = new Color32(10,94,14,255);
        //Shader
        //pieceRender.material.shader = Shader.Find("Diffuse"); 

        //add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
    }
}
