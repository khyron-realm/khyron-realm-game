using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Net;

namespace DarkRift.Client.Unity
{
    [CustomEditor(typeof(UnityClient))]
    [CanEditMultipleObjects]
    public class UnityClientEditor : Editor
    {
        private SerializedProperty host;
        private SerializedProperty port;
        private SerializedProperty connectOnStart;
        private SerializedProperty eventsFromDispatcher;
        private SerializedProperty sniffData;

        private SerializedProperty objectCacheSettings;

        private void OnEnable()
        {
            host                    = serializedObject.FindProperty("host");
            port                    = serializedObject.FindProperty("port");
            connectOnStart          = serializedObject.FindProperty("connectOnStart");
            eventsFromDispatcher    = serializedObject.FindProperty("eventsFromDispatcher");
            sniffData               = serializedObject.FindProperty("sniffData");

            objectCacheSettings     = serializedObject.FindProperty("objectCacheSettings");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //Display IP address
            EditorGUILayout.PropertyField(host);
            EditorGUILayout.PropertyField(port);
            EditorGUILayout.PropertyField(connectOnStart);

            //Alert to changes when this is unticked!
            bool old = eventsFromDispatcher.boolValue;
            EditorGUILayout.PropertyField(eventsFromDispatcher);

            if (eventsFromDispatcher.boolValue != old && !eventsFromDispatcher.boolValue)
            {
                eventsFromDispatcher.boolValue = !EditorUtility.DisplayDialog(
                    "Danger!",
                    "Unchecking " + eventsFromDispatcher.displayName + " will cause DarkRift to fire events from the .NET thread pool, unless you are confident using multithreading with Unity you should not disable this. Are you 100% sure you want to proceed?",
                    "Yes",
                    "No (Save me!)"
                );
            }

            EditorGUILayout.PropertyField(sniffData);
            
            EditorGUILayout.PropertyField(objectCacheSettings, true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
