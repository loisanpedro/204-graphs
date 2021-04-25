using System;

namespace SanPedro_Exer3
{
    //Create a Vertex class and its attributes
    public class Vertex
    {
        public string Label;
        public bool Visited = false;//Shows whether vertex has been visited or not

    }

    class Program
    {
        private static int vertexCount = 0; //Marks index of vertex in the array
        private static int top = -1; //Top of stack since DFS will use stack methods
        private static int rear = -1; //Rear of queue since BFS will use queue methods
        private static int front = 0; //Front of queue
        private static int queueItemCount = 0; //Number of items in queue

        //Stack methods
        private static void Push(int[] stack, int item) //Pushes item to stack
        {
            stack[++top] = item;
        }

        private static int Pop(int[] stack) //Removes item from stack
        {
            return stack[top--];
        }

        private static int Peek(int[] stack) //Peeks at top of stack
        {
            return stack[top];
        }

        private static bool IsStackEmpty() //Checks if stack is empty
        {
            return top == -1;
        }


        //Queue methods
        private static void Insert(int[] queue, int item) //Inserts vertex to queue
        {
            queue[++rear] = item; //Inserts data to rear of queue
            queueItemCount++; //Adds to queue count
        }

        private static int RemoveData(int[] queue) //Removes vertex from queue
        {
            queueItemCount--; //Subtracts from queue count
            return queue[front++]; //Return front of queue
        }

        private static bool IsQueueEmpty() //Checks if queue is empty
        {
            return queueItemCount == 0; //Returns a bool value of true if queue item count is equal to zero
        }

        //Graph methods
        public static void AddVertex(Vertex[] newarrVertices, string label) //Adds a vertex to the array of vertices and increases vertex count
        {
            Vertex newVertex = new Vertex(); //Create new Vertex object
            newVertex.Label = label; //Assign label parameter passed to Label attribute of created vertex
            newarrVertices[vertexCount++] = newVertex; 
        }

        public static void AddEdge(int[,] newadjacencyMatrix, int start, int end) //Adds edge to the adjacency matrix represented by a 2d array
        {
            newadjacencyMatrix[start, end] = 1; //Set up adjacency matrix representing edge of directed graph
            newadjacencyMatrix[end, start] = 1;
        }

        private static void DisplayVertex(Vertex[] newarrVertices, int vertexIndex) //Displays vertex of specified array
        {
            Console.WriteLine(newarrVertices[vertexIndex].Label + "");
        }

        private static void PrintVertex(Vertex[] newarrVertices, int vertexIndex) //Prints message if vertex was found on the graph
        {
            Console.WriteLine("Vertex " + newarrVertices[vertexIndex].Label + " was found on the graph.");
        }


        private static int GetAdjacentUnvisitedVertex(Vertex[] newarrVertices, int[,] newadjacencyMatrix, int vertexIndex) //Gets the next vertex to be visited
        {
            for (int i = 0; i < vertexCount; ++i)
            {
                if (newadjacencyMatrix[vertexIndex, i] == 1 && newarrVertices[i].Visited == false) //Vertex is adjacent AND unvisited
                {
                    return i;//Return the vertex index
                }
            }
            return -1; //Else, vertex is not adjacent or has been visited already
        }

        public static void DepthFirstTraversal(Vertex[] newarrVertices, int[,] newadjacencyMatrix, int[] newstack) //DFS Method - Print
        {
            newarrVertices[0].Visited = true; //Index 0 of the vertices array is marked as visited
            DisplayVertex(newarrVertices, 0); //Prints Label of the visited vertex
            Push(newstack, 0); //Adds index 0 to the stack

            while (!IsStackEmpty()) //While stack is not empty
            {
                int unvisitedVertex = GetAdjacentUnvisitedVertex(newarrVertices, newadjacencyMatrix, Peek(newstack));//Store index of adjacent unvisited vertices of vertex at the top of the stack
                if (unvisitedVertex == -1) //If vertex is not adjacent and unvisited
                {
                    Pop(newstack); //Remove it from the stack
                }
                else //Vertex has not been visited yet so visit the vertex
                {
                    newarrVertices[unvisitedVertex].Visited = true; //Mark vertex as visited
                    DisplayVertex(newarrVertices, unvisitedVertex); //Prints Label of the visited vertex
                    Push(newstack, unvisitedVertex); //Adds index of the visited vertex to the stack
                }
            }

            for (int i = 0; i < vertexCount; ++i)
            {
                newarrVertices[i].Visited = false;
            }

        }


