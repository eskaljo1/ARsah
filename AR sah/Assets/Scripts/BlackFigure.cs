using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFigure : MonoBehaviour
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
        if (collider.gameObject.tag == "White" && WhiteFigure.attacking)
        {
            if (graveyard == null)
                Destroy(gameObject);
            else
                transform.position = graveyard.transform.position;
        }
        else if (collider.gameObject.tag == "BlackEnd" && pawn)
        {
            GameObject g = Instantiate(queenPrefab, board.transform);
            g.transform.position = transform.position;
            transform.position = graveyard.transform.position;
        }
    }
}
