using System;
using System.Collections.Generic;
using Racer.Utilities;
using UnityEngine;

[DefaultExecutionOrder(-10)]
internal class CommentGenerator : SingletonPattern.StaticInstance<CommentGenerator>
{
    private int _startIndex;

    [SerializeField] private TextAsset textFile;

    [SerializeField] private string[] allTexts;

    [field: Space(10)]

    [field: SerializeField] public List<string> ZeroStarTexts { get; private set; }
    [field: SerializeField] public List<string> OneStarTexts { get; private set; }
    [field: SerializeField] public List<string> TwoStarTexts { get; private set; }
    [field: SerializeField] public List<string> ThreeStarTexts { get; private set; }


    [ContextMenu(nameof(Initialize))]
    private void Initialize()
    {
        allTexts = null;

        if (textFile)
            allTexts = textFile.text
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        else
            allTexts = null;
    }

    [ContextMenu(nameof(PopulateZeroStarTexts))]
    private void PopulateZeroStarTexts()
    {
        PopulateTexts(ThreeStarTexts, "#0");
    }

    [ContextMenu(nameof(PopulateOneStarTexts))]
    private void PopulateOneStarTexts()
    {
        PopulateTexts(ThreeStarTexts, "#1");
    }

    [ContextMenu(nameof(PopulateTwoTexts))]
    private void PopulateTwoTexts()
    {
        PopulateTexts(ThreeStarTexts, "#2");
    }

    [ContextMenu(nameof(PopulateThreeStarTexts))]
    private void PopulateThreeStarTexts()
    {
        PopulateTexts(ThreeStarTexts, "#3");
    }

    private void PopulateTexts(ICollection<string> texts, string index)
    {
        texts.Clear();

        _startIndex = 0;
        _startIndex = GetIndexOf(index);

        for (var i = _startIndex; i < allTexts.Length; i++)
        {
            var t = allTexts[i];

            if (t.StartsWith("#"))
                continue;

            if (string.IsNullOrEmpty(t))
                break;

            texts.Add(t);
        }
    }

    private int GetIndexOf(string value)
    {
        var index = 0;

        foreach (var t in allTexts)
        {
            index++;

            if (string.IsNullOrEmpty(t))
                continue;

            if (t.Contains(value))
                break;
        }

        return index;
    }
}
