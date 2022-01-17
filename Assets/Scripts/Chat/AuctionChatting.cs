using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AuxiliaryClasses;
using Networking.Auctions;
using Networking.Chat;
using DG.Tweening;


public class AuctionChatting : MonoBehaviour
{
    #region "Input field"
    [SerializeField] private InputField _textInput;
    [SerializeField] private ObjectPooling _pooling;

    [SerializeField] private ScrollRect _scrollRectPosition;

    [SerializeField] private Sprite _enterIcon;
    [SerializeField] private Sprite _exitIcon;
    [SerializeField] private Sprite _bidIcon;
    [SerializeField] private Sprite _playerIcon;
    #endregion

    private void Awake()
    {
        _textInput.characterLimit = 150;
        _textInput.onSubmit.AddListener((value) => ThisUserMessage(value));

        ChatManager.OnRoomMessage += MessageFromPlayer;
        AuctionsManager.OnPlayerJoined += PlayerJoined;
        AuctionsManager.OnPlayerLeft += PlayerLeft;
        AuctionsManager.OnAddBid += AddedBid;
        AuctionsManager.OnOverbid += AddedBid;
        AuctionsManager.OnSuccessfulAddBid += AddedBid;
    }


    private void AddedBid(Bid bid)
    {
        Message(bid.PlayerName + " has bidded " + bid.Amount + " energy", "", Color.yellow, false, _bidIcon);
    }
    private void PlayerLeft(string player)
    {
        Message(player + " has left the auction", "", Color.red, false, _exitIcon);
    }
    private void PlayerJoined(Player player)
    {
        Message(player.Name + " has joined in the auction", "", Color.green, false, _enterIcon);
    }
    private void MessageFromPlayer(ChatMessage message)
    {
        Message(message.Sender, message.Content, Color.white, true, _playerIcon);
    }
    private void ThisUserMessage(string msg)
    {
        ChatManager.SendRoomMessage(msg);
    }


    private void Message(string user, string content, Color color, bool open, Sprite image)
    {
        GameObject temp = _pooling.GetPooledObjects();
        temp.SetActive(true);

        temp.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = image;
        temp.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().color = color;
        temp.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = user;



        if(open)
        {
            temp.transform.GetChild(1).gameObject.SetActive(true);
            temp.transform.GetChild(1).GetComponent<Text>().text = content;
        }
        else
        {
            temp.transform.GetChild(1).gameObject.SetActive(false);
        }

        Canvas.ForceUpdateCanvases();
        _scrollRectPosition.verticalNormalizedPosition = 0;      
    }


    private void OnDestroy()
    {
        _textInput.onSubmit.RemoveAllListeners();
        ChatManager.OnRoomMessage -= MessageFromPlayer;
        AuctionsManager.OnPlayerJoined -= PlayerJoined;
        AuctionsManager.OnPlayerLeft -= PlayerLeft;
        AuctionsManager.OnAddBid -= AddedBid;
        AuctionsManager.OnOverbid -= AddedBid;
        AuctionsManager.OnSuccessfulAddBid -= AddedBid;
    }
}