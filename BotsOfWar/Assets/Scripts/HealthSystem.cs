// Skrypt �ledzi, czy gracze na mapie wesz�y w kontakt z pociskiem. Je�li tak, to odbierane jest im zdrowie

using System.Collections.Generic;
using UnityEngine;

public class HealthTracker : MonoBehaviour
{
    public List<GameObject> players;

    private void Start()
    {
        // Pobierz wszystkie obiekty z tagiem "Player" i dodaj je do listy "players"
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject playerObject in playerObjects)
            players.Add(playerObject);
    }

    private void Update()
    {
        foreach (GameObject player in players)
        {
            // Sprawd� czy gracz wszed� w interakcj� z innym obiektem
            var collider = Physics2D.OverlapBox(player.GetComponent<BoxCollider2D>().transform.position,
                player.GetComponent<BoxCollider2D>().size, 0f);

            if(collider.CompareTag("Bullet"))
            {
                var playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    if (collider != null)
                        // Wy��cz komponent BoxCollider2D uderzaj�cego obiektu (po to, �eby jeden pocisk zabiera� dok�adnie jedno zdrowie)
                        collider.enabled = false;

                    playerHealth.TakeDamage(1); // Przyjmijmy, �e gracz ma 100 HP
                }
            }
        }
    }
}