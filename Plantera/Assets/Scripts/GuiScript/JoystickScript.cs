using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickScript : MonoBehaviour, IPointerDownHandler,IPointerUpHandler, IDragHandler
{
    [SerializeField] private ControllerManager controllerManager;

    Image joystickBg;
    Image joystick;
    Vector2 InputDir { set; get; }
    public float offset;

    private void Start()
    {
        joystickBg = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
        InputDir = Vector2.zero;
    }

    public void Update()
    {
        if (controllerManager.playerManager)
        {
            controllerManager.playerManager.playerController.directionFromJoystick = InputDir;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputDir = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        float bgImageSizeX = joystickBg.rectTransform.sizeDelta.x;
        float bgImageSizeY = joystickBg.rectTransform.sizeDelta.y;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBg.rectTransform,eventData.position,eventData.pressEventCamera,out pos))
        {
            pos.x /= bgImageSizeX;
            pos.y /= bgImageSizeY;
            InputDir = new Vector2(pos.x, pos.y);
            InputDir = InputDir.magnitude > 1 ? InputDir.normalized : InputDir;

            joystick.rectTransform.anchoredPosition = new Vector2(InputDir.x * (bgImageSizeX/offset), InputDir.y * (bgImageSizeY/offset));
        }
    }
}
