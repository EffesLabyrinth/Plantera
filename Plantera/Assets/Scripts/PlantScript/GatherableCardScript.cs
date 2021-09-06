using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatherableCardScript : MonoBehaviour
{
    [SerializeField] private GatherableObject gatherable;
    [SerializeField] private Text gatherableName;
    [SerializeField] private Image realSprite;
    [SerializeField] private Text description;

    public void InitializeCard(GatherableObject gatherableObject)
    {
        Time.timeScale = 0f;
        gatherable = gatherableObject;
        gatherableName.text = gatherable.gatherableName;
        realSprite.sprite = gatherable.realSprite;
        description.text = gatherable.description;
    }
    public void DestroySelf()
    {
        Time.fixedDeltaTime = 0.02f;
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
