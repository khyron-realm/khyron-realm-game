using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AuxiliaryClasses;
using Networking.Auctions;
using Networking.Chat;


public class AuctionChatting : MonoBehaviour
{
    [SerializeField] private InputField _textInput;
    [SerializeField] private ObjectPooling _pooling;


    private void Awake()
    {
        _textInput.onSubmit.AddListener((value) => ThisUserMessage(value));
        ChatManager.OnRoomMessage += MessageFromPlayer;
        AuctionsManager.OnPlayerJoined += PlayerJoined;
    }


    private void PlayerJoined(Player player)
    {
        Message(player.Name + " has joined in the auction", "", Color.green);
    }
    private void MessageFromPlayer(ChatMessage message)
    {
        Message(message.Sender, message.Content, Color.white);
    }
    private void ThisUserMessage(string msg)
    {
        ChatManager.SendRoomMessage(msg);
    }


    private void Message(string user, string content, Color color)
    {
        GameObject temp = _pooling.GetPooledObjects();
        temp.SetActive(true);

        Canvas.ForceUpdateCanvases();
        temp.transform.GetChild(1).GetComponent<Text>().color = color;
        temp.transform.GetChild(1).GetComponent<Text>().text = user;
        temp.transform.GetChild(2).GetComponent<Text>().text = content;
        float height = temp.transform.GetChild(2).GetComponent<RectTransform>().rect.height;
        temp.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(560, height + 40);
        Debug.LogWarning(temp.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta);
    }


    private void OnDestroy()
    {
        _textInput.onSubmit.RemoveAllListeners();
        ChatManager.OnRoomMessage -= MessageFromPlayer;
        AuctionsManager.OnPlayerJoined -= PlayerJoined;
    }
}