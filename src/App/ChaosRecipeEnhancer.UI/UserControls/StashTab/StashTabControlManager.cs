﻿using System.Collections.Generic;
using ChaosRecipeEnhancer.UI.Properties;
using ChaosRecipeEnhancer.UI.Windows;

namespace ChaosRecipeEnhancer.UI.UserControls.StashTab;

/// <summary>
///     CRE-specific model of a stash tab within our app (NOT the GGG StashTab object model).
///     Represents various UI elements and additional metadata used in our app that is derived from the original
///     stash tab JSON object requested from GGG's API.
/// </summary>
public static class StashTabControlManager
{
    public static List<StashTabControl> StashTabControls { get; set; } = new();
    public static List<int> StashTabIndices { get; private set; }

    public static void GetStashTabIndices()
    {
        if (!string.IsNullOrWhiteSpace(Settings.Default.StashTabIndices))
        {
            var stashTabIndices = new List<int>();
            var indices = Settings.Default.StashTabIndices;

            foreach (var s in indices.Split(','))
            {
                if (int.TryParse(s.Trim(), out var parsedIndex))
                {
                    if (!stashTabIndices.Contains(parsedIndex)) stashTabIndices.Add(parsedIndex);
                }
                else
                {
                    ErrorWindow.Spawn("The 'Stash Tab Index' setting must be a number.", "Error: Stash Tab Overlay");
                }
            }

            if (stashTabIndices.Count == 0)
            {
                ErrorWindow.Spawn("Stash Tab indices empty", "Error: Stash Tab Overlay");
            }

            StashTabIndices = stashTabIndices;
        }
        else
        {
            ErrorWindow.Spawn("No valid Stash Tab indices could be found in the user settings. The 'Stash Tab Index' setting must be a number.", "Error: Stash Tab Overlay");
        }
    }
}