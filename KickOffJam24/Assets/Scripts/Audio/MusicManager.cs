using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
        {
            instance = this;

            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
    }
}
