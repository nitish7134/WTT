using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class LoginScript : MonoBehaviour
{
    private void Awake()
    {
        PlayGamesClientConfiguration playGamesClientConfiguration = new PlayGamesClientConfiguration.Builder().RequestServerAuthCode(false).Build();
        PlayGamesPlatform.InitializeInstance(playGamesClientConfiguration);
        PlayGamesPlatform.Activate();
        SignInWithPlayGames();
    }

    private void SignInWithPlayGames()
    {
        Firebase.Auth.FirebaseAuth firebaseAuth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        UnityEngine.Social.localUser.Authenticate((bool success) =>
        {
            if (!success)
            {
                Debug.LogError("Failed to sign into Play Games Services");
                return;
            }

            string authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
            if (string.IsNullOrEmpty(authCode))
            {
                Debug.LogError("Signed into Play Games Services, but failed to get auth code");
                return;
            }
            Debug.LogError("Auth code is " + authCode);

            Firebase.Auth.Credential credential = Firebase.Auth.PlayGamesAuthProvider.GetCredential(authCode);

            firebaseAuth.SignInWithCredentialAsync(credential).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("Sign in was cancelled");
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("Sign in ecountered an error: " + task.Exception);
                    return;
                }

                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.Log("Signed in successfully");
            });
        });
    }
}