        public static void BreadthFirstTraversal(Vertex[] arrVertices, int[,] adjacencyMatrix, int[] queue) //BFS Method - Print
        {

            arrVertices[0].Visited = true; //Index 0 of the vertices array is marked as visited
            DisplayVertex(arrVertices, 0); //Prints Label of the visited vertex
            Insert(queue, 0); //Adds index 0 to the queue
            int unvisitedVertex; //Set up int variable to contain unvisited vertex

            while (!IsQueueEmpty()) //While queue is not empty
            {
                int tempVertex = RemoveData(queue); //Store front of queue in temp
                while ((unvisitedVertex = GetAdjacentUnvisitedVertex(arrVertices, adjacencyMatrix, tempVertex)) != -1) //While there are adjacent unvisited vertices
                {
                    arrVertices[unvisitedVertex].Visited = true; //Marks vertex as visited
                    DisplayVertex(arrVertices, unvisitedVertex); //Prints Label of the visited vertex
                    Insert(queue, unvisitedVertex); //Add index of the visited vertex to the queue
                }
            }

            for (int i = 0; i < vertexCount; i++)
            {
                arrVertices[i].Visited = false;
            }
        }

        public static void DepthFirstSearch(Vertex[] arrVertices, int[,] adjacencyMatrix, int[] stack, string value) //DFS Method - Search
        {
            arrVertices[0].Visited = true; //Index 0 of the vertices array is marked as visited
            Push(stack, 0); //Adds index 0 to the stack
            if (arrVertices[0].Label == value) //If Label of the index 0 vertex is equal to the input value
            {
                PrintVertex(arrVertices, 0); //Prints Label of index 0
            }
            else //If not equal to index 0, search the stack
            {
                while (!IsStackEmpty()) //While stack is not empty
                {
                    int unvisitedVertex = GetAdjacentUnvisitedVertex(arrVertices, adjacencyMatrix, Peek(stack));//Store index of adjacent unvisited vertices of vertex at the top of the stack
                    if (unvisitedVertex == -1) //If vertex is not adjacent and unvisited
                    {
                        Pop(stack); //Remove it from the stack
                    }
                    else //Vertex has not been visited yet so visit the vertex
                    {
                        arrVertices[unvisitedVertex].Visited = true; //Marks vertex as visited
                        Push(stack, unvisitedVertex); //Adds index of the visited vertex to the stack
                        if (arrVertices[unvisitedVertex].Label == value) //If input value is found in the stack
                        {
                            PrintVertex(arrVertices, unvisitedVertex); //Prints Label attribute of the vertex
                            break;
                        }
                    }
                }
                for (int i = 0; i < vertexCount; i++)
                {
                    arrVertices[i].Visited = false; 
                }

            }
            if (IsStackEmpty() == true) //If stack is empty after traversal, vertex is not in the stack so print message
            {
                Console.Write("Vertex not found! Press any key to continue.");
            }
            else
            {
                Console.Write("Press any key to continue.");
            }
        }

        public static void BreadthFirstSearch(Vertex[] arrVertices, int[,] adjacencyMatrix, int[] queue, string value) //BFS Method - Search
        {

            arrVertices[0].Visited = true; //Index 0 of the vertices array is marked as visited
            Insert(queue, 0); //Adds index 0 to the queue
            if (arrVertices[0].Label == value) //If Label of the index 0 vertex is equal to the input value
            {
                PrintVertex(arrVertices, 0); //Prints Label of index 0
            }
            else //If not equal to index 0, search the queue
            {
                while (!IsQueueEmpty()) //While queue is not empty
                {
                    int tempVertex = RemoveData(queue); //Store front of queue in temp
                    int unvisitedVertex; //Set up int variable to contain unvisited vertex
                    while ((unvisitedVertex = GetAdjacentUnvisitedVertex(arrVertices, adjacencyMatrix, tempVertex)) != -1) //While there are adjacent unvisited vertices
                    {
                        arrVertices[unvisitedVertex].Visited = true; //Marks vertex as visited
                        Insert(queue, unvisitedVertex); //Adds vertex to the queue
                        if (arrVertices[unvisitedVertex].Label == value) //If Label of visited vertex is equal to the input value
                        {
                            PrintVertex(arrVertices, unvisitedVertex); //Prints Label attribute of the vertex
                            break;
                        }
                    }
                }

                for (int i = 0; i < vertexCount; i++)
                {
                    arrVertices[i].Visited = false;
                }
            }
            if (IsQueueEmpty() == true) //If queue is empty after traversal, vertex is not in the queue so print message
            {
                Console.Write("Vertex not found! Press any key to continue.");
            }
            else
            {
                Console.Write("Press any key to continue.");
            }
        }

