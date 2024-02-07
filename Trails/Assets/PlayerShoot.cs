using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject m_Projectile;    // this is a reference to your projectile prefab
    public Transform m_SpawnTransform; // this is a reference to the transform where the prefab will spawn

    public float _bulletSpeed = 3f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(m_Projectile, m_SpawnTransform.position, m_SpawnTransform.rotation);
            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

            rigidbody.velocity = _bulletSpeed * transform.up;
        }
    }
}
