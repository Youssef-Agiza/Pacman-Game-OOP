#pragma once
#include "../headers/Character.h"
#include "../headers/Board.h"
#include "../headers/Graph.h"
#include "../design patterns/IObserver.h"
class Ghost :
    public Character, public IObserver {

private:
    unsigned int mIntialRow;
    unsigned int mIntialCol;
    bool mFreight; 
    std::string mResource; //keeps resource for the texture
    Graph* mGraph;
    int homeTimer;
    bool inHome;
   

    
public:
    Ghost(int intialRow, int intialColumn, float size, Board* board);
    ~Ghost();
  
    unsigned int getIntialRow()const;
    unsigned int getIntialCol()const;
    const std::string& getResource()const;
    bool getFreight()const;


    Ghost& setIntialRow(unsigned int row);
    Ghost& setIntialCol(unsigned int col);
    Ghost& setFreight(bool f);
    Ghost& setResource(std::string resource);

    //Iobserver method
    void update(bool powerUp)override;
    
    // Inherited via Character
    virtual void resetPosition()override;
    virtual void die(sf::RenderWindow& w)override;
    virtual void animateMove() override;
    virtual void move()override;
    virtual Ghost& setDirection(Direction d)override;

    /*AI Functions*/
    void clyde(Pacman*);
    void blinky(Pacman*);    
    void pinky(Pacman*);
    void inky(Pacman*);
    void (Ghost::*ai)(Pacman*);
    void callAI(Pacman*);
    
    
    Ghost& setGraph(Graph* graph);
    Direction Path2Movement(std::list<int>* path); //utility function to convert graph shortest path to movements
    bool reverse(Direction d)const; //true if d is reverse direction of mDirection 


    //time functions
    int getHomeTimer()const;
    bool isInHome()const;
    Ghost& setHomeTimer(int htime);
    Ghost& setInHome(bool h);


};

