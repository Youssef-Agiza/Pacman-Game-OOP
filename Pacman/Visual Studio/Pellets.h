#pragma once
#include <iostream>
#include <SFML/Audio.hpp>
#include <SFML/Graphics.hpp>
using namespace std;
using namespace sf;

class Pellets
{
public:
	int** score;
	vector < std::vector<int>> B;
	Texture mPT;
	Texture mPPT;
	Pellets(vector < std::vector<int>> b)
	{
		for (int i = 0; i < b.size(); i++)
		{
			for (int j = 0; j < b[i].size();j++)
			{
				B[i][j] = b[i][j];
				if (b[i][j] > 0)
				{
					score[i][j] = 100;
				}
			}

		}

	}
	void drawOnWindow(sf::RenderWindow& w)
	{
		for (int i = 0; i < B.size(); i++)
			for (int j = 0; j < B[i].size(); j++)
				if (B[i][j] > 0)
				{
					score[i][j] = 100;
				}

	}
	void setTextues()
	{
		mPT.loadFromFile(".. / images / pellet.png");
		mPPT.loadFromFile(".. / images / yellowpellet.png");
	}






};

