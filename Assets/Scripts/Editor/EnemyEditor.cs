using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class EnemyEditor: EditorWindow
{
    private EnemyList enemyInfoList;
    private int viewIndex;

    [MenuItem("Window/Enemy Editor")]
    public static void Init()
    {
        GetWindow(typeof(EnemyEditor));
    }

    private void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = "Assets/EnemyList.asset";
            enemyInfoList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(EnemyList)) as EnemyList;
        }

        if (enemyInfoList == null)
        {
            viewIndex = 1;

            EnemyList asset = ScriptableObject.CreateInstance<EnemyList>();
            AssetDatabase.CreateAsset(asset, "Assets/EnemyList.asset");
            AssetDatabase.SaveAssets();

            enemyInfoList = asset;

            if (enemyInfoList)
            {
                enemyInfoList.enemyList = new List<SimpleEnemyInfo>();
                string relPath = AssetDatabase.GetAssetPath(enemyInfoList);
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Enemy Editor", EditorStyles.boldLabel);
        GUILayout.Space(10);

        if (enemyInfoList != null)
        {
            PrintTopMenu();
        }
        else
        {
            GUILayout.Space(10);
            GUILayout.Label("Can't load weapon list.");
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(enemyInfoList);
        }
    }

    private void PrintTopMenu()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(10);
        if (GUILayout.Button("<- Prev", GUILayout.ExpandWidth(false)))
        {
            if (viewIndex > 1)
                viewIndex--;
        }
        GUILayout.Space(5);
        if (GUILayout.Button("Next ->", GUILayout.ExpandWidth(false)))
        {
            if (viewIndex < enemyInfoList.enemyList.Count)
            {
                viewIndex++;
            }
        }
        GUILayout.Space(60);
        if (GUILayout.Button("+ Add Enemy", GUILayout.ExpandWidth(false)))
        {
            AddItem();
        }
        GUILayout.Space(5);
        if (GUILayout.Button("- Delete Enemy", GUILayout.ExpandWidth(false)))
        {
            DeleteItem(viewIndex - 1);
        }
        GUILayout.EndHorizontal();

        if (enemyInfoList.enemyList.Count > 0)
        {
            EnemyListMenu();
        }
        else
        {
            GUILayout.Space(10);
            GUILayout.Label("This Enemy List is empty.");
        }
    }

    void AddItem()
    {
        SimpleEnemyInfo newEnemy = new SimpleEnemyInfo();
        newEnemy.name = "New Enemy";
        enemyInfoList.enemyList.Add(newEnemy);
        viewIndex = enemyInfoList.enemyList.Count;
    }

    void DeleteItem(int index)
    {
        enemyInfoList.enemyList.RemoveAt(index);
    }

    void EnemyListMenu()
    {
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Enemy", viewIndex, GUILayout.ExpandWidth(false)), 1,
            enemyInfoList.enemyList.Count);
        EditorGUILayout.LabelField("of   " + enemyInfoList.enemyList.Count.ToString() + "   enemy", "", GUILayout.ExpandWidth(false) );
        GUILayout.EndHorizontal();
        string[] _choices = new String[enemyInfoList.enemyList.Count];
        for (int i = 0; i < enemyInfoList.enemyList.Count; i++)
        {
            _choices[i] = enemyInfoList.enemyList[i].name;
        }

        int _choiceIndex = viewIndex - 1;
        viewIndex = EditorGUILayout.Popup(_choiceIndex, _choices) + 1;
        
        GUILayout.Space(10);
        enemyInfoList.enemyList[viewIndex - 1].name =
            EditorGUILayout.TextField("Name", enemyInfoList.enemyList[viewIndex - 1].name as string);
        enemyInfoList.enemyList[viewIndex - 1].damage =
            EditorGUILayout.FloatField("Damage", enemyInfoList.enemyList[viewIndex - 1].damage);
        enemyInfoList.enemyList[viewIndex - 1].strength =
            EditorGUILayout.FloatField("Strength", enemyInfoList.enemyList[viewIndex - 1].strength);
    }
}