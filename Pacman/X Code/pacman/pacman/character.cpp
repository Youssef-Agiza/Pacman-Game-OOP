			#include "character.hpp"
const int DEFAULT_DISTANCE=20;

Character::Character(int intialRow, int intialColumn, string imageFile):mIntialRow(intialRow),mIntialColumn(intialColumn),mCurrentRow()//complete the rest
{//requires validation
    mTextures.loadFromFile(imageFile);
    mShape=nullptr;
    
}
void Character::Move(char direction)//add the size of the array
{
    if (direction == 'U' || direction == 'u')
        mShape->move(0, -DEFAULT_DISTANCE);
    else if (direction == 'D' || direction == 'd')
        mShape->move(0, DEFAULT_DISTANCE);
    else if (direction == 'R' || direction == 'r')
        mShape->move(DEFAULT_DISTANCE, 0);
    else if (direction == 'L' || direction == 'l')
        mShape->move(-DEFAULT_DISTANCE, 0);
}
