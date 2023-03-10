using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float mouseSense;
    [SerializeField] Transform player, playerArms;

    float xAxisClamp = 0;
    float yAxisClamp = 0;
    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        float rotateX = Input.GetAxis("Mouse X") * mouseSense;
        float rotateY = Input.GetAxis("Mouse Y") * mouseSense;

        xAxisClamp -= rotateX;
        yAxisClamp -= rotateY;

        Vector3 rotPlayerArms = playerArms.rotation.eulerAngles;
        Vector3 rotPlayer = player.rotation.eulerAngles;

        rotPlayerArms.x -= rotateY;
        rotPlayerArms.z = 0;
        rotPlayer.y += rotateX;

        // X ekseni sınırlandırması
        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            
        }

        // Y ekseni sınırlandırması
        if (yAxisClamp > 90)
        {
            yAxisClamp = -90;
            rotPlayerArms.x = 270;
        }
        else if (yAxisClamp < -90)
        {
            yAxisClamp = 90;
            rotPlayerArms.x = 90;
        }

        // playerArms nesnesini karakterin yönüne döndür
        playerArms.LookAt(playerArms.position - transform.right * rotPlayerArms.y);

        playerArms.rotation = Quaternion.Euler(rotPlayerArms);
        player.rotation = Quaternion.Euler(rotPlayer);
    }
}
