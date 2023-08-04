using UnityEditor;
using UnityEngine;

public class CreateEnemyList
{
    public static EnemyList Create()
    {
        EnemyList asset = ScriptableObject.CreateInstance<EnemyList>();
        AssetDatabase.CreateAsset(asset, "Assets/EnemyList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}