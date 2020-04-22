#include "Pacman.h"
//random numbers
const int INTIAL_ROW = 23;
const int INTIAL_COL = 14;
Pacman::Pacman(int intialRow, int intialColumn, float size, float posWindow) :
	Character( intialRow,  intialColumn,  size,  posWindow),mIntialColumn(INTIAL_COL)
	,mIntialRow(INTIAL_ROW),mLives(3)
{
	
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