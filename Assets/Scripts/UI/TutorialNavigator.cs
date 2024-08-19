using System.Collections.Generic;
using UnityEngine;

public class TutorialNavigator : MonoBehaviour
{
    public List<GameObject> panelList;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        foreach (GameObject go in panelList)
        {
            go.SetActive(false);
        }
        panelList[index].SetActive(true);
    }

    public void HandleNextButton()
    {
        panelList[index].SetActive(false);
        index++;
        if (index == panelList.Count)
            index = 0;
        panelList[index].SetActive(true);
    }
    public void HandlePreviousButton()
    {
        panelList[index].SetActive(false);
        index--;
        if (index == -1)
            index = panelList.Count - 1;
        panelList[index].SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
