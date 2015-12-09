// ## Day 9: All in a Single Night
// 
// Every year, Santa manages to deliver all of his presents in a single night.
// 
// This year, however, he has some <span title="Bonus points if you recognize
// all of the locations.">new locations</span> to visit; his elves have provided
// him the distances between every pair of locations. He can start and end at
// any two (different) locations he wants, but he must visit each location
// exactly once. What is the _shortest distance_ he can travel to achieve this?
// 
// For example, given the following distances:
//   London to Dublin = 464
//   London to Belfast = 518
//   Dublin to Belfast = 141
// 
// The possible routes are therefore:
//   Dublin -> London -> Belfast = 982
//   London -> Dublin -> Belfast = 605
//   London -> Belfast -> Dublin = 659
//   Dublin -> Belfast -> London = 659
//   Belfast -> Dublin -> London = 605
//   Belfast -> London -> Dublin = 982
// 
// The shortest of these is `London -> Dublin -> Belfast = 605`, and so the
// answer is `605` in this example.
// 
// What is the distance of the shortest route?
// 
// ## Part Two
//
// The next year, just to show off, Santa decides to take the route with the
// _longest distance_ instead.
// 
// He can still start and end at any two (different) locations he wants, and he
// still must visit each location exactly once.
// 
// For example, given the distances above, the longest route would be `982` via
// (for example) `Dublin -> London -> Belfast`.
// 
// What is the distance of the longest route?
// 
// 
// Source: http://adventofcode.com/day/9, 2015-12-09
// 
// Personal day 9 goal: See "Personal day 9 goal"


module DimitriVranken.AdventOfCode2015.Day9

open System
open System.Collections.Generic
open System.Text.RegularExpressions


let private ParseInstruction (line : string) : (string * string * int) =
    let pattern = @"(.*) to (.*) = (.*)"
    let matchedGroups = Regex.Match(line, pattern).Groups

    let origin = matchedGroups.[1].Value
    let destination = matchedGroups.[2].Value
    let distance = Convert.ToInt32(matchedGroups.[3].Value)

    (origin, destination, distance)

type private Node(name : string) =
    let name = name
    let edges = List<Node * int>()


    member this.Name = name

    member this.Edges = edges

    member this.AddEdge(targetNode : Node, cost : int) =
        edges.Add(targetNode, cost)
        targetNode.AddEdgeInternal(this, cost)

    member private this.AddEdgeInternal(targetNode : Node, cost : int) =
        edges.Add(targetNode, cost)

type private Edge = Node * int

type private Graph() =
    let nodes = new List<Node>()


    member this.Nodes = nodes

    member this.FindPath (pathSelector : (Node -> List<Node> -> Edge))
        (startingNodeIndex : int) : int =
        let unvisitedNodes = new List<Node>(this.Nodes)
        let startingNode = unvisitedNodes.[startingNodeIndex]
        unvisitedNodes.Remove startingNode |> ignore

        let path =
            Graph.FindPathInternal pathSelector
                startingNode unvisitedNodes (new List<Edge>())

        path
        |> Seq.sumBy(fun (_, cost) -> cost)

    static member private FindPathInternal
        (pathSelector : (Node -> List<Node> -> Edge))
        (currentNode : Node)
        (unvisitedNodes : List<Node>)
        (pathSoFar : List<Edge>)
        : List<Edge> =
        if unvisitedNodes.Count = 0 then
            pathSoFar
        else
            let nextEdge = pathSelector currentNode unvisitedNodes

            // Mark node as visited
            let destination, _ = nextEdge
            unvisitedNodes.Remove destination |> ignore

            // Extend travelled path
            pathSoFar.Add nextEdge

            // Recur
            Graph.FindPathInternal pathSelector
                destination unvisitedNodes pathSoFar

let private PopulateGraph (graph : Graph)
    (instruction : (string * string * int)) =
    let origin, destination, distance = instruction

    let findOrCreateNode (name : string) : Node =
        let nodeExists = graph.Nodes.Exists(fun node -> node.Name = name)

        let node =
            if nodeExists then
                graph.Nodes.Find(fun node -> node.Name = name)
            else
                new Node(name)

        if not nodeExists then
            graph.Nodes.Add(node)

        node

    let originNode = findOrCreateNode origin
    let destinationNode = findOrCreateNode destination
            
    originNode.AddEdge(destinationNode, distance)

let private NearestNeighbour (node : Node) (unvisitedNodes : List<Node>)
    : Edge =
    node.Edges
    |> Seq.filter(fun (destination, _) ->
        unvisitedNodes.Exists(fun unvisitedNode ->
            unvisitedNode.Name = destination.Name))
    |> Seq.minBy(fun (_, cost) -> cost)

let private FurthestNeighbour (node : Node) (unvisitedNodes : List<Node>)
    : Edge =
    node.Edges
    |> Seq.filter(fun (destination, _) ->
        unvisitedNodes.Exists(fun unvisitedNode ->
            unvisitedNode.Name = destination.Name))
    |> Seq.maxBy(fun (_, cost) -> cost)


let Solution (input : string) : (int * int) =
    if String.IsNullOrEmpty input then
        raise (ArgumentNullException "input")

    let graph = new Graph()

    input.Split('\n')
    |> Seq.map ParseInstruction
    |> Seq.map (PopulateGraph graph)
    |> Seq.length // Needed so whole loop doesn't get optimized away (needed for
    |> ignore     // side effects)

    let shortestRoute =
        [0..graph.Nodes.Count - 1]
        |> Seq.map (graph.FindPath NearestNeighbour)
        |> Seq.min
    let longestRoute =
        [0..graph.Nodes.Count - 1]
        |> Seq.map (graph.FindPath FurthestNeighbour)
        |> Seq.max

    (shortestRoute, longestRoute)

let FormattedSolution (solution : (int * int)) : string =
    String.Format("Shortest route: {0}\n" +
                  "Longest route: {1}",
                  fst solution, snd solution)
