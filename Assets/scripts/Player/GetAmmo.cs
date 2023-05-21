using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAmmo : MonoBehaviour
{
    public int ammoAmount = 30;  // ���������� �������� ��� ����������

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            if (collider.TryGetComponent<PlayerAimWeapon>(out var playerAimWeapon))
            {
                playerAimWeapon.AddAmmo(ammoAmount);
                Destroy(gameObject);  // ���������� ������ � ��������� ����� ����������
            }
        }
    }
}
