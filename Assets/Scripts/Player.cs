using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variables

    private bool _jumping;
    private bool _canUseDash;
    public static int Score;

    #endregion

    #region Components

    public Animator Animator;
    public Image ImgDash;
    public Sprite DashSprite;
    public Sprite DisabledDash;
    public TextMeshProUGUI TxtScore;
    public Config Config;
    private Rigidbody2D _rb;
    private GameObject _player;

    #endregion

    #region OnSceneStart

    void Start()
    {
        _player = this.gameObject;
        _rb = _player.GetComponent<Rigidbody2D>();
    }

    #endregion

    #region OnEachFrame

    void Update()
    {
        // Sets Dash Skill Image
        ImgDash.sprite = _canUseDash && _jumping ? DashSprite : DisabledDash;

        // Sets the Score Text
        TxtScore.text = "Score: " + Score.ToString();
    }

    #endregion

    #region OnGUI Event

    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Dash();
        }
    }

    #endregion

    #region Events

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // Resets Both Jump and Dash Skills
            _jumping = false;
            _canUseDash = true;
            Animator.SetBool("Roll", false);
        }

        if (collision.gameObject.tag == "Alien")
        {
            // Sets Game Over
            Config.GameOver();
            // Player Looses Collider
            this.gameObject.GetComponent<Collider2D>().isTrigger = true;
        }

        if (collision.gameObject.tag == "DeathZone")
        {
            // Sets Game Over
            Config.GameOver();
            // Player Gets Collider Back
            this.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _jumping = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DeathZone")
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    #endregion

    #region Methods

    public void Jump()
    {
        // If not Jumping and Game is not Over
        if (!_jumping && !Config.IsDead)
        {
            // Jump
            _rb.velocity = new Vector2(0, 10);
        }
    }

    public void Dash()
    {
        // If Jumping, Can Use Dash and Game is not Over
        if (_jumping && _canUseDash && !Config.IsDead)
        {
            // Dash
            _rb.velocity = new Vector2(0, 2);
            Animator.SetBool("Roll", true);
            _canUseDash = false;
        }
    }

    #endregion
}
