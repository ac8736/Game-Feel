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

    public bool CanInvincible = true;
    private bool isInvincible = false;
    private Animator anim;
    private Animator childAnim;
    private TrailRenderer trailRenderer;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody2D>();
        healthbar.SetMaxHealth(playerHealth);
        anim = GetComponent<Animator>();
        childAnim = transform.GetChild(3).gameObject.GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanInvincible)
        {
            StartCoroutine(Invincible());
        }

        if (playerHealth <= 0 && !IsDead)
        {
            Die();
            return;
        }

        trailRenderer.enabled = GameFeelConfig.config[GameFeelFeature.MovementTrail];
    }

    private void FixedUpdate()
    {
        if (IsDead) return;
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
        if (GameFeelConfig.config[GameFeelFeature.CameraShake])
        {
            float magnitude = (playerHealth > 0) ? 0.05f : 0.2f;
            if (playerHealth > 0)
            {
                GameController.self.CameraController.Shake(0.4f, 0.1f);
            }
            else
            {
                GameController.self.CameraController.Shake(0.6f, 0.25f);
            }
        }
    }

    void TakeDamage(int dmg)
    {
        if (GameFeelConfig.config[GameFeelFeature.ScreenFlash])
        {
            GameController.self.FlashGUI.SetTrigger("flash");
        }
        playerHealth -= dmg;
        healthbar.SetHealth(playerHealth);
    }

    IEnumerator Invincible()
    {
        childAnim.SetTrigger("shield");
        CanInvincible = false;
        healthbar.SetShieldUI(CanInvincible);
        isInvincible = true;
        healthbar.SetInvincible(isInvincible);
        yield return new WaitForSeconds(3);
        childAnim.SetTrigger("unshield");
        isInvincible = false;
        healthbar.SetInvincible(isInvincible);
        StartCoroutine(InvincibleCoolDown());
    }

    IEnumerator InvincibleCoolDown()
    {
        yield return new WaitForSeconds(invincibleCoolDown);
        CanInvincible = true;
        healthbar.SetShieldUI(CanInvincible);
    }

    void Die()
    {
        if (IsDead) return;
        IsDead = true;
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        trailRenderer.emitting = false;
        anim.SetTrigger("death");
        _rigidBody.velocity *= 0.5f;

        GetComponent<Collider2D>().enabled = false;
        if (GameFeelConfig.config[GameFeelFeature.Particles])
        {
            GetComponentInChildren<ParticleSystem>().Play();
        }
        StartCoroutine(PostDeath());
    }

    IEnumerator PostDeath()
    {
        bool slowMo = GameFeelConfig.config[GameFeelFeature.SlowMoOnDeath];
        // AudioSource.Play();
        AudioSource.pitch = 1.5f;
        if (slowMo) { Time.timeScale = 0.1f; }
        yield return new WaitForSeconds(0.6f);
        Time.timeScale = 1f;
        AudioSource.Stop();
        GameController.GameOver = true;
        GameController.self.GameOverGUI.SetTrigger("fadeIn");
        GameController.self.GameOverGUI.gameObject.SetActive(true);
        Destroy(gameObject);
        yield return null;
    }
}
