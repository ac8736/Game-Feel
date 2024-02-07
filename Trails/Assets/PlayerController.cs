using System;
using System.Collections;
using Assets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HealthManager healthbar;
    public int playerHealth = 100;
    public float playerSpeed = 25f;
    AudioSource AudioSource;
    Rigidbody2D _rigidBody;

    public bool IsDead { get; private set; } = false;

    public float invincibleCoolDown = 5.0f;

    private bool canInvincible = true;
    private bool isInvincible = false;
    private TrailRenderer trailRenderer;


    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody2D>();
        healthbar.SetMaxHealth(playerHealth);
        trailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canInvincible)
        {
            StartCoroutine(Invincible());
        }

        trailRenderer.enabled = GameFeelConfig.config[GameFeelFeature.MovementTrail];
    }

    private void FixedUpdate()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 movementDirection = (cursorPos - transform.position).normalized;
        _rigidBody.velocity = movementDirection * playerSpeed;
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x);
        float angleDegrees = Mathf.Rad2Deg * angle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleDegrees - 90f));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rock") && !isInvincible)
        {
            TakeDamage(20);
        }    
    }

    void TakeDamage(int dmg)
    {
        playerHealth -= dmg;
        healthbar.SetHealth(playerHealth);
        if (playerHealth < 0)
        {
            IsDead = true;
        }
    }

    IEnumerator Invincible()
    {
        canInvincible = false;
        healthbar.SetShieldUI(canInvincible);
        isInvincible = true;
        healthbar.SetInvincible(isInvincible);
        yield return new WaitForSeconds(3);
        isInvincible = false;
        healthbar.SetInvincible(isInvincible);
        StartCoroutine(InvincibleCoolDown());
    }

    IEnumerator InvincibleCoolDown()
    {
        yield return new WaitForSeconds(invincibleCoolDown);
        canInvincible = true;
        healthbar.SetShieldUI(canInvincible);
    }
}
