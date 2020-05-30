using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFigure : MonoBehaviour
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

    void Update()
    {
        if (pawn && transform.position.x > 5.0f)
        {
            GameObject g = Instantiate(queenPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.13f), Quaternion.identity);
            g.transform.SetParent(board.transform);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "White" && WhiteFigure.attacking)
        {
            Destroy(gameObject);
        }
    }
}
