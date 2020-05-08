#pragma once
#include "Common.h"
using namespace sf;
using namespace std;

class Words
{
private:
	Text T;
	Font font;
	Text Num;
	Text L;
	Text life;
public:
	Words()
	{
		font.loadFromFile("../boardTexts/font.ttf");
		T.setFont(font);
		T.setString("SCORE: ");
		T.setCharacterSize(50);
		T.setOutlineColor(Color::Yellow);
		T.setOutlineThickness(0.5);
		T.setOrigin(Vector2f(-1400, 0));
		Num.setFont(font);
		Num.setCharacterSize(50);
		Num.setOrigin(Vector2f(-1500, 0));
		L.setFont(font);
		L.setCharacterSize(50);
		L.setOrigin(Vector2f(-300, 0));
		L.setString("Lifes:");
		life.setFont(font);
		life.setCharacterSize(50);
		life.setOrigin(Vector2f(-400, 0));
	}
	void drawText(RenderWindow& w, int x, int score)
	{
		Num.setString(to_string(score));
		w.draw(T);
		w.draw(Num);
		w.draw(L);
		life.setString(to_string(x));
		w.draw(life);
	}
	

};

