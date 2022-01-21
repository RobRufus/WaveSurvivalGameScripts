using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class raycastUiInteractScript : MonoBehaviour
{


    public GraphicRaycaster graphicRaycaster;


    private void Awake()
    {
        graphicRaycaster = GameObject.Find("CampUI").GetComponent<GraphicRaycaster>();
    }


    void Update()
    {
        GraphicsRaycasts();
    }




    void GraphicsRaycasts()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);
        if (results.Count > 0)
        {
            for (int i = 0; i < results.Count; i++)
            {
                Button button = results[i].gameObject.GetComponent<Button>();
                if (button != null)
                {
                    if (Input.GetKeyDown(KeyCode.Q)) button.onClick.Invoke();
                }
            }
        }
    }




}
public interface InteractOnRaycast
{
    void OnInteract();
}
