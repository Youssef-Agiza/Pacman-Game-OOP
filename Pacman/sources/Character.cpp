#include "Character.h"

Character::Character(int intialRow, int intialColumn, float size, float posWindow) :
	mCurrentRow(intialRow), mCurrentColumn(intialColumn), mSize(size), mPositionOnWindow(posWindow),
	mAlive(true), mSpeed(1), mDirection(STOP), mBoard(nullptr)
{
	mShape.setSize(Vector2f(mSize, mSize));
	mShape.setPosition(mPositionOnWindow + mCurrentColumn * mSize, mPositionOnWindow + mCurrentRow * mSize);
}

Character::~Character()
{
	mBoard = nullptr;
}

//getters
bool Character::isAlive()const { return mAlive; }
int Character::getDirection()const { return mDirection; }
unsigned int Character::getRow()const { return mCurrentRow; }
unsigned int Character::getCol()const { return mCurrentColumn; }
float Character::getSize()const { return mSize; }
float Character::getPosition()const { return mPositionOnWindow; }
const Texture& Character::getTexture()const { return mTexture; }
float Character::getSpeed()const { return mSpeed; }


//setters
Character& Character::setSize(float s)
{
	mSize = s;
	return *this;
}
Character& Character::setPosition(float position)
{
	mPositionOnWindow = position;
	return *this;
}
Character& Character::setAlive(bool status)
{
	mAlive = status;
	return *this;
}
Character& Character::setRow(unsigned int row)
{
	mCurrentRow = row;

	return *this;
}
Character& Character::setCol(unsigned int col)
{
	mCurrentColumn = col;
	return *this;

}
Character& Character::setDirection(Direction dir)
{if(checkDestination(dir))
	mDirection = dir;
	return*this;
}
Character& Character::setTexture(std::string file)
{
	if (mTexture.loadFromFile(file))
		mShape.setTexture(&(this->mTexture));
	else
		mShape.setFillColor(Color::White);
	return *this;
}
Character& Character::setSpeed(float s)
{
	mSpeed = s;
	return *this;
}
Character& Character::setBoard(Board* board)
{
	mBoard = board;
	return *this;
}

void Character::resetPosition() {/*nothing*/ }
int Character::checkDestination(Direction d)const
{
	vector<vector<int>> board = mBoard->getBoard();


	if (d == UP)
		return (board[mCurrentRow - 1][mCurrentColumn] == -1) ? 0 : 1;
	if (d == DOWN)
		return (board[mCurrentRow + 1][mCurrentColumn] == -1) ? 0 : 1;
	if (d == LEFT)
	{
		if (mCurrentColumn - 1 == 0) //left portal
			return 2;
		return (board[mCurrentRow][mCurrentColumn - 1] == -1) ? 0 : 1;
	}
	
	if (d == RIGHT)
	{
		if (mCurrentColumn + 1 == board[mCurrentRow].size()) //right portal
			return 2;
		return (board[mCurrentRow][mCurrentColumn + 1] == -1) ? 0 : 1;
	}
	return 0;
}
void Character::move()
{
	if (checkDestination(mDirection) == 0)
		return;

	switch (mDirection)
	{
	case UP:
	{
		mCurrentRow--;
		mShape.move(0, -mSize * mSpeed);
		break; 
	}
	case DOWN:
	{	
	mCurrentRow++;
	mShape.move(0, mSize * mSpeed);
	break; }
	case LEFT:
	{
		if (checkDestination(mDirection)==2) //portal
		{
			mCurrentColumn = mBoard->getBoard()[mCurrentRow].size()-1;
			updateShape();
			return;
		}
		
		mCurrentColumn--;
		mShape.move(-mSize * mSpeed, 0);
		return;
	}
	case RIGHT: {
		if(checkDestination(mDirection)==2)//portal
		{
			mCurrentColumn = 1;
			updateShape();
			return;
		}
		
		mCurrentColumn++;
		mShape.move(mSize * mSpeed, 0);
		return;
	}
	default:break;
	}

}
void Character::drawOnWindow(RenderWindow& window)
{
	//updateShape();
	window.draw(mShape);
}
void Character::updateShape()
{
	
	mShape.setPosition(mPositionOnWindow + mCurrentColumn * mSize, mPositionOnWindow + mCurrentRow * mSize);
}

