using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{
  #region Setup

  ChatClient chatClient;
  [SerializeField] GameObject joinChatButton;
  [SerializeField] string username;
  private bool _isConnected;

  public void ChatConnectOnClick()
  {
    _isConnected = true;
    chatClient = new ChatClient(this);
    chatClient
      .Connect(
        PhotonNetwork
          .PhotonServerSettings
          .AppSettings
          .AppIdChat,
        PhotonNetwork
          .AppVersion,
        new AuthenticationValues(username)
      );
    Debug.Log("Connecting ...");
  }

  #endregion Setup

  #region General

  [SerializeField] GameObject chatPanel;
  string privateReceiver = "";
  string currentChat;
  [SerializeField] TMP_InputField chatField;
  [SerializeField] TMP_Text chatDisplay;

  #endregion General

  // Start is called before the first frame update
  void Start()
  {

  }


  // Update is called once per frame
  void Update()
  {
    if (_isConnected) chatClient.Service();

    if (chatField.text != "" && Input.GetKey(KeyCode.Return))
    {
      SubmitPublicChatOnClick();
      // SubmitPrivateChatOnClick();
    }
  }

  public void DebugReturn(DebugLevel level, string message)
  {
    // throw new System.NotImplementedException();
    Debug.Log("------- level & message -------");
    Debug.Log(level);
    Debug.Log(message);
    Debug.Log("------------- END -------------");
  }

  public void OnChatStateChange(ChatState state)
  {
    Debug.Log(state);
  }

  
  // This fn called when chatClient.Connect success
  public void OnConnected()
  {
    Debug.Log("Connected");
    // _isConnected = true;
    joinChatButton.SetActive(false);
    chatClient.Subscribe(new string[] { "RegionChannel" });
    // SubToChatOnClick();
  }

  public void OnDisconnected()
  {
    throw new System.NotImplementedException();
  }

  public void OnGetMessages(string channelName, string[] senders, object[] messages)
  {
    string msgs = "";
    for (int i = 0; i < senders.Length; i++)
    {
      msgs = string.Format("{0}: {1}", senders[i], messages[i]);

      chatDisplay.text += "\n " + msgs;

      Debug.Log(msgs);
    }
  }

  public void OnPrivateMessage(string sender, object message, string channelName)
  {
    throw new System.NotImplementedException();
  }

  public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
  {
    throw new System.NotImplementedException();
  }

  public void OnSubscribed(string[] channels, bool[] results)
  {
    chatPanel.SetActive(true);
  }

  public void OnUnsubscribed(string[] channels)
  {
    throw new System.NotImplementedException();
  }

  public void OnUserSubscribed(string channel, string user)
  {
    throw new System.NotImplementedException();
  }

  public void OnUserUnsubscribed(string channel, string user)
  {
    throw new System.NotImplementedException();
  }

  public void UserNameOnValueChange(string valueIn)
  {
    username = valueIn;
  }


  #region PublicChat

  public void SubmitPublicChatOnClick()
  {
    if (privateReceiver == "")
    {
      chatClient.PublishMessage("RegionChannel", currentChat);
      chatField.text = "";
      currentChat = "";
    }
  }

  public void TypeChatOnValueChange(string valueIn)
  {
    currentChat = valueIn;
  }

  #endregion PublicChat

  public void SubmitPrivateChatOnClick()
  {
    throw new NotImplementedException();
  }

}
