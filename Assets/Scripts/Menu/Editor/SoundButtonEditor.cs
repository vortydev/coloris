using UnityEditor;
using UnityEditor.UI;

namespace Menu.Editor
{
    [CustomEditor(typeof(SoundButton))]
    public class SoundButtonEditor : ButtonEditor
    {
        private SerializedProperty _clip;
        
        public override void OnInspectorGUI()
        {
            if (_clip == null)
                _clip = serializedObject.FindProperty("sound");

            EditorGUILayout.PropertyField(_clip);
            
            base.OnInspectorGUI();
        }
    }
}
