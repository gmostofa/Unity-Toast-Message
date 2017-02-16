using UnityEngine;

public class ToastMessageScript : MonoBehaviour
{
    string toastString;
    AndroidJavaObject currentActivity;
    AndroidJavaClass UnityPlayer;
    AndroidJavaObject context;

    static ToastMessageScript instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public static ToastMessageScript Controller()
    {
            return instance;
    }

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        }
    }
   
    public void showToastOnUiThread(string toastString)
    {
        this.toastString = toastString;
        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(ShowToast));
    }

    void ShowToast()
    {
        Debug.Log(this + ": Running on UI thread");

        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", toastString);
        AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
        toast.Call("show");
    }
}