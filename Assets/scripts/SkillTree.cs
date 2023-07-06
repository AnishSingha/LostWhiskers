using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public int skillPoint = 100;
    public Button button;

    public void skill1()
    {
        button.interactable = false;
        skillPoint = skillPoint - 50;
        

    }
    private void Awake()
    {
        if (skillPoint >= 50)
        {
            button.interactable = true;
        }
    }

}
