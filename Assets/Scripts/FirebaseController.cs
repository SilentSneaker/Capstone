using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System;
using Firebase.Database;
using System.Threading.Tasks;
using System.Diagnostics;
using Firebase.Extensions;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using Google;
using System.Net.Http;

public class FirebaseController : MonoBehaviour
{

    public GameObject loginScreen, RegisterScreen, AccountScreen, ForgotPasswordScreen, NotificationScreen;
    public InputField loginEmail, loginPassword, registerEmail, registerPassword, registerConfirmPassword, accountWeight, accountHeight, forgotPasswordEmail, RegisterUserName;
    public UnityEngine.UI.Text notificationHeader, notificationMessage, LoginEmail, AccountUserName;


    public DatabaseReference DBReference;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    public string GoogleWebAPI = "408757591197-v8a2nlqln8kgkvbtg2qtqg9rg4ombi7k.apps.googleusercontent.com";
    public GoogleSignInConfiguration configuration;

    bool isLoggedIn = false;
    bool isLog = false;

    void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = GoogleWebAPI,
            RequestIdToken = true
        };
    }
    void Start()
    {
        OpenLoginScreen();
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                InitializeFirebase();

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
    public void GoogleSignInClick()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticatedFinished);
    }

    void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
            UnityEngine.Debug.LogError("Fault");
        else if (task.IsCanceled)
            UnityEngine.Debug.LogError("Login Cancelled");
        else
        {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);
            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    UnityEngine.Debug.LogError("Sign In With Credential Async was canceled");
                    showNotification("Error","Sign In With Credential Async was canceled");
                    return;
                }
                if (task.IsFaulted)
                {
                    UnityEngine.Debug.LogError("Sign in with credential async encountered an error" + task.Exception);
                    showNotification("Error","Sign in with credential async encountered an error" + task.Exception);
                    return;
                }
                user = auth.CurrentUser;

                AccountUserName.text = user.DisplayName;
                LoginEmail.text = user.Email;

                loginScreen.SetActive(false);
                AccountScreen.SetActive(true);
            });
        }
    }
    public void OpenLoginScreen()
    {
        loginScreen.SetActive(true);
        RegisterScreen.SetActive(false);
        AccountScreen.SetActive(false);
        ForgotPasswordScreen.SetActive(false);
    }
    public void OpenRegisterScreen()
    {
        loginScreen.SetActive(false);
        RegisterScreen.SetActive(true);
        AccountScreen.SetActive(false);
        ForgotPasswordScreen.SetActive(false);
    }
    public void OpenAccountScreen()
    {
        loginScreen.SetActive(false);
        RegisterScreen.SetActive(false);
        AccountScreen.SetActive(true);
        ForgotPasswordScreen.SetActive(false);
    }
    public void OpenForgotScreen()
    {
        loginScreen.SetActive(false);
        RegisterScreen.SetActive(false);
        AccountScreen.SetActive(false);
        ForgotPasswordScreen.SetActive(true);
    }
    public void userLogin()
    {
        if (string.IsNullOrEmpty(loginEmail.text) || string.IsNullOrEmpty(loginPassword.text))
        {
            showNotification("Error", "Fields are missing");
            return;
        }

        signIn(loginEmail.text, loginPassword.text);
        LoginEmail.text = "";
        loginPassword.text = "";
    }
    public void userRegister()
    {
        if (string.IsNullOrEmpty(registerEmail.text) || string.IsNullOrEmpty(registerPassword.text) || string.IsNullOrEmpty(registerConfirmPassword.text))
        {
            showNotification("Error", "Fields are missing");
            return;
        }

        createUser(registerEmail.text,registerPassword.text, RegisterUserName.text);
    }
    public void Forgotpassword()
    {
        if (string.IsNullOrEmpty(forgotPasswordEmail.text))
        {
            showNotification("Error", "Email Section is empty.");
            return;
        }
        forgotPasswordCheck(forgotPasswordEmail.text);
    }

    public void showNotification(string title, string message)
    {
        notificationMessage.text = message;
        notificationHeader.text = title;
        NotificationScreen.SetActive(true);
    }

    public void closeNotification()
    {
        notificationMessage.text = "";
        notificationHeader.text = "";
        NotificationScreen.SetActive(false);
    }
    public void Save()
    {
        StartCoroutine(UpdateUserName(AccountUserName.text));
        StartCoroutine(UpdateWeight(accountWeight.text));
        StartCoroutine(UpdateHeight(accountHeight.text));
        UnityEngine.Debug.Log("Information Saved");
    }
    
    public void Logout()
    {
        


        auth.SignOut();
        OpenLoginScreen();
    }

    public void createUser(string email, string password, string userName)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                UnityEngine.Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                {
                    UnityEngine.Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    Firebase.FirebaseException firebaseException = exception as Firebase.FirebaseException;
                    if (firebaseException != null)
                    {
                        var errorCode = (AuthError)firebaseException.ErrorCode;
                        showNotification("Error", GetErrorMessage(errorCode));
                    }
                    return;
                }
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            UnityEngine.Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            updateUserProfile(userName);
        });
    }
    public void signIn(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                UnityEngine.Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                foreach (Exception exception in task.Exception.Flatten().InnerExceptions) {

                    UnityEngine.Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    Firebase.FirebaseException firebaseException = exception as Firebase.FirebaseException;
                    if (firebaseException != null)
                    {
                        var errorCode = (AuthError)firebaseException.ErrorCode;
                        showNotification("Error", GetErrorMessage(errorCode));
                    }
                    return;
                }
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            UnityEngine.Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            LoginEmail.text = newUser.Email;
            AccountUserName.text = ""+newUser.DisplayName;

            OpenAccountScreen();
        });
    }
    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        DBReference = FirebaseDatabase.DefaultInstance.RootReference;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
        
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                UnityEngine.Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                UnityEngine.Debug.Log("Signed in " + user.UserId);
                isLoggedIn= true;
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }
    public void updateUserProfile(string userName)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = userName,
                PhotoUrl = new System.Uri("https://th.bing.com/th/id/R.9fe1988f83eeae24d16db131475d31b2?rik=Eq0UEK5i3b%2bfgg&riu=http%3a%2f%2fcohenwoodworking.com%2fwp-content%2fuploads%2f2016%2f09%2fimage-placeholder-500x500.jpg&ehk=6xxwN2hsF1pbhTTWWflHnkIka8Rxe3PZahhFfRQJIrY%3d&risl=&pid=ImgRaw&r=0"),
            };
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    UnityEngine.Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    UnityEngine.Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                UnityEngine.Debug.Log("User profile updated successfully.");

                showNotification("Alert", "Successfully created");
            });
        }
    }
    
    void Update()
    {
        if(isLoggedIn)
        {
            if(!isLog)
            {
                isLog = true;
                LoginEmail.text = user.Email;
                AccountUserName.text = "" + user.DisplayName;
                OpenAccountScreen();
            }
        }
    }

    private static string GetErrorMessage(AuthError errorCode)
    {
        var message = "";
        switch (errorCode)
        {
            case AuthError.AccountExistsWithDifferentCredentials:
                message = "Account Does NOT exist";
                break;
            case AuthError.MissingPassword:
                message = "Password is missing";
                break;
            case AuthError.WeakPassword:
                message = "Password is too weak please make it stronger";
                break;
            case AuthError.WrongPassword:
                message = "Wrong Password";
                break;
            case AuthError.EmailAlreadyInUse:
                message = "Email is already being used";
                break;
            case AuthError.InvalidEmail:
                message = "Incorrect Email";
                break;
            case AuthError.MissingEmail:
                message = "Email is missing";
                break;
            default:
                message = "Account not found";
                break;
        }
        return message;
    }

    void forgotPasswordCheck(string Email)
    {
        auth.SendPasswordResetEmailAsync(Email).ContinueWithOnMainThread(task=>{

            if (task.IsCanceled)
            {
                UnityEngine.Debug.LogError("Send Password Reset Email was canceled.");
            }
            if (task.IsFaulted)
            {
                foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                {

                    UnityEngine.Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    Firebase.FirebaseException firebaseException = exception as Firebase.FirebaseException;
                    if (firebaseException != null)
                    {
                        var errorCode = (AuthError)firebaseException.ErrorCode;
                        showNotification("Error", GetErrorMessage(errorCode));
                    }
                    return;
                }
            }
            showNotification("Alert", "Password Recovery sent to email");
        });
    }
    private IEnumerator UpdateWeight(string weight)
    {
        var DBTask = DBReference.Child("users").Child(user.UserId).Child("weight").SetValueAsync(weight);
        yield return new WaitUntil(predicate: () => DBTask.IsCompletedSuccessfully);
        if(DBTask.Exception != null)
        {
            UnityEngine.Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }
    private IEnumerator UpdateHeight(string height)
    {
        var DBTask = DBReference.Child("users").Child(user.UserId).Child("height").SetValueAsync(height);
        yield return new WaitUntil(predicate: () => DBTask.IsCompletedSuccessfully);
        if (DBTask.Exception != null)
        {
            UnityEngine.Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }
    private IEnumerator UpdateUserName(string userName)
    {
        var DBTask = DBReference.Child("users").Child(user.UserId).Child("username").SetValueAsync(userName);
        yield return new WaitUntil(predicate:()=> DBTask.IsCompletedSuccessfully);
        if (DBTask.Exception != null)
        {
            UnityEngine.Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }
    private IEnumerator LoadUserData()
    {
        var DBTask = DBReference.Child("users").Child(user.UserId).GetValueAsync();
        yield return new WaitUntil(predicate:() =>DBTask.IsCompletedSuccessfully);
        if(DBTask.Exception != null)
        {
            UnityEngine.Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            accountHeight.text= "0";
            accountWeight.text= "0";
        }
        else
        {
            DataSnapshot snapShot = DBTask.Result;
            accountWeight.text= snapShot.Child("weight").Value.ToString();
            accountHeight.text = snapShot.Child("height").Value.ToString();
        }
    }
}
