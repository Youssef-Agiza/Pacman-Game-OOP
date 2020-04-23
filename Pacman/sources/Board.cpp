#include "Board.h"

//default values
//const int ROWS = 10;
//const int COLS = 10;
const int WALL = -1;
const int TILE = 50;
const float POSITION = 50;

namespace PacmanCS {

	Board::Board(std::vector < std::vector<int>> board,float tileSize=TILE,float posOnWindow=POSITION):
		mBoard(board),mTileSize(tileSize),mPositionOnWindow(posOnWindow)
	{
		mWallTexture.loadFromFile("../images/wall.png");
		intializeShape();
		updateTextures();
	}

	void Board::intializeShape()
	{
		
		mShape = new sf::RectangleShape * [mBoard.size()];
		for (int i = 0; i < mBoard.size(); i++)
		{
			mShape[i] = new sf::RectangleShape[mBoard[i].size()];
			for (int j = 0; j < mBoard[i].size(); j++)
			{
				mShape[i][j].setPosition(mPositionOnWindow + mTileSize * j, mPositionOnWindow + mTileSize * i);
				mShape[i][j].setSize(sf::Vector2f(mTileSize, mTileSize));
			}
		}
	}

	void Board::updateTextures()
	{
		
		for (int i = 0; i < mBoard.size(); i++)
			for (int j = 0; j < mBoard[i].size(); j++)
			{
				if (mBoard[i][j] == WALL)

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
		for (int i = 0; i < mBoard.size(); i++)
			for(int j=0;j< mBoard[i].size();j++)
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
	//int Board::getNumOfCols()const { return mNumOfCols; }
	//int Board::getNumOfRows()const { return mNumOfRows; }
	const std::vector < std::vector<int>>& Board::getBoard()const { return mBoard; }
	

}