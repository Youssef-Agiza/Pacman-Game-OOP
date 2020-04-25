#pragma once
#include "Character.h"
#include "Board.h"

class Ghost :public Character {

private:
    unsigned int mIntialRow;
    unsigned int mIntialCol;
    bool mVulenrable; //freight mood

public:
    Ghost(int intialRow, int intialColumn, int size, int position);
  
    Ghost& setIntialRow(unsigned int row);
    Ghost& setIntialCol(unsigned int col);
    unsigned int getIntialRow()const;
    unsigned int getIntialCol()const;

    Ghost& setVulenrable(bool v);
    bool getVulenrable()const;

    void resetPosition()override;
        
};

//
////bool eaten();
//  //bool dead();
//  //bool isAtCenter();
//  //bool frightMode();
//  //bool scatterMode();
//  //bool chaseMode();
////    vector<int >moveGhost;
//
//Ghost::Ghost(int intialRow, int intialColumn, int size, int  position, string imageFile = "") :Character{ intialRow, intialColumn,  size,  position, imageFile }
//{
//    mTextures.loadFromFile(imageFile);
//
//    Inky = new Sprite(mTextures, IntRect(23, 45, 12, 9));
//    Pinky = new Sprite(mTextures, IntRect(23, 45, 12, 9));
//    Blinky = new Sprite(mTextures, IntRect(23, 45, 12, 9));
//    Clyde = new Sprite(mTextures, IntRect(23, 45, 12, 9));
//
//    mShape.setRadius(mSize / 2);
//    mShape.setPosition(mCurrentRow + 14 * intialRow, mCurrentRow + 20 * intialColumn);
//    Inky->setScale(4, 4);
//    Blinky->setScale(4, 4); //any random numbers
//    Pinky->setScale(4, 4); //any random numbers
//    Clyde->setScale(4, 4); //any random numbers
//
//
//}
//
////setter function
//void Ghost::setDir(int dir) {
//
//
//
//}
//bool Ghost::isAtCenter()
//{
//    return (int)(mIntialRow * 10.0f) % 10 == 0 && (int)(mIntialColumn * 10.0f) % 10 == 0;
//}
////switch on every sprite
//
//void Ghost::move() {
//
//    if (!pinky->scatterMode())
//        switch (Pacman->getDis()) {
//        case sigaction::Up:
//            Pinky->
//                break;
//
//        default:
//            break;
//        }
//
//}
//
//using namespace PacmanCS;
//
//int main()
//{
//
//    const int ROWS = 31, COLUMNS = 28, BLOCK = 30;
//    const int POSITION = 25;
//    RenderWindow window;
//    window.create(VideoMode(1000, 1000), "Simple Maze");
//
//    int** arr;
//    arr = new int* [ROWS];
//
//    for (int i = 0; i < ROWS; i++)
//    {
//        arr[i] = new int[COLUMNS];
//    }
//
//    ifstream inputFile;
//    inputFile.open("BoardText2.txt");
//    if (inputFile.is_open())
//    {
//        for (int i = 0; i < ROWS; i++)
//            for (int j = 0; j < COLUMNS; j++)
//                if (!inputFile.eof())
//                    inputFile >> arr[i][j];
//    }
//    inputFile.close();
//    Texture block, grass;
//    block.loadFromFile("Block.png");
//    grass.loadFromFile("grass_tiled.png");
//    Board myBoard(ROWS, COLUMNS, BLOCK, POSITION, "Block.png", "grass_tiled.png");
//    myBoard.setTextures(arr);
//
//    Pacman pac(23, 30, 50, POSITION, "pacman.png");
//    Ghost pinky(32, 20, 50, POSITION, "pinky,png");
//    Ghost inky(31, 21, 50, POSITION, "inky,png");
//    Ghost clyde(30, 22, 50, POSITION, "clyde,png");
//    Ghost blinky(29, 21, 50, POSITION, "blinky,png");
//
//    Event e;
//    while (window.isOpen())
//    {
//        while (window.pollEvent(e))
//        {
//            if (e.type == Event::Closed)
//                window.close();
//            else if ((e).type == Event::KeyPressed)
//            {
//                switch ((e).key.code)
//                {
//                case Keyboard::Up:
//                    pac.Move('U', arr);
//                    break;
//                case Keyboard::Down:
//                    pac.Move('D', arr);
//                    break;
//                case Keyboard::Right:
//                    pac.Move('R', arr);
//                    break;
//                case Keyboard::Left:
//                    pac.Move('L', arr);
//                    break;
//
//                }
//
//            }
//        }
//    }
//
//    //void Ghost::drawOnWindow(RenderWindow &window)
//    //    {
//    //        window.draw(*Ghost::Inky);
//    //        window.draw(*Ghost::Pinky);
//    //        window.draw(*Ghost::Blinky);
//    //        window.draw(*Ghost::Clyde);
//    //
//    //
//    //    }
//    ////
//    //        vector<int>moveGhost(Ghost ghost)={
//    //
//    //
//    //           Vector<int> v:
//    //            switch(moveType) {
//    //                case CHASE:
//    //                            switch (Ghostchar) {
//    //                                case blinky:
//    //                                    v.push_back(pac.getrow());
//    //                                    v.push_back(pac.getColumn());
//    //                                    break;
//    //                                case blinky:
//    //                                    switch(pac.currentDir) {
//    //                                        case LEFT:
//    //                                            v.push_back(pac.getrow() - 2);
//    //                                            v.push_back(pac.getColumn());
//    //                                            break;
//    //                                        case UP:
//    //                                            v.push_back(pac.getrow() );
//    //                                            v.push_back(pac.getColumn() + 2);
//    //                                            break;
//    //                                        case RIGHT:
//    //                                            v.push_back(pac.getrow() + 2);
//    //                                            v.push_back(pac.getColumn());
//    //                                            break;
//    //                                        case DOWN:
//    //                                            v.push_back(pac.getrow());
//    //                                            v.push_back(pac.getColumn() - 2);
//    //                                            break;
//    //                                        default:
//    //                                            v.push_back(pac.getrow());
//    //                                            v.push_back(pac.getColumn());
//    //                                    }
//    //                                    break;
//    //                                case clyde:
//    //                                    v.push_back(0);
//    //                                    v.push_back(0);
//    //                                    break;
//    //                                case pinky:
//    //                                    switch(pac.currentDir) {
//    //                                        case LEFT:
//    //                                            v.push_back(pac.getrow() - 4);
//    //                                            v.push_back(pac.getColumn());
//    //                                            break;
//    //                                        case UP:
//    //                                            v.push_back(pac.getrow() );
//    //                                            v.push_back(pac.getPacY() + 4);
//    //                                            break;
//    //                                        case RIGHT:
//    //                                            v.push_back(pac.getrow() + 4);
//    //                                            v.push_back(pac.getPacY());
//    //                                            break;
//    //                                        case DOWN:
//    //                                            v.push_back(pac.getrow());
//    //                                            v.push_back(pac.getPacY() - 4);
//    //                                            break;
//    //                                        default:
//    //                                            v.push_back(pac.getrow());
//    //                                            v.push_back(pac.getPacY());
//    //                                    }
//    //                                    break;
//    //                            }
//    //                    break;
//    //                case SCATTER:
//    //                        switch (ghostColour) {
//    //                            case RED:
//    //                                v.push_back(100);
//    //                                v.push_back(100);
//    //                                break;
//    //                            case BLUE:
//    //                                v.push_back(20);
//    //                                v.push_back(0);
//    //                                break;
//    //                            case YELLOW:
//    //                                v.push_back(0);
//    //                                v.push_back(0);
//    //                                break;
//    //                            case PINK:
//    //                                v.push_back(0);
//    //                                v.push_back(100);
//    //                                break;
//    //                        }
//    //
//    //            return v;
//    //        }
//    //
//    //    };
//    //
//
//    window.clear();
//
//    myBoard.drawOnWindow(window);
//    pac.drawOnWindow(window);
//    pinky.drawOnWindow(window);
//    inky.drawOnWindow(window);
//    clyde.drawOnWindow(window);
//    blinky.drawOnWindow(window);
//    window.display();
//
//
//
//
//    return EXIT_SUCCESS;
//}
