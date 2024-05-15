using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    public enum BlockType
    {
        NormalBrick,
        SilverBrick
    }

    [SerializeField]  float pointsBrick = 100;
    [SerializeField]  float pointsSilverBrick = 200;
    [SerializeField]  float currentPoints;
    TextMeshProUGUI text;
    [SerializeField] HealthPlayer hp;

    bool alredyHealthAdded;
    bool alredyHealthAdded1;
    bool alredyHealthAdded2;
    bool alredyHealthAdded3;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public  void AddPointsNormalBrick(BlockType blocktype)
    {
        if (blocktype == BlockType.NormalBrick)
        {
            currentPoints = currentPoints + pointsBrick;
        }
        else if(blocktype == BlockType.SilverBrick)
        {
            currentPoints = currentPoints + pointsSilverBrick;
        }
        text.text = $"Score: {currentPoints} ";
        
    }

    void Update()
    {
        GetHeal();
    }

    void GetHeal()
    {
        if(currentPoints >= 1000 && !alredyHealthAdded)
        {
            alredyHealthAdded = true;
            hp.AddingHealth();
        }
        if (currentPoints >= 10000 && !alredyHealthAdded1)
        {
            alredyHealthAdded1 = true;
            hp.AddingHealth();
        }
        if (currentPoints >= 20000 && !alredyHealthAdded2)
        {
            alredyHealthAdded2 = true;
            hp.AddingHealth();
        }
        if (currentPoints >= 30000 && !alredyHealthAdded3)
        {
            alredyHealthAdded3 = true;
            hp.AddingHealth();
        }
    }

    private void Reset()
    {
        alredyHealthAdded = false;
        alredyHealthAdded1 = false;
        alredyHealthAdded2 = false;
        alredyHealthAdded3 = false;
    }
}
