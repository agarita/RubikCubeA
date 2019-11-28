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

    public Solutionn(RubiksCube pTarget, RubiksCube pRC, bool pValue)
    {
        this.target = pTarget;
        this.RC = pRC;
        this.verbose = pValue;
    }

    public List<Node> getWalkableNodes(Node pNode)
    {
        List<Node> walkableNodes = new List<Node>();
        Node right = pNode.DeepClone();
        right.addNewMovement("R");
        Node rightI = pNode.DeepClone();
        rightI.addNewMovement("Ri");

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

    public bool piece1(List<List<List<Color>>> cube){
      return cube[3][0][0] == Cube.WHITECOLOR &&
             cube[0][0][0] == Cube.ORANGECOLOR &&
             cube[5][0][2] == Cube.BLUECOLOR;
    }

    public bool piece2(List<List<List<Color>>> cube){
      return cube[3][0][2] == Cube.WHITECOLOR &&
             cube[1][0][2] == Cube.REDCOLOR &&
             cube[5][0][0] == Cube.BLUECOLOR;
    }

    public bool piece3(List<List<List<Color>>> cube){
      return cube[3][2][2] == Cube.WHITECOLOR &&
             cube[1][0][0] == Cube.REDCOLOR &&
             cube[4][0][2] == Cube.GREENCOLOR;
    }

    public bool piece4(List<List<List<Color>>> cube){
      return cube[3][2][0] == Cube.WHITECOLOR &&
             cube[0][0][2] == Cube.ORANGECOLOR &&
             cube[4][0][0] == Cube.GREENCOLOR;
    }

    public bool piece5(List<List<List<Color>>> cube){
      return cube[2][2][0] == Cube.YELLOWCOLOR &&
             cube[0][2][0] == Cube.ORANGECOLOR &&
             cube[5][2][2] == Cube.BLUECOLOR;
    }

    public bool piece6(List<List<List<Color>>> cube){
      return cube[2][2][2] == Cube.YELLOWCOLOR &&
             cube[1][2][2] == Cube.REDCOLOR &&
             cube[5][2][0] == Cube.BLUECOLOR;
    }

    public bool piece7(List<List<List<Color>>> cube){
      return cube[2][0][2] == Cube.YELLOWCOLOR &&
             cube[1][2][0] == Cube.REDCOLOR &&
             cube[4][2][2] == Cube.GREENCOLOR;
    }

    public bool piece8(List<List<List<Color>>> cube){
      return cube[2][0][0] == Cube.YELLOWCOLOR &&
             cube[0][2][2] == Cube.ORANGECOLOR &&
             cube[4][2][0] == Cube.GREENCOLOR;
    }

    public bool piece9(List<List<List<Color>>> cube){
      return cube[3][1][0] == Cube.WHITECOLOR &&
             cube[0][0][1] == Cube.ORANGECOLOR;
    }

    public bool piece10(List<List<List<Color>>> cube){
      return cube[3][0][1] == Cube.WHITECOLOR &&
             cube[5][0][1] == Cube.BLUECOLOR;
    }

    public bool piece11(List<List<List<Color>>> cube){
      return cube[3][1][2] == Cube.WHITECOLOR &&
             cube[1][0][1] == Cube.REDCOLOR;
    }

    public bool piece12(List<List<List<Color>>> cube){
      return cube[3][2][1] == Cube.WHITECOLOR &&
             cube[4][0][1] == Cube.GREENCOLOR;
    }

    public bool piece13(List<List<List<Color>>> cube){
      return cube[0][1][0] == Cube.ORANGECOLOR &&
             cube[5][1][2] == Cube.BLUECOLOR;
    }

    public bool piece14(List<List<List<Color>>> cube){
      return cube[5][1][0] == Cube.BLUECOLOR &&
             cube[1][1][2] == Cube.REDCOLOR;
    }

    public bool piece15(List<List<List<Color>>> cube){
      return cube[1][1][0] == Cube.REDCOLOR &&
             cube[4][1][2] == Cube.GREENCOLOR;
    }

    public bool piece16(List<List<List<Color>>> cube){
      return cube[4][1][0] == Cube.GREENCOLOR &&
             cube[0][1][2] == Cube.ORANGECOLOR;
    }

    public bool piece17(List<List<List<Color>>> cube){
      return cube[2][1][0] == Cube.YELLOWCOLOR &&
             cube[0][2][1] == Cube.ORANGECOLOR;
    }

    public bool piece18(List<List<List<Color>>> cube){
      return cube[2][2][1] == Cube.YELLOWCOLOR &&
             cube[5][2][1] == Cube.BLUECOLOR;
    }

    public bool piece19(List<List<List<Color>>> cube){
      return cube[2][1][2] == Cube.YELLOWCOLOR &&
             cube[1][2][1] == Cube.REDCOLOR;
    }

    public bool piece20(List<List<List<Color>>> cube){
      return cube[2][0][1] == Cube.YELLOWCOLOR &&
             cube[4][2][1] == Cube.GREENCOLOR;
    }

    public double ComputeHScore(Node pNode)
    {
        bool admissable = true;
        RubiksCube tRubikCube = RC.cloneCube();
        tRubikCube.RunCustomSequence(pNode.sequenceMovements);

        double totalPoints = 20.0;

        //List<List<List<Color>>> cubeColors = this.target.getAllFaces();

        List<List<List<Color>>> cubeColors = tRubikCube.getAllFaces();

        List<Color> listColors = new List<Color>();
        listColors.Add(Cube.ORANGECOLOR); // 0
        listColors.Add(Cube.REDCOLOR);    // 1
        listColors.Add(Cube.YELLOWCOLOR); // 2
        listColors.Add(Cube.WHITECOLOR);  // 3
        listColors.Add(Cube.GREENCOLOR);  // 4
        listColors.Add(Cube.BLUECOLOR);   // 5


        if(piece1(cubeColors)) totalPoints--;
        if(piece2(cubeColors)) totalPoints--;
        if(piece3(cubeColors)) totalPoints--;
        if(piece4(cubeColors)) totalPoints--;
        if(piece5(cubeColors)) totalPoints--;
        if(piece6(cubeColors)) totalPoints--;
        if(piece7(cubeColors)) totalPoints--;
        if(piece8(cubeColors)) totalPoints--;
        if(piece9(cubeColors)) totalPoints--;
        if(piece10(cubeColors)) totalPoints--;
        if(piece11(cubeColors)) totalPoints--;
        if(piece12(cubeColors)) totalPoints--;
        if(piece13(cubeColors)) totalPoints--;
        if(piece14(cubeColors)) totalPoints--;
        if(piece15(cubeColors)) totalPoints--;
        if(piece16(cubeColors)) totalPoints--;
        if(piece17(cubeColors)) totalPoints--;
        if(piece18(cubeColors)) totalPoints--;
        if(piece19(cubeColors)) totalPoints--;
        if(piece20(cubeColors)) totalPoints--;

        if(admissable) totalPoints /= 8.0;

        return totalPoints;
    }

    public bool isSolved(Node pNode)
    {
        RubiksCube tRubikCube = RC.cloneCube();
        tRubikCube.RunCustomSequence(pNode.sequenceMovements);
        return tRubikCube.isSolved();
    }

    public string A()
    {
      Debug.Log("Start");
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


        int limitAttemps = 15000;
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

            if (closedList.FirstOrDefault(l => isSolved(l)) != null)
            {
                if (verbose) {
                  Debug.Log("Solution found");
                }

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

                    adjacentNode.H = (int)ComputeHScore(adjacentNode);

                    //    adjacentNode.Y, target.X, target.Y);
                    adjacentNode.F = adjacentNode.G + adjacentNode.H;

                    Debug.Log("Sequence: " + adjacentNode.sequenceMovements + "\tF: " + adjacentNode.F + " G: " + adjacentNode.G + " H: " + adjacentNode.H);

                    adjacentNode.Parent = current;

                    if(adjacentNode.H == 0){
                      Debug.Log("Solution found...");
                      Debug.Log("open list elements: " + openList.Count);
                      Debug.Log("closed list elements: " + closedList.Count);

                      Debug.Log("solution: " + adjacentNode.sequenceMovements);

                      return adjacentNode.sequenceMovements;

                    }

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

        Debug.Log("open list elements: " + openList.Count);
        Debug.Log("closed list elements: " + closedList.Count);

        Debug.Log("solution: " + closedList[closedList.Count - 1].sequenceMovements);

        //SceneManager.solutionString = closedList[closedList.Count - 1].sequenceMovements;


        return closedList[closedList.Count-1].sequenceMovements;

        //RC.RunCustomSequence("B");//openList[openList.Count-1].sequenceMovements);

        /*for (int i = 0; i < openList.Count; i++)
        {
            Debug.Log(openList[i].sequenceMovements);
        }*/
    }
}
