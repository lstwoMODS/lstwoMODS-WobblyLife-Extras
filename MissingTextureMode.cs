using System;
using lstwoMODS_Core;
using lstwoMODS_Core.Hacks;
using lstwoMODS_Core.UI.TabMenus;
using UnityEngine;
using System.IO;
using System.Linq;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace lstwoMODS_WobblyLife_Extras;

public class MissingTextureMode : BaseHack
{
    private const string CustomTextureFolder = "lstwoMODS/extras/custom_texture";

    public override void ConstructUI(GameObject root)
    {
        if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CustomTextureFolder)))
        {
            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CustomTextureFolder));
        }
        
        missingTexture = Plugin.AssetBundle.LoadAsset<Texture2D>("MissingTexture");
        missingTextureMaterial = new Material(Shader.Find("Standard"));
        missingTextureMaterial.SetTexture(MainTex, missingTexture);
        missingTextureMaterial.SetFloat(Metallic, 0f);
        missingTextureMaterial.SetFloat(Smoothness, 0f);

        var ui = new HacksUIHelper(root);

        ui.AddSpacer(6);

        ui.CreateLBDuo("Activate Missing Texture Mode (cannot be undone)", "ActivateLB", () =>
        {
            ApplyTextureToAllRenderers(missingTextureMaterial);
        }, "Activate", "ActivateButton");

        ui.AddSpacer(12);

        ui.CreateLabel($"Put custom textures in <Wobbly Life Folder>/{CustomTextureFolder}/ (png, jpg, jpeg only)", fontSize: 14);
        
        ui.AddSpacer(6);

        ui.CreateLBDuo("Load Custom Texture", "LoadCustomTextureLB", () =>
        {
            var folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CustomTextureFolder);
            var randomImagePath = GetRandomImageFile(folderPath);
            
            if (string.IsNullOrEmpty(randomImagePath))
            {
                Debug.LogError("No image files found in folder: " + folderPath);
                return;
            }

            var customTexture = LoadTextureFromFile(randomImagePath);
            
            if (customTexture != null)
            {
                customTextureMaterial = new Material(Shader.Find("Standard"));
                customTextureMaterial.SetTexture(MainTex, customTexture);
                customTextureMaterial.SetFloat(Metallic, 0f);
                customTextureMaterial.SetFloat(Smoothness, 0f);

                ApplyTextureToAllRenderers(customTextureMaterial);
            }
            else
            {
                Debug.LogError("Failed to load custom texture.");
            }
        }, "Apply", "LoadCustomTextureButton");

        ui.AddSpacer(6);
    }

    public override void Update() { }
    public override void RefreshUI() { }

    public override string Name => "Missing Texture Mode";
    public override string Description => "Replaces all materials with a missing texture or random image.";
    public override HacksTab HacksTab => lstwoMODS_WobblyLife.Plugin.ExtraHacksTab;

    private static Material missingTextureMaterial;
    private static Material customTextureMaterial;
    private static Texture2D missingTexture;

    private static readonly int Smoothness = Shader.PropertyToID("_Smoothness");
    private static readonly int Metallic = Shader.PropertyToID("_Metallic");
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    private void ApplyTextureToAllRenderers(Material material)
    {
        foreach (var renderer in Object.FindObjectsOfType<Renderer>())
        {
            renderer.sharedMaterial = material;
            renderer.sharedMaterials = [material];

            renderer.material = material;
            renderer.materials = [material];
        }
    }

    private string GetRandomImageFile(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            return null;
        }

        var imageFiles = Directory.GetFiles(folderPath)
            .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || 
                        f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || 
                        f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)).ToArray();

        if (imageFiles.Length == 0)
        {
            return null;
        }

        var randomIndex = Random.Range(0, imageFiles.Length);
        return imageFiles[randomIndex];
    }

    private static Texture2D LoadTextureFromFile(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("File not found: " + path);
            return null;
        }

        var fileData = File.ReadAllBytes(path);
        var texture = new Texture2D(2, 2);
        
        if (texture.LoadImage(fileData))
        {
            return texture;
        }

        Debug.LogError("Failed to load image from file.");
        return null;
    }
}