        static void Main(string[] args)
        {
            int userMenuInput = 0;

            Console.WriteLine("[1] Perform Depth First Traversal");
            Console.WriteLine("[2] Perform Breadth First Traversal");
            Console.WriteLine("[3] Search Graph 1 (DFS)");
            Console.WriteLine("[4] Search Graph 2 (BFS)");
            Console.WriteLine("[5] Exit");
            while (true)
            {
                Console.Write("Enter a number depending on the operation you would like to perform.");
                Console.WriteLine("");
                userMenuInput = Convert.ToInt32(Console.ReadLine());//Get user input for what they want to do
                int input;
                string vertexChoice;
                switch (userMenuInput)
                {
                    case 1:
                        Console.Write("Which graph would you like to traverse? Please select either 1 or 2 only.");
                        input = Convert.ToInt32(Console.ReadLine());//Graph selection of user
                        if (input == 1) //Perform Depth First Traversal on Graph1
                        {
                            //Construct Graph 1 and call Depth First Traversal method
                            Console.WriteLine("Graph 1 Depth First Traversal");
                            int graph1max1 = 7;
                            vertexCount = 0;
                            int[] stack1 = new int[graph1max1];
                            Vertex[] arrVertices = new Vertex[graph1max1];
                            int[,] adjacencyMatrix = new int[graph1max1, graph1max1];


                            for (int i = 0; i < graph1max1; i++)
                                for (int j = 0; j < graph1max1; j++)
                                    adjacencyMatrix[i, j] = 0;

                            AddVertex(arrVertices, " 1");
                            AddVertex(arrVertices, " 2");
                            AddVertex(arrVertices, " 3");
                            AddVertex(arrVertices, " 4");
                            AddVertex(arrVertices, " 5");
                            AddVertex(arrVertices, " 6");
                            AddVertex(arrVertices, " 7");
                            AddEdge(adjacencyMatrix, 0, 1);
                            AddEdge(adjacencyMatrix, 0, 2);
                            AddEdge(adjacencyMatrix, 1, 3);
                            AddEdge(adjacencyMatrix, 2, 5);
                            AddEdge(adjacencyMatrix, 3, 4);
                            AddEdge(adjacencyMatrix, 4, 1);
                            AddEdge(adjacencyMatrix, 5, 3);
                            AddEdge(adjacencyMatrix, 5, 6);
                            DepthFirstTraversal(arrVertices, adjacencyMatrix, stack1);

                            Console.WriteLine("Press any key to continue.");
                            Console.ReadLine();
                            break;
                        }
                        if (input == 2) //Perform Depth First Traversal on Graph2
                        {
                            //Construct Graph 2 and call Depth First Traversal method
                            Console.WriteLine("Graph 2 Depth First Traversal");
                            int graph2max1 = 5;
                            vertexCount = 0;
                            int[] stack2 = new int[graph2max1];
                            Vertex[] arrVertices2 = new Vertex[graph2max1];
                            int[,] adjacencyMatrix2 = new int[graph2max1, graph2max1];

                            for (int i = 0; i < graph2max1; i++)
                                for (int j = 0; j < graph2max1; j++)
                                    adjacencyMatrix2[i, j] = 0;

                            AddVertex(arrVertices2, " A");
                            AddVertex(arrVertices2, " B");
                            AddVertex(arrVertices2, " C");
                            AddVertex(arrVertices2, " D");
                            AddVertex(arrVertices2, " E");
                            AddEdge(adjacencyMatrix2, 0, 1);
                            AddEdge(adjacencyMatrix2, 1, 4);
                            AddEdge(adjacencyMatrix2, 4, 2);
                            AddEdge(adjacencyMatrix2, 4, 3);
                            DepthFirstTraversal(arrVertices2, adjacencyMatrix2, stack2);

                            Console.WriteLine("Press any key to continue.");
                            Console.ReadLine();
                            break;

                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice");
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadLine();
                        }
                        break;


                    case 2:
                        Console.Write("Which graph would you like to traverse?");
                        input = Convert.ToInt32(Console.ReadLine());//Graph selection of user
                        if (input == 1) //Perform Breadth First Traversal on Graph1
                        {
                            //Construct Graph 1 and call Breadth First Traversal method
                            Console.WriteLine("Graph 1 Breadth First Traversal");
                            int graph1max2 = 7;
                            vertexCount = 0;
                            int[] queue1 = new int[100];
                            Vertex[] arrVertices = new Vertex[graph1max2];
                            int[,] adjacencyMatrix = new int[graph1max2, graph1max2];

                            for (int i = 0; i < graph1max2; i++)
                                for (int j = 0; j < graph1max2; j++)
                                    adjacencyMatrix[i, j] = 0;

                            AddVertex(arrVertices, " 1");
                            AddVertex(arrVertices, " 2");
                            AddVertex(arrVertices, " 3");
                            AddVertex(arrVertices, " 4");
                            AddVertex(arrVertices, " 5");
                            AddVertex(arrVertices, " 6");
                            AddVertex(arrVertices, " 7");
                            AddEdge(adjacencyMatrix, 0, 1);
                            AddEdge(adjacencyMatrix, 0, 2);
                            AddEdge(adjacencyMatrix, 1, 3);
                            AddEdge(adjacencyMatrix, 2, 5);
                            AddEdge(adjacencyMatrix, 3, 4);
                            AddEdge(adjacencyMatrix, 4, 1);
                            AddEdge(adjacencyMatrix, 5, 3);
                            AddEdge(adjacencyMatrix, 5, 6);
                            BreadthFirstTraversal(arrVertices, adjacencyMatrix, queue1);
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadLine();
                            break;
                        }
                        else if (input == 2) //Perform Breadth First Traversal on Graph2
                        {
                            //Construct Graph 2 and call Breadth First Traversal method
                            Console.WriteLine("Graph 2 Breadth First Traversal");
                            int graph2max2 = 5;
                            vertexCount = 0;
                            int[] queue2 = new int[100];
                            Vertex[] arrVertices2 = new Vertex[graph2max2];
                            int[,] adjacencyMatrix2 = new int[graph2max2, graph2max2];

                            for (int i = 0; i < graph2max2; i++)
                                for (int j = 0; j < graph2max2; j++)
                                    adjacencyMatrix2[i, j] = 0;

                            AddVertex(arrVertices2, " A");
                            AddVertex(arrVertices2, " B");
                            AddVertex(arrVertices2, " C");
                            AddVertex(arrVertices2, " D");
                            AddVertex(arrVertices2, " E");
                            AddEdge(adjacencyMatrix2, 0, 1);
                            AddEdge(adjacencyMatrix2, 1, 4);
                            AddEdge(adjacencyMatrix2, 4, 2);
                            AddEdge(adjacencyMatrix2, 4, 3);
                            BreadthFirstTraversal(arrVertices2, adjacencyMatrix2, queue2);
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadLine();

                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice");
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadLine();
                        }
                        break;

                    case 3:
                        //Construct Graph 1 and call Depth First Search method
                        Console.WriteLine("Enter the vertex that you would like to search for on Graph 1.");
                        vertexChoice = (" " + (Console.ReadLine()));//Input for vertex
                        int graph1max = 7;
                        vertexCount = 0;
                        int[] stack3 = new int[graph1max];
                        Vertex[] arrVertices3 = new Vertex[graph1max];
                        int[,] adjacencyMatrix3 = new int[graph1max, graph1max];

                        for (int i = 0; i < graph1max; i++)
                            for (int j = 0; j < graph1max; j++)
                                adjacencyMatrix3[i, j] = 0;

                        AddVertex(arrVertices3, " 1");
                        AddVertex(arrVertices3, " 2");
                        AddVertex(arrVertices3, " 3");
                        AddVertex(arrVertices3, " 4");
                        AddVertex(arrVertices3, " 5");
                        AddVertex(arrVertices3, " 6");
                        AddVertex(arrVertices3, " 7");
                        AddEdge(adjacencyMatrix3, 0, 1);
                        AddEdge(adjacencyMatrix3, 0, 2);
                        AddEdge(adjacencyMatrix3, 1, 3);
                        AddEdge(adjacencyMatrix3, 2, 5);
                        AddEdge(adjacencyMatrix3, 3, 4);
                        AddEdge(adjacencyMatrix3, 4, 1);
                        AddEdge(adjacencyMatrix3, 5, 3);
                        AddEdge(adjacencyMatrix3, 5, 6);
                        DepthFirstSearch(arrVertices3, adjacencyMatrix3, stack3, vertexChoice);
                        Console.ReadLine();
                        break;

                    case 4:
                        //Construct Graph 2 and call Breadth First Search method
                        Console.WriteLine("Enter the vertex that you would like to search for on Graph 2.");
                        vertexChoice = (" " + (Console.ReadLine()));//Input for vertex
                        int graph2max = 5;
                        vertexCount = 0;
                        int[] queue4 = new int[graph2max];
                        Vertex[] arrVertices4 = new Vertex[graph2max];
                        int[,] adjacencyMatrix4 = new int[graph2max, graph2max];

                        for (int i = 0; i < graph2max; i++)
                            for (int j = 0; j < graph2max; j++)
                                adjacencyMatrix4[i, j] = 0;

                        AddVertex(arrVertices4, " A");
                        AddVertex(arrVertices4, " B");
                        AddVertex(arrVertices4, " C");
                        AddVertex(arrVertices4, " D");
                        AddVertex(arrVertices4, " E");
                        AddEdge(adjacencyMatrix4, 0, 1);
                        AddEdge(adjacencyMatrix4, 1, 4);
                        AddEdge(adjacencyMatrix4, 4, 2);
                        AddEdge(adjacencyMatrix4, 4, 3);
                        BreadthFirstSearch(arrVertices4, adjacencyMatrix4, queue4, vertexChoice);
                        Console.ReadLine();
                        break;

                    case 5:
                        System.Environment.Exit(1);
                        break;


                }
            }
        }
    }

}




