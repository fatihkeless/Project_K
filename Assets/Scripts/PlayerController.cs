using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform rifleStart;
    [SerializeField] private Text HpText;

    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject Victory;


    public float speed = 6.0f;         // Hareket hýzý
    public float jumpSpeed = 8.0f;     // Zýplama hýzý
    public float gravity = 20.0f;      // Yerçekimi katsayýsý

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    public float health = 0;

    void Start()
    {
        
        controller = GetComponent<CharacterController>();
        ChangeHealth(100);
        

    }

    public void ChangeHealth(int hp)
    {
        health += hp;
        if (health > 100)
        {
            health = 100;
        }
        else if (health <= 0)
        {
            Lost();
        }
        HpText.text = health.ToString();
    }

    public void Win()
    {
        Victory.SetActive(true);
        Destroy(GetComponent<PlayerLook>());
        Cursor.lockState = CursorLockMode.None;
    }

    public void Lost()
    {
        GameOver.SetActive(true);
        Destroy(GetComponent<PlayerLook>());
        Destroy(GetComponent<PlayerController>());
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        playerMove();

        if (Input.GetMouseButtonDown(0))
        {
            GameObject buf = Instantiate(bullet);
            buf.transform.position = rifleStart.position;
            buf.GetComponent<Bullet>().setDirection(transform.forward);
            buf.transform.rotation = transform.rotation;
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            Collider[] tar = Physics.OverlapSphere(transform.position, 2);
            foreach (var item in tar)
            {
                if (item.tag == "Enemy")
                {
                    Destroy(item.gameObject);
                }
            }
        }

        Collider[] targets = Physics.OverlapSphere(transform.position, 3);
        foreach (var item in targets)
        {
            if (item.tag == "Heal")
            {
                ChangeHealth(50);
                Destroy(item.gameObject);
            }
            if (item.tag == "Finish")
            {
                Win();
            }
            if (item.tag == "Enemy")
            {
                // enemy bize deðince biraz canýmýz azalasýn
                Destroy(item.gameObject);
                ChangeHealth(-34);
            }
        }
    }


    private void playerMove()
    {
        if (controller.isGrounded)
        {
            // Hareket yönü hesaplanýyor
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            // Zýplama iþlemi
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Yerçekimi uygulanýyor
        moveDirection.y -= gravity * Time.deltaTime;

        // Hareket iþlemi uygulanýyor
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(rifleStart.position, 0.1f);
    }
}
