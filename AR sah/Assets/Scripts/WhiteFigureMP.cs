using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteFigureMP : MonoBehaviour
{
    public static int queenID = 400;
    public static bool attacking = false;
    public bool pawn;
    private GameObject board;
    public GameObject queenPrefab;
    public GameObject graveyard;

    private GameObject g;

    void Start()
    {
        attacking = false;
        board = GameObject.FindGameObjectWithTag("B");
    }

    [PunRPC]
    void Move(int viewId)
    {
        GameObject g = PhotonView.Find(viewId).gameObject;
        transform.position = g.transform.position;
    }

    [PunRPC]
    void MoveToWhiteGY()
    {
        transform.position = graveyard.transform.position;
    }

    [PunRPC]
    void DestroyFigure()
    {
        Destroy(gameObject);
    }

    [PunRPC]
    void WhiteQueenSpawn()
    {
        g = Instantiate(queenPrefab, board.transform);
        g.transform.position = transform.position;
        g.GetComponent<PhotonView>().ViewID = queenID;
        queenID++;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Black" && BlackFigure.attacking)
        {
            if (graveyard == null)
            {
                if (PhotonNetwork.IsMasterClient)
                    DestroyFigure();
                else
                {
                    PhotonView photonView = PhotonView.Get(GetComponent<PhotonView>());
                    photonView.RPC("DestroyFigure", RpcTarget.All);
                }
            }
            else
            {
                if (PhotonNetwork.IsMasterClient)
                    MoveToWhiteGY();
                else
                {
                    PhotonView photonView = PhotonView.Get(GetComponent<PhotonView>());
                    photonView.RPC("MoveToWhiteGY", RpcTarget.All);
                }
            }
        }
        else if (collider.gameObject.tag == "WhiteEnd" && pawn)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                WhiteQueenSpawn();
                MoveToWhiteGY();
            }
            else
            {
                PhotonView photonView = PhotonView.Get(GetComponent<PhotonView>());
                photonView.RPC("WhiteQueenSpawn", RpcTarget.All);
                photonView.RPC("MoveToWhiteGY", RpcTarget.All);
            }
        }
    }
}
