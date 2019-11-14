using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Node
{
    public string sequenceMovements = "";
    public int F;
    public int G;
    public int H;
    public Node Parent;

    public void addNewMovement(string pMovement)
    {
        this.sequenceMovements = pMovement + this.sequenceMovements;
    }

    public Node(string pSequenceMovements, int pF, int pG, int pH, Node pParent)
    {
        this.sequenceMovements = pSequenceMovements;
        this.F = pF;
        this.G = pG;
        this.H = pH;
        this.Parent = pParent;
    }

    public Node()
    {

    }

    public Node DeepClone()
    {
        Node deepcopyNode = new Node(this.sequenceMovements, this.F,
                           this.G, this.H, this.Parent);
        return deepcopyNode;
    }
}


public class Solutionn
{
    RubiksCube RC;
    RubiksCube target;
    bool verbose;

    public void setVerbose(bool pValue)
    {
        this.verbose = pValue;
    }

    public Solutionn(RubiksCube pTarget, RubiksCube pRC)
    {
        this.target = pTarget;

        this.RC = pRC;
    }

    public List<Node> getWalkableNodes(Node pNode)
    {
        List<Node> walkableNodes = new List<Node>();
        Node right = pNode.DeepClone();
        right.addNewMovement("R");
        Node rightI = pNode.DeepClone();
        right.addNewMovement("Ri");

        Node left = pNode.DeepClone();
        left.addNewMovement("L");
        Node leftI = pNode.DeepClone();
        leftI.addNewMovement("Li");

        Node top = pNode.DeepClone();
        top.addNewMovement("U");
        Node topI = pNode.DeepClone();
        topI.addNewMovement("Ui");

        Node bottom = pNode.DeepClone();
        bottom.addNewMovement("D");
        Node bottomI = pNode.DeepClone();
        bottomI.addNewMovement("Di");

        Node back = pNode.DeepClone();
        back.addNewMovement("B");
        Node backI = pNode.DeepClone();
        backI.addNewMovement("Bi");

        Node front = pNode.DeepClone();
        front.addNewMovement("F");
        Node frontI = pNode.DeepClone();
        frontI.addNewMovement("Fi");

        walkableNodes.Add(right);
        walkableNodes.Add(rightI);
        walkableNodes.Add(left);
        walkableNodes.Add(leftI);
        walkableNodes.Add(back);
        walkableNodes.Add(backI);
        walkableNodes.Add(front);
        walkableNodes.Add(frontI);
        walkableNodes.Add(bottom);
        walkableNodes.Add(bottomI);
        walkableNodes.Add(top);
        walkableNodes.Add(topI);

        return walkableNodes;
    }

    public int ComputeHScore(Node pNode)
    {
        RubiksCube tRubikCube = RC.cloneCube();
        tRubikCube.RunCustomSequence(pNode.sequenceMovements);

        int totalPoints = 0;

        List<List<List<Color>>> cubeColors = this.target.getAllFaces();

        List<Color> listColors = new List<Color>();
        listColors.Add(Cube.ORANGECOLOR);
        listColors.Add(Cube.REDCOLOR);
        listColors.Add(Cube.YELLOWCOLOR);
        listColors.Add(Cube.WHITECOLOR);
        listColors.Add(Cube.GREENCOLOR);
        listColors.Add(Cube.BLUECOLOR);

        for (int indexColors = 0; indexColors < listColors.Count(); indexColors++)
        {
            for(int i = 0; i < cubeColors[indexColors].Count(); i++)
            {
                for(int j = 0; j < cubeColors[indexColors][i].Count(); j++)
                {
                    if (cubeColors[indexColors][i][j] == listColors[indexColors])
                    {
                        totalPoints += 10;
                    }
                }

            }
        }

        //Debug.Log("total points: " + 2*totalPoints);

        return 2*totalPoints;
    }

    public bool isSolved(Node pNode)
    {
        RubiksCube tRubikCube = RC.cloneCube();
        tRubikCube.RunCustomSequence(pNode.sequenceMovements);
        return tRubikCube.isSolved();
    }

