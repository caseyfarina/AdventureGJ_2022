using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing_Enemy : MonoBehaviour {
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 5f;
    public GameObject DestroyEffect;
    private GameObject target;
    // Start is called before the first frame update
        void Start() 

    {
        target = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()

    {
        if (target == null)
        {
            Instantiate(DestroyEffect, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
              
        else {
            Vector3 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
            Debug.Log(direction);
        }
    }
    private void FixedUpdate()
    {
        moveCharacter(movement);
    }
    void moveCharacter(Vector2 direction) 
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
