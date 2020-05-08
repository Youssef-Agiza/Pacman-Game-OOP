#include "Pellets.h"
#include "Common.h"
using namespace sf;
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
	for (int i = 13; i < 16; i++)
		for (int j = 11; j < 17; j++)
		{
			arrScore[i][j] = -6;//no pellets in home
		}
	mPelletTexture.loadFromFile("../images/pellet.png");
	mPowerTexture.loadFromFile("../images/yellowpellet.png");
	mBlack.loadFromFile("../images/black.png");
	mTransparent.loadFromFile("../images/transparent.png");
}
Pellets::~Pellets()
{
	
}
void Pellets::intersectPellets(int r, int c, Pacman* P)
{
	if (arrScore[r][c] > 0)
	{
		P->incrementScore(arrScore[r][c]);
		arrScore[r][c] = -6;
		Audio::getInstance()->playChomp();
	}
//	cout<<score<<endl;
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
			else  if (arrScore[i][j]==-6)
				mShape[i][j].setFillColor(Color::Transparent);
			w.draw(mShape[i][j]);
		}
	
}

int Pellets::getScore() const
{
	return score;
}
void Pellets::incrementLife(Pacman* P)
{
		P->setLives(P->getLives() + 1);
}
void Pellets::incrementScore(int x)
{
	score += x;
}


