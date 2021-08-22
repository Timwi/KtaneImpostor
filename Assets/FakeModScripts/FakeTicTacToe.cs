using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeTicTacToe : ImpostorMod 
{
    [SerializeField]
    private TextMesh[] displays;
    [SerializeField]
    private TextMesh upNext;
    private char?[] grid = new char?[9]{ null, null, null, null, null, null, null, null, null };
    private static readonly int[][] ticTacToes = new int[][] { new int[]{0,1,2 },new int[]{3,4,5 },new int[]{6,7,8 },new int[]{0,3,6 },new int[]{1,4,7 },new int[]{2,5,8 },new int[]{0,4,8 },new int[]{2,4,6 }, };
    private char[] order;
    private int[] line;
    private int pointer;
    private int Case;
    private char chosenWinner;

    void Start()
    {
        Case = Rnd.Range(0, 2);
        List<char> preorder;
        if (Case == 0)
        {
            preorder = Enumerable.Range('1', 9).ToArray().Shuffle().Take(4).Select(x => (char)x).ToList();
            for (int i = 0; i < 3; i++)
                preorder.Add(Rnd.Range(0, 2) == 0 ? 'X' : 'O');
            order = preorder.ToArray().Shuffle();
            line = ticTacToes[Rnd.Range(0, 8)];
            chosenWinner = Rnd.Range(0, 2) == 0 ? 'X' : 'O';
            Log("the game is already won by " + chosenWinner);
            foreach (int ix in line)
            {
                grid[ix] = chosenWinner;
                flickerObjs.Add(displays[ix].gameObject);
            }
            for (int i = 0; i < 9; i++)
                if (grid[i] == null)
                    grid[i] = order[pointer++];
            upNext.text = Rnd.Range(0, 2) == 0 ? "X" : "O";
        }
        else
        {
            upNext.text = "";
            Log("nothing is on the up-next display");
            flickerObjs.Add(upNext.transform.parent.Find("UpNextDisplay").gameObject);
            do
            {
                preorder = Enumerable.Range('1', 9).ToArray().Shuffle().Take(4).Select(x => (char)x).ToList();
                for (int i = 0; i < 5; i++)
                    preorder.Add(Rnd.Range(0, 2) == 0 ? 'X' : 'O');
                grid = preorder.Cast<char?>().ToArray().Shuffle();
            } while (ticTacToes.Any(line => line.Select(x => grid[x]).Distinct().Count() == 1)); //If there's any line where all the cells match, redo.
        }
        for (int i = 0; i < 9; i++)
            displays[i].text = grid[i].ToString();
    }
}
