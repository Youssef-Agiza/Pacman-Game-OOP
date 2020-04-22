#include "Pacman.h"
//random numbers
const int INTIAL_ROW = 23;
const int INTIAL_COL = 14;
Pacman::Pacman(int intialRow, int intialColumn, float size, float posWindow) :
	Character( intialRow,  intialColumn,  size,  posWindow),mIntialColumn(INTIAL_COL)
	,mIntialRow(INTIAL_ROW),mLives(3),mBoard(nullptr)
{
	
}

Pacman& Pacman::setBoard(Board* board)
{
	mBoard = board;
	return*this;
}
Pacman& Pacman::setLives(unsigned int l) 
{
	mLives = l;
	return *this;
}
unsigned int Pacman::getLives()const { return mLives; }

void Pacman::resetPosition()
{
	mCurrentColumn = mIntialColumn;
	mCurrentRow = mIntialRow;
	mShape.setPosition(mPositionOnWindow + mCurrentColumn * mSize, mPositionOnWindow + mCurrentRow * mSize);
}

bool Pacman::isValidDirection(Direction d)
{//needs to check out of range for vector
	if (mBoard == nullptr)
		return false;
	if (d == UP && mBoard->getBoard()[mCurrentRow - 1][mCurrentColumn] == -1)
		return false;
	if (d == DOWN && mBoard->getBoard()[mCurrentRow +1][mCurrentColumn] == -1)
		return false;
	if (d == RIGHT && mBoard->getBoard()[mCurrentRow ][mCurrentColumn+1] == -1)
		return false;
	if (d == LEFT && mBoard->getBoard()[mCurrentRow ][mCurrentColumn-1] == -1)
		return false;
	return true;
}


