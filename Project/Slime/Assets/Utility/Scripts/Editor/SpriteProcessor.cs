using UnityEditor;
using UnityEngine;

namespace Utility.Tools
{
    public class SpriteProcessor : AssetPostprocessor
    {
        private void OnPostprocessTexture(Texture2D texture)
        {
            //Convert the asset directory to lowercase for easier reading.
            string lowerCaseAssetPath = assetPath.ToLower();

            //Find the sprites folder. If I can't find it it will return -1.
            bool isInSpritesDirectory = lowerCaseAssetPath.IndexOf("/sprites/") != -1;

            //Follow the sprite in the directory.
            if (isInSpritesDirectory)
            {
                TextureImporter textureImporter = (TextureImporter)assetImporter;

                textureImporter.textureCompression = TextureImporterCompression.Uncompressed;

                textureImporter.filterMode = FilterMode.Point;

                textureImporter.spritePixelsPerUnit = 4;
            }
        }
    }
}