using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnifeThrow : MonoBehaviour
{
    [SerializeField] Vector2 throwForce;
  
    
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
      
    }

  
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _rb.AddForce(throwForce,ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Log")
        {
            _rb.velocity = new Vector2(0, 0);

           
            transform.SetParent(collision.collider.transform ); 
            
            KnifeSpawner.instance.SpawnKnife();
            
        }
        else if (collision.gameObject.tag == "Knife")
        {
            //TODO: game over
        }



    }

  
}
