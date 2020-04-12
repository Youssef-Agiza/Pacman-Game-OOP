#include "Board.h"
namespace PacmanCS {
	const int DEFAULT_ROW = 10;
	const int DEFAULT_COL = 10;
	const int DEFAULT_WALL = -1;
	const int DEFAULT_BLOCK = 50;
	const float DEFAULT_POSITION = 50;
	Board::Board(int rows=DEFAULT_ROW, int cols=DEFAULT_COL, float block=DEFAULT_BLOCK,float poistion=DEFAULT_POSITION, std::string wall="", std::string road )
		:mRows(rows), mCols(cols),mBlockSize(block), mPositionOnWindow(poistion)
	{
		//validate later
		mWallTexture.loadFromFile(wall);
		mRoadTexture.loadFromFile(road);
		mShape = new sf::RectangleShape*[mRows];
		for (int i = 0; i < mRows; i++)
		{
			mShape[i] = new sf::RectangleShape[mCols];
		}
	}
	void Board::drawOnWindow(sf::RenderWindow& w)
	{//needs validation: check that shape is not a nullpointer
		for (int i = 0; i < mRows; i++)
			for(int j=0;j<mCols;j++)
			w.draw((this->mShape[i][j]));

	}
	void Board::setTextures( int** arr)
	{//needs validation to check if the size of arr == the size of mRows,mCols
	// and check the pointers are not null
		for (int i = 0; i < mRows; i++)
			for (int j = 0; j < mCols; j++)
			{
				mShape[i][j].setPosition(mPositionOnWindow + mBlockSize * j, mPositionOnWindow + mBlockSize * i);
				mShape[i][j].setSize(sf::Vector2f(mBlockSize, mBlockSize));
				if (arr[i][j] == DEFAULT_WALL)
				{
					mShape[i][j].setTexture(&(this->mWallTexture));
					mShape[i][j].setFillColor(sf::Color::Blue);
				}
				else
				{
					mShape[i][j].setTexture(&(this->mRoadTexture));
					mShape[i][j].setFillColor(sf::Color::Black);
				}
			}
	
	
	}

}