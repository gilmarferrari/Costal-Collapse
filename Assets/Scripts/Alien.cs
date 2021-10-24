using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    #region Componets

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
        this.gameObject.transform.position = new Vector3(pos.x - AlienSpeed(), pos.y, pos.z);

        // Destroys GameObject After Certain Distance
        if (Vector3.Distance(this.gameObject.transform.position, _player.transform.position) > 60)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region Methods

    float AlienSpeed()
    {
        // Defines GameObject Movement Speed by Player's Score
        if (Player.Score > 40)
        {
            return 0.30f;
        }
        else if (Player.Score > 30)
        {
            return 0.26f;
        }
        else if (Player.Score > 20)
        {
            return 0.24f;
        }
        else if (Player.Score > 10)
        {
            return 0.22f;
        }
        return 0.2f;
    }

    #endregion
}
