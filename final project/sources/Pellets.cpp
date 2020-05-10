#include "../headers/Pellets.h"
#include "../headers/Common.h"
using namespace sf;
using namespace std;

Pellets::Pellets(vector <vector<int>> v):arrScore(v),mPelletCount(0)
{
	
	
	for (unsigned int i = 0; i < v.size(); i++)
	{
		for (unsigned int j = 0; j < v[i].size(); j++)
		{
			arrScore[i][j] = 0;
			if (v[i][j] == -5)
				arrScore[i][j] = -5;
			else if (v[i][j] > -1)
			{
				arrScore[i][j] = 100;
				mPelletCount++;
			}
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
			mPelletCount--;
		}
	for (int i = 11; i <= 17; i++)
	{
		arrScore[i][9] = -6; 
		arrScore[i][18] = -6; 
		for (int j = 9; j < 18; j++)
		{
			arrScore[11][j] = -6;//no pellets around the home
			arrScore[17][j] = -6;
		}
	}
	arrScore[14][0] = -6; mPelletCount--;
	arrScore[14][27] = -6; mPelletCount--;
	arrScore[12][13]=-6; mPelletCount--;
	arrScore[12][14]=-6; mPelletCount--;
	mPelletCount -= 30;
	mPelletTexture.loadFromFile("../images/pellet.png");
	mPowerTexture.loadFromFile("../images/yellowpellet.png");
	mBlack.loadFromFile("../images/black.png");
	mTransparent.loadFromFile("../images/transparent.png");
	mFruit.loadFromFile("../images/fruits.png");
}
Pellets::~Pellets()
{
	
}
void Pellets::intersectPellets( Pacman* pacman)
{
	int r = pacman->getRow(); int c = pacman->getCol();
	if (arrScore[r][c] > 0)
	{

		pacman->incrementScore(arrScore[r][c]);
		arrScore[r][c] = -6;
		if (((r == 5) && (c == 2)) || ((r == 26) && (c == 25)) || ((r == 26) && (c == 2)) || (((r == 5) && (c == 25))))
		{
				pacman->setPowerUp(true);
				arrScore[r][c] = -6;
		}
		Audio::getInstance()->playChomp();
		mPelletCount--;
	}
	if ((mPelletCount == 250) || (mPelletCount == 200) || (mPelletCount == 150) || (mPelletCount == 100))
		addFruit();
}
	

void Pellets::drawPellets(RenderWindow& w, RectangleShape** mShape)
{
	for (unsigned int i = 0; i<31; i++)
		for (unsigned int j = 0; j<28; j++)
		{
			if (arrScore[i][j] == 100)
			{
				mShape[i][j].setFillColor(sf::Color::Yellow);
				mShape[i][j].setTexture(&mPelletTexture);

			}
			else if (arrScore[i][j] == 1000)
			{
				mShape[i][j].setFillColor(sf::Color::Yellow);
				mShape[i][j].setTexture(&mPowerTexture);
			}
			else if (arrScore[i][j] == -5)
			{
				mShape[i][j].setTexture(&mBlack);
			}
			else if (arrScore[i][j] == 250)
			{
				mShape[i][j].setFillColor(sf::Color::Yellow);
				mShape[i][j].setTexture(&mFruit);
			}
			else if (arrScore[i][j]==-6)
				mShape[i][j].setFillColor(Color::Transparent);
			w.draw(mShape[i][j]);
		}
	
}

void Pellets::addFruit()
{
		if (mPelletCount == 250)
			arrScore[11][9] = 250;
		else if (mPelletCount == 200)
			arrScore[15][9] = 250;
		else if (mPelletCount == 150)
			arrScore[16][18] = 250;
		else if (mPelletCount == 100)
			arrScore[13][9] = 250;
}