    public string A()
    {

        Node current = null;
        var openList = new List<Node>();
        var closedList = new List<Node>();

        int g = 0;

        Node right = new Node();
        right.addNewMovement("R");
        Node rightI = new Node();
        rightI.addNewMovement("Ri");

        Node left = new Node();
        left.addNewMovement("L");
        Node leftI = new Node();
        leftI.addNewMovement("Li");

        Node top = new Node();
        top.addNewMovement("U");
        Node topI = new Node();
        topI.addNewMovement("Ui");

        Node bottom = new Node();
        bottom.addNewMovement("D");
        Node bottomI = new Node();
        bottomI.addNewMovement("Di");

        Node back = new Node();
        back.addNewMovement("B");
        Node backI = new Node();
        backI.addNewMovement("Bi");

        Node front = new Node();
        front.addNewMovement("F");
        Node frontI = new Node();
        frontI.addNewMovement("Fi");

        openList.Add(right);
        openList.Add(rightI);
        openList.Add(left);
        openList.Add(leftI);
        openList.Add(back);
        openList.Add(backI);
        openList.Add(front);
        openList.Add(frontI);
        openList.Add(bottom);
        openList.Add(bottomI);
        openList.Add(top);
        openList.Add(topI);


        int limitAttemps = 1500;
        int counter = 0;

        while (openList.Count > 0 && counter < limitAttemps )
        {
            counter++;
            // get the node with the lowest F score
            var lowest = openList.Min(l => l.F);
            current = openList.First(l => l.F == lowest);



            // add the current cube to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);

            // if we added the destination to the closed list, we've found a path

            if (verbose) {
                //Debug.Log("new Current: ");
                //Debug.Log(current.sequenceMovements);
            }



            if (closedList.FirstOrDefault(l => isSolved(l)) != null)
            {
                if (verbose) { Debug.Log("Solution found"); }

                break;
            }



            List<Node> adjacentNodes = getWalkableNodes(current);
            g++;

            foreach (Node adjacentNode in adjacentNodes)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.sequenceMovements.Equals(adjacentNode.sequenceMovements)) != null)
                {
                    //if(verbose) { Debug.Log("ignored it, since it is in the closedList"); }
                    continue;
                }


                // if it's not in the open list...
                //if(verbose) { Debug.Log("Cheking if currentNode is in openList"); Debug.Log(openList.FirstOrDefault(l => l == adjacentNode)); }
                if (openList.FirstOrDefault(l => l.sequenceMovements.Equals(adjacentNode.sequenceMovements)) == null)
                {
                    // compute its score, set the parent

                    adjacentNode.G = adjacentNode.sequenceMovements.Replace("i", "").Count();
                    adjacentNode.H = ComputeHScore(adjacentNode);
                    //    adjacentNode.Y, target.X, target.Y);
                    adjacentNode.F = adjacentNode.G + adjacentNode.H;
                    //Debug.Log(adjacentNode.H);
                    //Debug.Log(adjacentNode.F);

                    adjacentNode.Parent = current;

                    // and add it to the open list
                    openList.Add(adjacentNode);
                }
                else
                {
                    // if(verbose) { Debug.Log("else statement"); }
                    // test if using the current G score makes the adjacent square's F score
                    // lower, if yes update the parent because it means it's a better path
                    if (g + adjacentNode.H < adjacentNode.F)
                    {
                        adjacentNode.G = g;
                        adjacentNode.F = adjacentNode.G - adjacentNode.H;
                        adjacentNode.Parent = current;
                    }
                }
            }
        }
        /*
        for(int i = 0; i<closedList.Count;i++)
        {
            Debug.Log(closedList[i].sequenceMovements);
        }
        */

        Debug.Log("open list elements");


        Debug.Log(closedList[1].sequenceMovements);

        return closedList[closedList.Count-1].sequenceMovements;

        //RC.RunCustomSequence("B");//openList[openList.Count-1].sequenceMovements);

        /*for (int i = 0; i < openList.Count; i++)
        {
            Debug.Log(openList[i].sequenceMovements);
        }*/
    }
}
