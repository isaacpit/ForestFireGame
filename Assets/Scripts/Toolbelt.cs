using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toolbelt : MonoBehaviour
{
    private Image[] tools;
    public Image currentTool;
    public int toolBeltIndex;
    public float scrollScale = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        tools = GetComponentsInChildren<Image>();
        toolBeltIndex = 1;
        updateBelt();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1)
        {
            float scrollAmount = Input.mouseScrollDelta.y * scrollScale;
            if (Mathf.Abs(scrollAmount) > 1.0f)
            {
                int sign = (scrollAmount > 0) ? -1 : 1;
                toolBeltIndex += sign;
                if (toolBeltIndex <= 0)
                {
                    toolBeltIndex = 3;
                }
                else if (toolBeltIndex >= 4)
                {
                    toolBeltIndex = 1;
                }
                updateBelt();
            }
            else if (Input.GetButtonDown("Toolbelt 1"))
            {
                toolBeltIndex = 1;
                updateBelt();
            }
            else if (Input.GetButtonDown("Toolbelt 2"))
            {
                toolBeltIndex = 2;
                updateBelt();
            }
            else if (Input.GetButtonDown("Toolbelt 3"))
            {
                toolBeltIndex = 3;
                updateBelt();
            }
        }
    }

    public void updateBelt()
    {
        if (currentTool == tools[toolBeltIndex - 1])
        {
            return;
        } else if (currentTool != null)
        {
            currentTool.GetComponent<RectTransform>().anchoredPosition += new Vector2(0.0f, -30.0f);
        }
        
        currentTool = tools[toolBeltIndex - 1];
        currentTool.GetComponent<RectTransform>().anchoredPosition += new Vector2(0.0f, 30.0f);
    }
}
