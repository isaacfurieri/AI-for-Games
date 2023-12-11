using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviour
{
    public Button hostButton;
    public Button clientButton;
    public TextMeshProUGUI flagsText;
    
    public static ConnectionManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        hostButton.onClick.AddListener(startHost);
        clientButton.onClick.AddListener(startClient);
    }

    void Update()
    {

    }

    private void startClient()
    {
        NetworkManager.Singleton.StartClient();
    }
    private void startHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void UpdateFlags(bool isHost, bool isClient) 
    {
        flagsText.text = "IsHost :: " + isHost + "\n IsClient :: " + isClient;
    }
}

/*
 * RPC - Map logic
 * IsClient
 * IsHost
 * IsLocalPlayer
 * IsOwnedByServer
 * IsOwner - Projectiles, Player
 * IsServer
 */