using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    #region Variables

    private bool _firstTouch;

    #endregion

    #region Components

    private GameObject _player;

    #endregion

    #region OnSceneStart

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    #endregion

    #region OnEachFrame

    void Update()
    {
        // Gets the Current GameObject Position
        Vector3 pos = this.gameObject.transform.position;

        // Moves GameObject by Reducing its 'x' Position
        this.gameObject.transform.position = new Vector3(pos.x - MapSpeed(), pos.y, pos.z);

        // Destroys GameObject After Certain Distance
        if (Vector3.Distance(this.gameObject.transform.position, _player.transform.position) > 60)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region Events

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If GameObject Colliders with Another
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }

        // If GameObject Colliders with Player
        if (collision.gameObject.tag == "Player")
        {
            if (!_firstTouch)
            {
                Player.Score += 1;
                _firstTouch = true;
            }
        }
    }

    #endregion

    #region Methods

    float MapSpeed()
    {
        // Defines GameObject Movement Speed by Player's Score
        if (Player.Score > 40)
        {
            return 0.30f;
        }
        else if (Player.Score > 30)
        {
            return 0.25f;
        }
        else if (Player.Score > 20)
        {
            return 0.20f;
        }
        else if (Player.Score > 10)
        {
            return 0.15f;
        }
        return 0.1f;
    }

    #endregion
}
