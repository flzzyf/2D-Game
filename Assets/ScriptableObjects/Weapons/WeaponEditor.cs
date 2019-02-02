using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Weapon main = (Weapon)target;

        //显示图标
        if (main.gunSprite != null)
        {
            Texture texture = main.gunSprite.texture;
            float ratio = texture.width / texture.height;
            int height = 100;
            GUILayout.Box(texture, EditorStyles.objectFieldThumb,
            GUILayout.Width(height * ratio), GUILayout.Height(height));

            Texture cube = Resources.Load<Texture2D>("Textures/Cube");
            Rect last = GUILayoutUtility.GetLastRect();
            last.x += main.firePoint.x;
            last.y += main.firePoint.y;
            last.width = 10;
            last.height = 10;
            GUI.DrawTexture(last, cube, ScaleMode.ScaleToFit);
        }

        base.OnInspectorGUI();

        serializedObject.ApplyModifiedProperties();
    }
}
