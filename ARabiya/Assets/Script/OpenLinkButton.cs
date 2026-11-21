using UnityEngine;

public class OpenLinkButton : MonoBehaviour
{
    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }
}
