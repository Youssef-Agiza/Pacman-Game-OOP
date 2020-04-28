#pragma once
#include"SFML/Graphics.hpp"
#include <string>
#include <iostream>
#include <vector>
namespace PacmanCS {

	class Board
	{
	private:
		sf::Texture mWallTexture;
		sf::Texture mRoadTexture; //change this name later
		sf::RectangleShape** mShape;
		int** score;
		sf::Texture mPT;
		std::vector < std::vector<int>> mBoard;

		const float mTileSize;
		const float mPositionOnWindow;

		void intializeShape();
	public:
		Board(std::vector < std::vector<int>> , float tileSize,float posOnWindow);
		
		//setters
		Board& setRoadTexture(std::string fileName);
		Board& setWallTexture(std::string fileName);
		

		//getters
		float getPositionOneWindow()const;
		float getTileSize()const;
		const std::vector < std::vector<int>>& getBoard()const;
		/*int getNumOfRows()const;
		int getNumOfCols()const;*/


		void drawOnWindow(sf::RenderWindow& w);
		void updateTextures();

	};

}