using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Silly
{
    [CustomEditor(typeof(MapRandom))]
    public class MapGenEditorRandom : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            MapRandom myGenerator = (MapRandom)target;
            if(GUILayout.Button("���� �����մϴ�"))
            {
                myGenerator.BuildGenerator();

            }
        }
    }
}
