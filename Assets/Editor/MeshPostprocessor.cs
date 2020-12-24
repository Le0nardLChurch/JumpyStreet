using UnityEngine;
using UnityEditor;


class MeshPostprocessor : AssetPostprocessor
{
    [SerializeField] float scaleFactor = 12.5f;


    void OnPreprocessModel()
    {
        (assetImporter as ModelImporter).globalScale = scaleFactor;
    }
}





