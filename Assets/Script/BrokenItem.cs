using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenItem : MonoBehaviour
{
    //makes a compoment 
    public GameObject replacement;

    void OnCollisionEnter(){
        //an extising object that you wan to make a copy of
        GameObject.Instantiate(replacement, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
