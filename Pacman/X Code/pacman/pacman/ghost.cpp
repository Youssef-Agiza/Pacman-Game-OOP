//
//  ghost.cpp
//  character
//
//  Created by Nada Mohamed  on 4/12/20.
//  Copyright © 2020 Nada Mohamed . All rights reserved.
//

#include "ghost.hpp"
#include "pacman.cpp"

class Ghost:public pacman{
    
protected:
    
    
public:
    Ghost( int intialRow, int intialColumn, string fileName);
    void Move();
    bool eaten();
    
};
