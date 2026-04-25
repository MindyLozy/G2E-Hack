using System;
using UnityEngine;
using MelonLoader;
using Il2Cpp;
using System.Reflection;
using Il2CppInterop.Runtime;

[assembly: MelonInfo(typeof(L0LeRModMenu.Main), "Issue's Hack", "1.0.5", "L0LeR")]
[assembly: MelonGame("Omega Mega Gigal Intel", "Granny 2 Enchanted")]

namespace L0LeRModMenu
{
    public class Main : MelonMod
    {
        private bool menuOpen = false;
        private Rect menuRect = new Rect(20, 20, 400, 500);
        private int currentTab = 0;
        private string[] tabNames = { "Baby_AI", "Granny_AI", "Grandpa_AI" };

        public override void OnUpdate()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Insert))
            {
                menuOpen = !menuOpen;
                Cursor.visible = menuOpen;
                Cursor.lockState = menuOpen ? CursorLockMode.None : CursorLockMode.Locked;
            }
        }

        public override void OnGUI()
        {
            if (!menuOpen) return;

            GUI.color = Color.white;
            GUI.backgroundColor = new Color(0.05f, 0.05f, 0.05f, 0.95f);
            
            menuRect = GUI.Window(999, menuRect, (GUI.WindowFunction)DrawMenu, "Issue's Hack v1.05");
        }
        
        private void DrawMenu(int windowID)
        {
            GUI.DragWindow(new Rect(0, 0, menuRect.width, 25));
            
            currentTab = GUILayout.Toolbar(currentTab, tabNames);
            
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            switch (currentTab)
            {
                case 0: DrawBabyAI(); break;
                case 1: DrawGrannyAI(); break;
                case 2: DrawGrandpaAI(); break;
            }
            GUILayout.EndVertical();
        }

        private void DrawBabyAI()
        {
            GUILayout.Label("Baby AI", GUI.skin.box);
            if (GUILayout.Button("Call Granny")) SetBool("AI_Baby", "CalledGranny", true);
        }

        private void DrawGrannyAI()
        {
            GUILayout.Label("Granny AI", GUI.skin.box);
            if (GUILayout.Button("Kill Granny")) SetBool("AI_Granny", "IsDying", true);
        }

        private void DrawGrandpaAI()
        {
            GUILayout.Label("Grandpa AI", GUI.skin.box);
            if (GUILayout.Button("Kill Grandpa")) SetBool("AI_Grandpa", "IsDying", true);
        }

        private void SetBool(string className, string fieldName, bool value)
        {
            if (className == "AI_Baby")
            {
                var obj = UnityEngine.Object.FindObjectOfType<AI_Baby>();
                if (obj != null) typeof(AI_Baby).GetField(fieldName, BindingFlags.Public | BindingFlags.Instance)?.SetValue(obj, value);
            }
            else if (className == "AI_Granny")
            {
                var obj = UnityEngine.Object.FindObjectOfType<AI_Granny>();
                if (obj != null) typeof(AI_Granny).GetField(fieldName, BindingFlags.Public | BindingFlags.Instance)?.SetValue(obj, value);
            }
            else if (className == "AI_Grandpa")
            {
                var obj = UnityEngine.Object.FindObjectOfType<AI_Grandpa>();
                if (obj != null) typeof(AI_Grandpa).GetField(fieldName, BindingFlags.Public | BindingFlags.Instance)?.SetValue(obj, value);
            }
        }
    }
}
