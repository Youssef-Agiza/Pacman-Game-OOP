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
	font.loadFromFile("../boardTexts/font.ttf");
	T.setFont(font);
	T.setString("SCORE");
	T.setCharacterSize(50);
	T.setOutlineColor(Color::Yellow);
	T.setOutlineThickness(0.5);
	T.setOrigin(Vector2f(-880,0));
	Num.setFont(font);
	Num.setCharacterSize(50);
	Num.setOrigin(Vector2f(-880, -50));
	L.setFont(font);
	L.setCharacterSize(50);
	L.setOrigin(Vector2f(-880, -100)); 
	L.setString("Lifes");
	life.setFont(font);
	life.setCharacterSize(50);
	life.setOrigin(Vector2f(-880, -150));
}
Pellets::~Pellets()
{
	
}
void Pellets::intersectPellets(int r, int c)
{
	if (arrScore[r][c] > 0)
	{
		score += arrScore[r][c];
		arrScore[r][c] = -6;
		chomp.playChomp();
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
			else if (arrScore[i][j]==-6)
				mShape[i][j].setFillColor(Color::Transparent);
			w.draw(mShape[i][j]);
		}
	
}
void Pellets::drawText(RenderWindow& w, int x)
{
	Num.setString(to_string(score));
	w.draw(T);
	w.draw(Num);
	w.draw(L);
	life.setString(to_string(x));
	w.draw(life);
	

	
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


