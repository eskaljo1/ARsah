using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteFigure : MonoBehaviour
{
    public static bool attacking = false;
    public bool pawn;
    private GameObject board;
    public GameObject queenPrefab;

    void Start()
    {
        attacking = false;
        board = GameObject.FindGameObjectWithTag("B");
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Black" && BlackFigure.attacking)
        {
            Destroy(gameObject);
        }
        else if(collider.gameObject.tag == "WhiteEnd" && pawn)
        {
            GameObject g = Instantiate(queenPrefab, transform);
            g.transform.SetParent(board.transform);
            Destroy(gameObject);
        }
    }
}
