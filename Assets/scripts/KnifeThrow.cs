using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnifeThrow : MonoBehaviour
{
    [SerializeField] Vector2 throwForce;
 
    
    private bool isActive = true;
    
     Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Time.timeScale=1f;
   
    }

  
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isActive)
        {
            _rb.AddForce(throwForce,ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive)
        {
            return;
        }
        if (collision.gameObject.tag == "Log")
        {
            isActive = false;
            _rb.velocity = new Vector2(0, 0);
            _rb.constraints = RigidbodyConstraints2D.FreezePosition;
            transform.SetParent(collision.collider.transform );
            LogHealth.instance.updateHealth();
            KnifeSpawner.instance.SpawnKnife();
            
        }
        else if (collision.gameObject.tag == "Knife")
        {
           GameManager.instance.GameOver();
        }



    }

  
}
