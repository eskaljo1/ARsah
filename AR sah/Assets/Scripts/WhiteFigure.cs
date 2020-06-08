using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteFigure : MonoBehaviour
{
    public static bool attacking = false;
    public bool pawn;
    private GameObject board;
    public GameObject queenPrefab;
    public GameObject graveyard;

    void Start()
    {
        attacking = false;
        board = GameObject.FindGameObjectWithTag("B");
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Black" && BlackFigure.attacking)
        {
            if (graveyard == null)
                Destroy(gameObject);
            else
                transform.position = graveyard.transform.position;
        }
        else if (collider.gameObject.tag == "WhiteEnd" && pawn)
        {
            GameObject g = Instantiate(queenPrefab, board.transform);
            g.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
