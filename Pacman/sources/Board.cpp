#include "../headers/Board.h"
#include "../headers/Pacman.h"
//default values
//const int ROWS = 10;
//const int COLS = 10;
const int WALL = -1;
const int TILE = 50;
const float POSITION = 50;

namespace PacmanCS {

	Board::Board(std::vector < std::vector<int>> board,float tileSize=TILE,sf::Vector2f 
		posOnWindow=sf::Vector2f(POSITION,POSITION)):
		mBoard(board),mTileSize(tileSize),mPositionOnWindow(posOnWindow)
	{
		
		mWallTexture.loadFromFile("../images/wall.png");
		intializeShape();
		updateTextures();
	}

	void Board::intializeShape()
	{
		
		mShape = new sf::RectangleShape * [mBoard.size()];
		for (unsigned int i = 0; i < mBoard.size(); i++)
		{
			mShape[i] = new sf::RectangleShape[mBoard[i].size()];
			for (unsigned int j = 0; j < mBoard[i].size(); j++)
			{
				mShape[i][j].setPosition(mPositionOnWindow.x + mTileSize * j, mPositionOnWindow.y + mTileSize * i);
				mShape[i][j].setSize(sf::Vector2f(mTileSize, mTileSize));
			}
		}
	}

	void Board::updateTextures()
	{

		for (unsigned int i = 0; i < mBoard.size(); i++)
			for (unsigned int j = 0; j < mBoard[i].size(); j++)
			{
				if (mBoard[i][j] == WALL)
					mShape[i][j].setTexture(&(this->mWallTexture));
			}
	}

	int Board::checkVertex(unsigned int r, unsigned int c) const
	{
		if (r >= mBoard.size() || c >= mBoard[r].size())
			return -1;
		return (mBoard[r][c] < 0) ? 0 : 1;
	}

	
	void Board::drawOnWindow(sf::RenderWindow& w)
	{//needs validation: check that shape is not a nullpointer
		for (unsigned int i = 0; i < mBoard.size(); i++)
			for(unsigned int j=0;j< mBoard[i].size();j++)
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
	const sf::Vector2f& Board::getPositionOneWindow()const { return mPositionOnWindow; }
	float Board::getTileSize()const { return mTileSize; }
	const std::vector < std::vector<int>>& Board::getBoard()const { return mBoard; }
	
}