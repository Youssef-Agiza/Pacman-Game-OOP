#include "../headers/Graph.h"
#include<iostream>
Graph::Graph(int v) :mVertices(v), mArray(nullptr)
{
	// Create an array of adjacency lists. Size of array will be V 
	mArray = new AdjList[v];

	// Initialize each adjacency list as empty by making head as NULL 
	for (int i = 0; i < v; ++i)
		mArray[i].head = NULL;

}

Graph::~Graph()
{
    for (int i = 0; i < mVertices;i++)
    {
        AdjListNode* discard = mArray[i].head;
        
        while (discard != nullptr)
        {
            mArray[i].head = mArray[i].head->next;
            delete discard;
            discard = mArray[i].head;
        }
    }
}

void Graph::addEdge(int src, int dest, int weight)
{
	// Add an edge from src to dest. A new node is added to the adjacency 
			// list of src. The node is added at the beginning 
	struct AdjListNode* newNode = new AdjListNode(dest, weight);
	newNode->next = this->mArray[src].head;
	this->mArray[src].head = newNode;

	// Since graph is undirected, add an edge from dest to src also 
	newNode = new AdjListNode(src, weight);
	newNode->next = this->mArray[dest].head;
	this->mArray[dest].head = newNode;
}

std::list<int>* Graph::_convertToPath(int parent[], int dest, std::list<int>* path)
{

	{
		if (parent[dest] == -1)
			return nullptr;
		_convertToPath(parent, parent[dest], path);
		path->push_back(dest);
		return path;
	}
}

std::list<int>* Graph::convertToPath(int p[], int target, std::list<int>* path)
{
	return	_convertToPath(p, target, path);
	
}
void printArr(int dist[], int n)
{
    printf("Vertex   Distance from Source\n");
    for (int i = 0; i < n; ++i)
        printf("%d \t\t %d\n", i, dist[i]);
}
void printPath(std::list<int> path)
{
    int x;
    while (!path.empty())
    {
        x = path.front();
        std::cout << x << "-->";
        path.pop_front();
    }
}

std::list<int>* Graph::dijkstra(int src, int target)
{
    if (src == target)
        return nullptr;
   
    int V = this->mVertices;
    int* dist=new int[V];      // dist values used to pick minimum weight edge in cut 
    int* parent = new int[V];
    parent[src] = -1;
    // minHeap represents set E 
    struct MinHeap* minHeap = createMinHeap(V);

    // Initialize min heap with all vertices. dist value of all vertices  
    for (int v = 0; v < V; ++v)
    {
        dist[v] = INT_MAX;
        minHeap->array[v] = newMinHeapNode(v, dist[v]);
        minHeap->pos[v] = v;
    }

    // Make dist value of src vertex as 0 so that it is extracted first 
    minHeap->array[src] = newMinHeapNode(src, dist[src]);
    minHeap->pos[src] = src;
    dist[src] = 0;
    decreaseKey(minHeap, src, dist[src]);

    // Initially size of min heap is equal to V 
    minHeap->size = V;

    // In the followin loop, min heap contains all nodes 
    // whose shortest distance is not yet finalized. 
    while (!isEmpty(minHeap))
    {
        // Extract the vertex with minimum distance value 
        struct MinHeapNode* minHeapNode = extractMin(minHeap);
        int u = minHeapNode->v; // Store the extracted vertex number 

        // Traverse through all adjacent vertices of u (the extracted 
        // vertex) and update their distance values 
        struct AdjListNode* pCrawl = this->mArray[u].head;
        while (pCrawl != NULL)
        {
            int v = pCrawl->dest;

            // If shortest distance to v is not finalized yet, and distance to v 
            // through u is less than its previously calculated distance 
            if (isInMinHeap(minHeap, v) && dist[u] != INT_MAX &&
                pCrawl->weight + dist[u] < dist[v])
            {
                dist[v] = dist[u] + pCrawl->weight;

                // update distance value in min heap also 
                decreaseKey(minHeap, v, dist[v]);
                parent[v] = u;
            }
            
            pCrawl = pCrawl->next;
         
        }
    }

    std::list<int>* path= new std::list<int>();
    convertToPath(parent, target, path);
   // printPath(*path);
    delete[]dist;
	delete[]parent;
	return path;
}


