#include "Pellets.h"
#include <iostream>
#include<vector>
using namespace std;

Pellets::Pellets(vector <vector<int>> v)
{
	arrScore = new int* [v.size()];
	for (unsigned int i = 0; i < v.size(); i++)
	{
		arrScore[i] = new int[v[i].size()];
		for (unsigned int j = 0; j < v[i].size(); j++)
		{
			arrScore[i][j] = 00;
			if (v[i][j] == -5)
				arrScore[i][j] = -5;
			else if (v[i][j] > -1)
				arrScore[i][j] = 100;
		}
	}
	arrScore[5][2] = 1000;
	arrScore[26][2] = 1000;
	arrScore[26][25] = 1000;
	arrScore[5][25] = 1000;
	mPelletTexture.loadFromFile("../images/pellet.png");
	mPowerTexture.loadFromFile("../images/yellowpellet.png");
	mBlack.loadFromFile("../images/black.png");
	mTransparent.loadFromFile("../images/transparent.png");
	score = 0;
}
Pellets::~Pellets()
{
	delete[] arrScore;
	while (arrScore)
	{
		int i = 0;
		delete arrScore[i];
		i++;
	}
}
void Pellets::intersectPellets(int r, int c)
{
	score += arrScore[r][c];
	arrScore[r][c] = -6;
	//cout << r<<" "<<c << endl;
}

void Pellets::drawPellets(RenderWindow& w, RectangleShape** mShape)
{
	for (unsigned int i = 0; i<31; i++)
		for (unsigned int j = 0; j<28; j++)
		{
			if (arrScore[i][j] == 100)
			{
				mShape[i][j].setTexture(&mPelletTexture);

			}
			else if (arrScore[i][j] == 1000)
			{
				mShape[i][j].setTexture(&mPowerTexture);
			}
			else if (arrScore[i][j] == -5)
			{
				mShape[i][j].setTexture(&mBlack);
			}
			else if (arrScore[i][j]==-6)
				mShape[i][j].setFillColor(Color::Transparent);
			w.draw(mShape[i][j]);
		}
	
}


