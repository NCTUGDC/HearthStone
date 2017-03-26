using UnityEngine;

public class EternalObject : MonoBehaviour
{
	void Start ()
    {
        DontDestroyOnLoad(this);
	}
}
