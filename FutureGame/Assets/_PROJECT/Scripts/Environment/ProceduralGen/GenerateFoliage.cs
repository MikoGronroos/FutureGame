using System;
using UnityEngine;
using UnityEngine.UI;

public class GenerateFoliage : MonoBehaviour
{

    [SerializeField] private Transform terrainObject;
    [SerializeField] private GameObject[] foliageObjects;
    [SerializeField] private Renderer noisePreview;

    [SerializeField] private float intensity;
    [SerializeField] private float amountOfLayers;

    private void Start()
    {
        noisePreview.material.mainTexture = GenerateNoise((int)terrainObject.lossyScale.x, (int)terrainObject.lossyScale.z);
    }

    private Texture2D GenerateNoise(int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y, width, height);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    private Color CalculateColor(int x, int y, int width, int height)
    {
        float xCoord = (float)x / width * intensity;
        float yCoord = (float)y / height * intensity;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }
}
