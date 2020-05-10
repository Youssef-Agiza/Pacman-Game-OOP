#pragma once
#include <cstddef>
#include<vector>
#include "../headers/Pacman.h"
#include "../headers/Audio.h"
#include"SFML/Graphics.hpp"
using namespace std;
using namespace sf;


class Pellets
{
private:
	vector <vector<int>> arrScore;
	Texture mPelletTexture;
	Texture mPowerTexture;
	Texture mBlack;
	Texture mTransparent;
	Texture mFruit;
	void addFruit();
public:
	Pellets(vector <vector<int>> v);
	void drawPellets(RenderWindow &w, RectangleShape** mShape);
	int mPelletCount;
	void intersectPellets(Pacman* P);
	~Pellets();
};

