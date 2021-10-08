using Networking.Login;
using Networking.Game;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    public void onClickLogin()
    {
        LoginManager.Login("gigel", "12345");
        LoginManager.AddUser("serban", "parola");
        //UnlimitedPlayerManager.GetPlayerDataRequest();
        //UnlimitedPlayerManager.SendConvertRequest();
    }
}