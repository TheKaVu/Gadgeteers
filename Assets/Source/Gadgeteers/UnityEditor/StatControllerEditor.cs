using UnityEngine;
using Source.Gadgeteers.Game;
using UnityEditor;

/* Unity Editor - GUI drawer */
namespace Source.Gadgeteers.UnityEditor
{
    [CustomEditor(typeof(StatController))]
    public class StatControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var statController = (StatController)target;
            if (GUILayout.Button("Log Stats"))
            {
                statController.LogStats();
            }
            if (GUILayout.Button("Log Mods"))
            {
                statController.LogMods();
            }
        }
    }
}