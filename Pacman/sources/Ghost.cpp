#include "Ghost.h"
#include "../design patterns/ISubject.h"
Ghost::Ghost(int intialRow, int intialColumn, float size, float position) :
		Character( intialRow,  intialColumn,  size,  position),mVulenrable(false),
	mIntialRow(intialRow),mIntialCol(intialColumn)
{}

Ghost& Ghost::setIntialRow(unsigned int row)
{
	mIntialRow = row;
	return*this;
}
Ghost& Ghost::setIntialCol(unsigned int col)
{
	mIntialCol = col;
	return *this;
}
unsigned int Ghost::getIntialRow()const { return mIntialRow; }
unsigned int Ghost::getIntialCol()const { return mIntialCol; }


Ghost& Ghost::setVulenrable(bool v)
{
	mVulenrable = v;
	return *this;
}
bool Ghost::getVulenrable()const { return mVulenrable; }

void Ghost::resetPosition()
{
	mCurrentColumn = mIntialCol;
	mCurrentRow = mIntialRow;
	updateShape();
	
}
void Ghost::die()
{
	resetPosition();
	setVulenrable(false);

}
void Ghost::toggleFreight()
{
	mVulenrable = (mVulenrable) ? false : true;
	//should change image here
}
void Ghost::update()
{
	toggleFreight();
}