using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject mapUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (GameManager.Instance != null && GameManager.Instance.gameActive)
                mapUI.SetActive(!mapUI.activeSelf);
        }
    }
}
