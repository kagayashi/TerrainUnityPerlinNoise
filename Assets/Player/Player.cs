using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    public bool ground;
    public float speed = 2f;
    public float runspeed = 3f;
    public Rigidbody rb;
    public float jumpPower = 400f;
    
    Animator Anim;
    public GameObject PauseMenu;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (PauseMenu.activeInHierarchy == true)
            {
                PauseMenu.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                rb.Sleep();
            }
            else
            {
                PauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (PauseMenu.activeInHierarchy == false)
        {
            GetInput();
            Anim = GetComponent<Animator>();
            Anim.SetFloat("Speed", rb.velocity.magnitude);
        }


        
        
        

    }

    private void GetInput()

    {
        
        if(Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {

                transform.localPosition += transform.forward * runspeed * Time.deltaTime;
                rb.MovePosition(transform.localPosition) ;
              
            }
            else
            {
                transform.localPosition += transform.forward * speed * Time.deltaTime;
               
            }
        }
         if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += -transform.forward * speed * Time.deltaTime;
         
        }
         if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += -transform.right * speed * Time.deltaTime;
          
        }
         if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += transform.right * speed * Time.deltaTime;
           
         }

        if (Input.GetKeyDown(KeyCode.Space) && ground)
        {
            rb.AddForce(transform.up * jumpPower);
        }




    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            ground = true;
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = false;
        }

    }
}
