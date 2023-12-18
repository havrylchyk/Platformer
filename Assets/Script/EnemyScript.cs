using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float damageDistance = 2f;
    [SerializeField] private int damageAmount = 1;

    private void Update()
    {
        // Find the player by checking if it has the MovementScript component
        MovementScript player = FindObjectOfType<MovementScript>();

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < damageDistance)
            {
                player.TakeDamage();

                Debug.Log("Player Lives left: " + player.lives);

                if (player.lives <= 0)
                {
                    Debug.Log("Game Over");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}
