#include "../headers/GameManager.h"
#include "../headers/PlayerList.h"
using namespace std;
using namespace sf;
using namespace PacmanCS;
/************************************************************************/

/************************************************************************/

int main()
{
	

	PlayerList pl("../boardTexts/player.txt");
	pl.LoadFiletoList();
	for (auto p : pl.getList())
	{
		std::cout << p.email<<std::endl;
	}

	PlayerData p("yo", "aiwa", 12);
	pl.addPlayer(p);
	pl.loadListToFile();
	std::cout<<"Highest:"<< pl.getHighestScore().highScore;
GameManager Game;

//Game.startGame();
Game.Play();
return 0;
}
