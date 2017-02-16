using UnityEngine;
using UnityEngine.UI;

public class User : MonoBehaviour {
    public Button show;

	void Start () {
        show.onClick.AddListener(delegate{
           ToastMessageScript.Controller().showToastOnUiThread("Successfully showed.");
        });
	}
}
