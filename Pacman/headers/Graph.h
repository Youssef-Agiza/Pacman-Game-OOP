#pragma once
#include <list>
#include <cstdlib>
#include <list>
#include "MinHeap.h"

// A structure to represent a node in adjacency list 
struct AdjListNode
{
	int dest;
	int weight;
	struct AdjListNode* next;

	AdjListNode(int p_dest,int p_weight):dest(p_dest),weight(p_weight),next(nullptr){}
};

// A structure to represent an adjacency list 
struct AdjList
{
	struct AdjListNode* head; // pointer to head node of list 
};


class Graph
{
	int mVertices; //number of vertices
	AdjList* mArray;
	std::list<int>* _convertToPath(int parent[], int dest, std::list<int>* path);

public:
	Graph(int v);
	void addEdge(int src, int dest, int weight = 1);


	std::list<int>* convertToPath(int p[], int target, std::list<int>* path); //called in dijkstra to get list of vertices to trace

	std::list<int>* dijkstra(int src,int target);

};





