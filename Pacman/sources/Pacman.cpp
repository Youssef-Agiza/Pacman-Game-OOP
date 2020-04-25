#include "Pacman.h"
//random numbers
#define INTIAL_ROW  23
#define INTIAL_COL  14
Pacman::Pacman(int intialRow, int intialColumn, float size, float posWindow) :
	Character( intialRow,  intialColumn,  size,  posWindow),mLives(3)
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
	mCurrentColumn = INTIAL_COL;
	mCurrentRow = INTIAL_ROW;
	mShape.setPosition(mPositionOnWindow + mCurrentColumn * mSize, mPositionOnWindow + mCurrentRow * mSize);
}

