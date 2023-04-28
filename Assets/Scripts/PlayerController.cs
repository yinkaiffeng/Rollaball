using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float Speed = 0.0f; 
    private Rigidbody rb;
    public TextMeshProUGUI CountText;
    public GameObject WintextObject;
    private int count;
    private float movementX;
    private float movementY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
      SetCountText();
      WintextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        //Function Body
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        CountText.text = "Count:" + count.ToString();
        if (count >= 6)
        {
            WintextObject.SetActive(true);

            StartCoroutine(ExampleCoroutine());
            
        }
    }

    IEnumerator ExampleCoroutine()
    {
       yield return new  WaitForSeconds(5);
       
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement*Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count += 1;
       SetCountText();
            Debug.Log(count);
        }
        
    }
}
