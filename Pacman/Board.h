#pragma once
#include"SFML/Graphics.hpp"
#include <string>
#include <iostream>
namespace PacmanCS {

	class Board
	{
	private:
		sf::Texture mWallTexture;
		sf::Texture mRoadTexture; //change this name later
		sf::RectangleShape** mShape;
		const int mRows;
		const int mCols;
		const float mBlockSize;
		const float mPositionOnWindow;
	public:
		Board(int rows, int cols, float block,float position,std::string wall, std::string road = "");
		void drawOnWindow(sf::RenderWindow& w);
		void setTextures( int** arr);

	};

}