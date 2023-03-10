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
        //player tagi bulunan objeyi target olarak alalým ve objenin transformu ile eþitleyelim
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }

    void Update()
    {
        // aradaki mesafeyi ölçmek için basit bir çýkarma iþlemi yapalým
        Vector3 direction = target.position - transform.position;
        
        // çýkarma iþlemini yaptýktan sonra y ekseninde haraket etmemesini saðlamak için sýfýra eþitleyelim 
        direction.y = 0;

        // magnitude özelliði direction vektörünün uzaklýðýný hesaplar

        if (direction.magnitude > 1.0f)
        {
            // Normalize etmek, vektörü ayný yönde fakat büyüklüðü 1 olan birim vektöre dönüþtürmek anlamýna gelir.
            direction = direction.normalized;
            controller.Move(direction * speed * Time.deltaTime);
        }

        // rotasyonunu targeta göre ayarlýyalým
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }

}
