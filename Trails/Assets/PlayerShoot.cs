using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    AudioSource AudioSource;
    public AudioClip shootingAudio;
    public GameObject m_Projectile;    // this is a reference to your projectile prefab
    public Transform m_SpawnTransform; // this is a reference to the transform where the prefab will spawn

    public float _bulletSpeed = 3f;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GetComponent<PlayerController>().IsDead)
        {
            GameObject bullet = Instantiate(m_Projectile, m_SpawnTransform.position, m_SpawnTransform.rotation);
            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            AudioSource.clip = shootingAudio;
            AudioSource.volume = 1f;
            if (GameFeelConfig.config[GameFeelFeature.Audio])
            {
                AudioSource.Play();
            }

            rigidbody.velocity = _bulletSpeed * transform.up;
        }
    }
}
