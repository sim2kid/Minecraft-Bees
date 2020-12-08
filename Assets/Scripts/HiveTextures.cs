using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveTextures : MonoBehaviour
{
    [SerializeField] private Texture2D[] frontHiveTextures;
    [SerializeField] private Material frontHiveMaterial;
    public int State;


    // Start is called before the first frame update
    void Start()
    {
        EmptyHive();
    }

    private void updateHiveTexture() 
    {
        frontHiveMaterial.mainTexture = frontHiveTextures[State];
        
    }

    public void FillHive() 
    {
        if (State < frontHiveTextures.Length - 1)
        {
            State++;
            updateHiveTexture();
        }
    }

    public void EmptyHive() 
    {
        State = 0;
        updateHiveTexture();
    }
}
