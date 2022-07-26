using System;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.Random;

class CommentGenerator : MonoBehaviour
{
    [SerializeField]
    TextAsset comments;

    [SerializeField]
    string[] allComments;

    [SerializeField]
    List<string> zeroStarComments;

    [SerializeField]
    List<string> oneStarComments;

    [SerializeField]
    List<string> twoStarComments;

    [SerializeField]
    List<string> threeStarComments;


    [ContextMenu("Initialize")]
    void InitializeArray()
    {
        InitializeComments();

        StoreZeroStarComments();
        StoreOneStarComments();
        StoreTwoStarComments();
        StoreThreeStarComments();
    }

    void InitializeComments()
    {
        allComments = comments
                    ? comments.text
                    .Split(new[] { Environment.NewLine, ":" }, StringSplitOptions.RemoveEmptyEntries)
                    : null;
    }

    void StoreThreeStarComments()
    {
        for (int i = 1; i <= 7; i++)
        {
            threeStarComments.Add(allComments[i]);
        }
    }
    void StoreTwoStarComments()
    {
        for (int i = 9; i <= 15; i++)
        {
            twoStarComments.Add(allComments[i]);
        }
    }
    void StoreOneStarComments()
    {
        for (int i = 17; i <= 23; i++)
        {
            oneStarComments.Add(allComments[i]);
        }
    }
    void StoreZeroStarComments()
    {
        for (int i = 25; i <= 31; i++)
        {
            zeroStarComments.Add(allComments[i]);
        }
    }

    public string GetZeroStarComment()
    {
        return zeroStarComments[Range(0, zeroStarComments.Count)];
    }
    public string GetOneStarComment()
    {
        return oneStarComments[Range(0, oneStarComments.Count)];
    }
    public string GetTwoStarComment()
    {
        return twoStarComments[Range(0, twoStarComments.Count)];
    }
    public string GetThreeStarComment()
    {
        return threeStarComments[Range(0, threeStarComments.Count)];
    }
}
