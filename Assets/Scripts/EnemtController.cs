using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemtController : MonoBehaviour
{

    private Transform target;
    public float speed = 5.0f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        //player tagi bulunan objeyi target olarak alal�m ve objenin transformu ile e�itleyelim
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }

    void Update()
    {
        // aradaki mesafeyi �l�mek i�in basit bir ��karma i�lemi yapal�m
        Vector3 direction = target.position - transform.position;
        
        // ��karma i�lemini yapt�ktan sonra y ekseninde haraket etmemesini sa�lamak i�in s�f�ra e�itleyelim 
        direction.y = 0;

        // magnitude �zelli�i direction vekt�r�n�n uzakl���n� hesaplar

        if (direction.magnitude > 1.0f)
        {
            // Normalize etmek, vekt�r� ayn� y�nde fakat b�y�kl��� 1 olan birim vekt�re d�n��t�rmek anlam�na gelir.
            direction = direction.normalized;
            controller.Move(direction * speed * Time.deltaTime);
        }

        // rotasyonunu targeta g�re ayarl�yal�m
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }

}
