#include "Character.h"



Character::Character(int intialRow, int intialColumn, float size, float posWindow) :
	mCurrentRow(intialRow), mCurrentColumn(intialColumn), mSize(size), mPositionOnWindow(posWindow),
	mAlive(true) ,mDirection(STOP), mBoard(nullptr),mTexture(nullptr)
{
	mSprite.setPosition(mPositionOnWindow + mCurrentColumn * mSize, mPositionOnWindow + mCurrentRow * mSize);
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
const Texture& Character::getTexture()const { return *mTexture; }

//setters
Character& Character::setSize(float s){mSize = s;	return *this;}

Character& Character::setPosition(float position){	mPositionOnWindow = position; return *this; }
Character& Character::setAlive(bool status) {mAlive = status; return *this;}
Character& Character::setRow(unsigned int row) {	mCurrentRow = row; return *this;}
Character& Character::setCol(unsigned int col) {	mCurrentColumn = col;	return *this;}
Character& Character::setDirection(Direction dir)
{if(checkDestination(dir))
	mDirection = dir;
	return*this;
}

Character& Character::setBoard(Board* board){ mBoard = board;	return *this;}


Character& Character::setTexture(std::string file, float imagesPerRow, float imagesPerCol)
{
	if (mTexture != nullptr)
		delete mTexture;

	mTexture = new Texture;

	if (mTexture->loadFromFile(file))
	{
		mSprite.setTexture(*mTexture);
		adjustScale(imagesPerRow, imagesPerCol);

	}
	return *this;
}

void Character::adjustScale(float imagesPerRow,float imagesPerCol)
{
	if (mTexture == nullptr)
		return;

	sf::Vector2u v = mTexture->getSize();
	mSprite.setTextureRect(sf::IntRect(0, 0, v.x / imagesPerRow, v.y / imagesPerCol));
	mSprite.setScale((mSize * imagesPerRow) / v.x, (mSize * imagesPerCol) / v.y);
}
/*End of setters*/



int Character::checkDestination(Direction d)const
{
	vector<vector<int>> board = mBoard->getBoard();


	if (d == UP)
		return (board[mCurrentRow - 1][mCurrentColumn] == -1) ? 0 : 1;
	if (d == DOWN)
		return (board[mCurrentRow + 1][mCurrentColumn] == -1) ? 0 : 1;
	if (d == LEFT)
	{
		if (mCurrentColumn - 1 == -1) //left portal
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

void Character::drawOnWindow(RenderWindow& window)
{
	window.draw(mSprite);
}
void Character::updateShape()
{
	
	mSprite.setPosition(mPositionOnWindow + mCurrentColumn * mSize, mPositionOnWindow + mCurrentRow * mSize);
}

