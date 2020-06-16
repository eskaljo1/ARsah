using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFigureMP : MonoBehaviour
{
    public static int queenID = 200;
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
    void MoveToBlackGY()
    {
        transform.position = graveyard.transform.position;
    }

    [PunRPC]
    void DestroyFigure()
    {
        Destroy(gameObject);
    }

    [PunRPC]
    void BlackQueenSpawn()
    {
        g = Instantiate(queenPrefab, board.transform);
        g.transform.position = transform.position;
        g.GetComponent<PhotonView>().ViewID = queenID;
        queenID++;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "White" && WhiteFigure.attacking)
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
                    MoveToBlackGY();
                else
                {
                    PhotonView photonView = PhotonView.Get(GetComponent<PhotonView>());
                    photonView.RPC("MoveToBlackGY", RpcTarget.All);
                }
            }
        }
        else if (collider.gameObject.tag == "BlackEnd" && pawn)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                BlackQueenSpawn();
                MoveToBlackGY();
            }
            else
            {
                PhotonView photonView = PhotonView.Get(GetComponent<PhotonView>());
                photonView.RPC("BlackQueenSpawn", RpcTarget.All);
                photonView.RPC("MoveToBlackGY", RpcTarget.All);
            }
        }
    }
}
