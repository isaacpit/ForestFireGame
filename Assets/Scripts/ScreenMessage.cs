using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMessage : MonoBehaviour
{
    public GameObject screenMessageUI;
    public float msgDurationInSeconds = 6.0f;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        coroutine = ShowMessageForSeconds(msgDurationInSeconds);
        StartCoroutine(coroutine);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            screenMessageUI.SetActive(false);
            Time.timeScale = 1;
            gameObject.GetComponent<ScreenMessage>().enabled = false;
        }
    }

    private IEnumerator ShowMessageForSeconds(float msgLength)
    {
        yield return new WaitForSecondsRealtime(msgLength);
        screenMessageUI.SetActive(false);
        Time.timeScale = 1;
        gameObject.GetComponent<ScreenMessage>().enabled = false;
    }
}