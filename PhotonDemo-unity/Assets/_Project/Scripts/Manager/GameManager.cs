﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables

    [Header("Settings")]
    [SerializeField]
    private GameObject m_panel;

    [SerializeField]
    private GameObject m_hudScorePrefab;

    [SerializeField]
    private List<PlayerConn> m_PlayerList;
    public List<PlayerConn> PlayerList { get => m_PlayerList; set => m_PlayerList = value; }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerGo = PhotonNetwork.Instantiate(Constants.PlayerPrefab, new Vector3(Random.Range(-5, 5), 0, 0), Quaternion.identity);
        PlayerList.Add(playerGo.GetComponent<PlayerConn>());

        playerGo.GetComponent<PlayerConn>().Init();

        //GameObject m_Hud = PhotonNetwork.Instantiate(Constants.HUDPlayer, Vector3.zero, Quaternion.identity);
        //m_Hud.transform.SetParent(m_panel.transform);
        //m_Hud.SetActive(true);

        //playerGo.GetComponent<PlayerConn>().ScoreHud = m_Hud.GetComponent<ScoreHud>();
        //m_Hud.GetComponent<ScoreHud>().TxtLabel.text = playerGo.GetComponent<PlayerConn>().NameLabel.text;

        SubscriveEvent();
    }

    private void OnDestroy()
    {
        UnSubscriveEvent();
    }

    private void SubscriveEvent()
    {
        Conn.Instance.OnDisconnectedAction += LoadMain;
    }

    private void UnSubscriveEvent()
    {
        Conn.Instance.OnDisconnectedAction -= LoadMain;
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public void LoadMain()
    {
        SceneManager.LoadScene(Constants.MainScene);
    }

}
