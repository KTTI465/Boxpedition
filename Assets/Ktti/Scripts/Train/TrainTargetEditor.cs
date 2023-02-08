using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


#if UNITY_EDITOR
[CustomEditor(typeof(TrainTarget))]
public class TrainTargetEditor : Editor
{
    private TrainTarget _target;

    private void Awake()
    {
        _target = target as TrainTarget;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        _target.enableBranch = EditorGUILayout.ToggleLeft("EnableBranch", _target.enableBranch);
        if (_target.enableBranch)
        {
            _target.setting.leftTarget = (TrainTarget)EditorGUILayout.ObjectField("LeftTarget", _target.setting.leftTarget, typeof(TrainTarget), true);
            _target.setting.rightTarget = (TrainTarget)EditorGUILayout.ObjectField("RightTarget", _target.setting.rightTarget, typeof(TrainTarget), true);
            _target.setting.right = EditorGUILayout.Toggle("right", _target.setting.right);
        }
        else
        {
            _target.setting.nextTarget = (TrainTarget)EditorGUILayout.ObjectField("NextTarget", _target.setting.nextTarget, typeof(TrainTarget), true);
        }

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_target, "target set");
            EditorUtility.SetDirty(_target);
        }
    }
}
#endif
