using UnityEditor;


#if UNITY_EDITOR
[CustomEditor(typeof(Train))]
public class TrainEditor : Editor
{
    private Train _train;

    private void Awake()
    {
        _train = target as Train;
    }
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        _train.lead = EditorGUILayout.ToggleLeft("lead", _train.lead);
        if (_train.lead)
        {
            _train.setting.speed = EditorGUILayout.FloatField("Speed", _train.setting.speed);
            _train.setting.rotateSpeed = EditorGUILayout.FloatField("RptateSpeed", _train.setting.rotateSpeed);
            _train.moveActive = EditorGUILayout.Toggle("MoveActive", _train.moveActive);
        }
        else
        {
            _train.setting.train = (Train)EditorGUILayout.ObjectField("FlontTrain", _train.setting.train, typeof(Train), true);
        }

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_train, "train set");
            EditorUtility.SetDirty(_train);
        }
    }
}
#endif