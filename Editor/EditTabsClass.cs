﻿using System.Collections.Generic;
using UnityEngine;
using static SAIN.Editor.RectLayout;
using static SAIN.Editor.SAINLayout;

namespace SAIN.Editor
{
    internal class EditTabsClass
    {
        static EditTabsClass()
        {
            TabClasses = new Dictionary<EEditorTab, TabClass>
            {
                {
                    EEditorTab.None, new TabClass
                    {
                        Name = "None",
                        ToolTip = "",
                    }
                },
                {
                    EEditorTab.Home, new TabClass
                    {
                        Name = "Home",
                        ToolTip = "Select Presets and check which mods are detected that can affect SAIN.",
                    }
                },
                {
                    EEditorTab.GlobalSettings, new TabClass
                    {
                        Name = "Global Settings",
                        ToolTip = "Modify settings that are the same between all bots.",
                    }
                },
                {
                    EEditorTab.BotSettings, new TabClass
                    {
                        Name = "Bot Settings",
                        ToolTip = "Modify Settings that are unique to particular bot types for individual difficulties. Difficulty is determined on spawn by EFT, and is changed by selecting the Difficulty value when starting a raid. As Online is a mix of all difficulties.",
                    }
                },
                {
                    EEditorTab.Personalities, new TabClass
                    {
                        Name = "Personalities",
                        ToolTip = "Modify Individual Personality settings for how they are assigned to bots, and what each personality does for a bot's behavior.",
                    }
                },
                {
                    EEditorTab.Advanced, new TabClass
                    {
                        Name = "Advanced Options",
                        ToolTip = "Edit at your own risk. Enable additional advanced config options here",
                    }
                },
            };

            List<string> names = new List<string>();
            List<string> tooltips = new List<string>();
            foreach (var tab in TabClasses)
            {
                if (tab.Key != EEditorTab.None)
                {
                    names.Add(tab.Value.Name);
                    tooltips.Add(tab.Value.ToolTip);
                }
            }
            Tabs = names.ToArray();
            TabTooltips = tooltips.ToArray();
        }

        private const float TabMenuHeight = 50f;
        private const float TabMenuVerticalMargin = 5f;

        private static BuilderClass Builder => SAINPlugin.Editor.Builder;

        public static EEditorTab TabSelectMenu(float minHeight = 15, float speed = 3, float closeSpeedMulti = 0.66f)
        {
            if (TabMenuRect == null || TabRects == null)
            {
                TabMenuRect = new Rect(0, ExitRect.height + TabMenuVerticalMargin, MainWindow.width, TabMenuHeight);
                TabRects = Builder.HorizontalGridRects(TabMenuRect, Tabs.Length, 15f);
            }

            string openTabString = Builder.SelectionGridExpandHeight(TabMenuRect, Tabs, TabClasses[SelectedTab].Name, TabRects, minHeight, speed, closeSpeedMulti, TabTooltips);

            foreach (var tab in TabClasses)
            {
                if (tab.Value.Name == openTabString)
                {
                    SelectedTab = tab.Key;
                }
            }
            return SelectedTab;
        }

        private static Rect[] TabRects;
        public static Rect TabMenuRect;

        public static void BeginScrollView()
        {
            TabClasses[SelectedTab].Scroll = GUILayout.BeginScrollView(TabClasses[SelectedTab].Scroll, GetStyle(Style.scrollView));
            GUILayout.BeginVertical();
        }

        public static void EndScrollView()
        {
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }

        public static bool IsTabSelected(EEditorTab tab)
        {
            return SelectedTab == tab;
        }

        public static EEditorTab SelectedTab = EEditorTab.None;
        public static readonly string[] Tabs;
        public static readonly string[] TabTooltips;
        public static readonly Dictionary<EEditorTab, TabClass> TabClasses;
    }

    public sealed class TabClass
    {
        public string Name;
        public string ToolTip;
        public Vector2 Scroll = Vector2.zero;
    }

    public enum EEditorTab
    {
        None,
        Home,
        GlobalSettings,
        BotSettings,
        Personalities,
        Advanced
    }
}