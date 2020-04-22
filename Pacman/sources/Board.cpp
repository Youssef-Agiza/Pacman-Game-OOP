#include "Board.h"

//default values
const int ROWS = 10;
const int COLS = 10;
const int WALL = -1;
const int TILE = 50;
const float POSITION = 50;

namespace PacmanCS {

	Board::Board(int rows=ROWS, int cols=COLS,float tileSize=TILE,float posOnWindow=POSITION):
		mNumOfRows(rows),mNumOfCols(cols),mTileSize(tileSize),mPositionOnWindow(posOnWindow)
	{
		intializeShape();
	}

	void Board::intializeShape()
	{
		if (mNumOfCols<=0||mNumOfRows <= 0)
			return;
		mShape = new sf::RectangleShape * [mNumOfRows];
		for (int i = 0; i < mNumOfRows; i++)
		{
			mShape[i] = new sf::RectangleShape[mNumOfCols];
			for (int j = 0; j < mNumOfCols; j++)
			{
				mShape[i][j].setPosition(mPositionOnWindow + mTileSize * j, mPositionOnWindow + mTileSize * i);
				mShape[i][j].setSize(sf::Vector2f(mTileSize, mTileSize));
			}
		}
	}

	void Board::setTextures(int** arr)
	{
		if (arr == nullptr)
			return;
		for (int i = 0; i < mNumOfRows; i++)
			for (int j = 0; j < mNumOfCols; j++)
			{
				if (arr[i][j] == WALL)

					mShape[i][j].setTexture(&(this->mWallTexture));
				else
				{
					mShape[i][j].setTexture(&(this->mRoadTexture));
					mShape[i][j].setFillColor(sf::Color::Black);
				}
			}
	}

	void Board::drawOnWindow(sf::RenderWindow& w)
	{//needs validation: check that shape is not a nullpointer
		for (int i = 0; i < mNumOfRows; i++)
			for(int j=0;j<mNumOfCols;j++)
			w.draw((this->mShape[i][j]));

	}

	//setters
	//setters
	Board& Board::setRoadTexture(std::string fileName)
	{
		if (!fileName.empty())
			mRoadTexture.loadFromFile(fileName);
		return *this;
	}
	Board& Board::setWallTexture(std::string fileName)
	{
		if (!fileName.empty())
			mWallTexture.loadFromFile(fileName);
		return *this;

	}


	//getters
	float Board::getPositionOneWindow()const { return mPositionOnWindow; }
	float Board::getTileSize()const { return mTileSize; }
	int Board::getNumOfCols()const { return mNumOfCols; }
	int Board::getNumOfRows()const { return mNumOfRows; }
	

}