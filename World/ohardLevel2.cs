import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class ohardLevel2 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class ohardLevel2 extends World
{

    /**
     * Constructor for objects of class ohardLevel2.
     * 
     */
    
    enum PlatformState {
        WAITING,      // Platform hasn't started moving yet
        MOVING,       // Platform is moving up
        STOPPED,      // Platform stopped at target position
        REMOVED,       // Platform has been removed from world
        DISAPPEAR,
        SPAWNED,
        APPEAR
    }
    
    enum SpikeState {
        NOT_CREATED,  // Spike doesn't exist yet
        CREATED,      // Spike exists but not ready to move
        READY,        // Player touched ground, spike ready to move
        MOVING,       // Spike is moving right
        STOPPED,       // Spike finished moving
        DISAPPEAR,
        APPEAR
    }
    
    int playerX;
    int playerY;
    Spike sp1;
    Spike sp2;
    Spike sp3;
    Spike sp4;
    Spike sp5;
    Platform p3Right;
    Platform p4Up;
    Platform p6Up;
    PlatformState p3RightState = PlatformState.WAITING;
    PlatformState p4UpState = PlatformState.WAITING;
    PlatformState p6UpState = PlatformState.WAITING;
    SpikeState sp1State = SpikeState.CREATED;
    SpikeState sp2State = SpikeState.CREATED;
    SpikeState sp3State = SpikeState.CREATED;
    SpikeState sp4State = SpikeState.CREATED;
    SpikeState sp5State = SpikeState.CREATED;
    public ohardLevel2()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
       super(750, 400, 1); 
       prepare();
    }
    
    public void prepare(){
        Person player1 = new Person();
        addObject(player1, 10, 180);
        Platform p1 = new Platform(100,200);
        addObject(p1, 50, 300);
        Platform p2 = new Platform(100,100);
        addObject(p2, 220, 360);
        sp2 = new Spike();
        addObject(sp2, 220, 302);
         p3Right = new Platform(80, 100);
         addObject(p3Right, 310, 360);
        p4Up = new Platform(25,150);
        addObject(p4Up, 342, 399);
        Platform p5 = new Platform(150,100);
        addObject(p5, 425, 360);
        Spike sp3 = new Spike();
        addObject(sp3, 380, 302);
        p6Up = new Platform(25,150);
        addObject(p6Up, 487, 399);
        Platform p7 = new Platform (50, 10);
        addObject(p7, 550, 270);
          Platform p8 = new Platform (50, 10);
        addObject(p8, 630, 230);
        Platform p9 = new Platform(100, 230);
        addObject(p9, 740, 290);
        hardPortal hPor = new hardPortal(2);
        addObject(hPor, 739, 139);
        pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
    }
    
    public void act(){
        updatePlayerPosition();
        trapsV1();
        trapsV2();
        trapsV3();
    }
    
      public void updatePlayerPosition(){
        java.util.List<Person> players = getObjects(Person.class);
        
        if(players.isEmpty()){
            return;
        }
        
        Person player = players.get(0);
        playerX = player.getX();
        playerY = player.getY();
    }
    
    boolean firstMovement = false;
    public void trapsV1(){
        
        //for spike appearance
        if(playerX >= 65 && sp1State != SpikeState.READY
        && sp1State != SpikeState.APPEAR){
            sp1State = SpikeState.READY;
        } else if(sp1State == SpikeState.READY 
        && sp1State != SpikeState.APPEAR){
             sp1 = new Spike();
             addObject(sp1, 85, 192);
             sp1State = SpikeState.APPEAR;
        } else if(sp1State == SpikeState.APPEAR && playerX <= 60){
            removeObject(sp1);
        }
        
        //for moving spike
        if(playerX >= 120 && playerY <= 140 && 
        sp2State == SpikeState.CREATED){
            sp2State = SpikeState.READY;
        } else if(sp2State == SpikeState.READY){
            sp2State = SpikeState.MOVING;
        } else if(sp2State == SpikeState.MOVING && !firstMovement){
            sp2.setLocation(sp2.getX() - 10, sp2.getY());
            
            if(sp2.getX() <= 190){
                sp2State = SpikeState.STOPPED;
               
            }
        } else if(sp2State == SpikeState.STOPPED 
        && playerX <= 100 && !firstMovement){
            sp2State = SpikeState.MOVING;
             firstMovement = true;
        } else if (firstMovement && sp2State == SpikeState.MOVING){
              sp2.setLocation(sp2.getX() + 10, sp2.getY());
              if(sp2.getX() >= 220){
                  sp2State = SpikeState.STOPPED;
                 
              }
        }
    }
    
    boolean goDown = false;
    boolean firstMovementV2 = false;
    
    public void trapsV2(){
        //movement and spike
        if(playerX >= 260 && p3RightState == PlatformState.WAITING){
            p3RightState = PlatformState.MOVING;
        } else if(p3RightState == PlatformState.MOVING){
            p3Right.setLocation(p3Right.getX() + 10, p3Right.getY());
            if(p3Right.getX() >= 370){
                p3RightState = PlatformState.STOPPED;
            }
        }
        if(playerX >= 260 && p4UpState == PlatformState.WAITING){
            p4UpState = PlatformState.MOVING;
        } else if(p4UpState == PlatformState.MOVING && !goDown){
            p4Up.setLocation(p4Up.getX(), p4Up.getY() - 20);
            if(p4Up.getY() <= 250){
                p4UpState = PlatformState.STOPPED;
                
            }
        } else if (p4UpState == PlatformState.STOPPED 
        && playerX <= 270 && !goDown){
            p4UpState = PlatformState.MOVING;
            goDown = true;
        } else if(p4UpState == PlatformState.MOVING && goDown){
            p4Up.setLocation(p4Up.getX(), p4Up.getY() + 20);
            if(p4Up.getY() >= 399){
                p4UpState = PlatformState.STOPPED;
                
            }
        }
        
        //movement to spike 410
        
        if(playerX >= 440 && p6UpState == PlatformState.WAITING){
            p6UpState = PlatformState.MOVING;
        } else if (p6UpState == PlatformState.MOVING && !firstMovementV2) {
            p6Up.setLocation(p6Up.getX(), p6Up.getY() - 20);
            if (p6Up.getY() <= 240) {
                p6UpState = PlatformState.STOPPED;
                
            }
        } else if(p6UpState == PlatformState.STOPPED && !firstMovementV2){
            p6UpState = PlatformState.MOVING;
            firstMovementV2 = true;
        } else if(firstMovementV2 && p6UpState == PlatformState.MOVING){
             p6Up.setLocation(p6Up.getX() - 20, p6Up.getY());
            if (p6Up.getX() <= 410) {
                p6UpState = PlatformState.STOPPED;
                
            }
        } else if(p6UpState == PlatformState.STOPPED && firstMovementV2){
             p6Up.setLocation(p6Up.getX(), p6Up.getY() + 20) ;
          
        }
    
    }
    
    public void trapsV3(){
        
        if(playerX >= 520 && playerY <= 230 && sp3State == SpikeState.CREATED){
              sp3State = SpikeState.READY;
       
       //190 588
    } else if(sp3State == SpikeState.READY){
        sp3 = new Spike();
        addObject(sp3, 540, 257);
        sp3State = SpikeState.APPEAR;
    } else if(sp3State == SpikeState.APPEAR && playerY == 291 
    && playerX <= 500){
        removeObject(sp3);
    }
    
    if(playerX >= 588 && playerY <= 190 && sp4State == SpikeState.CREATED) {
        sp4State = SpikeState.READY;
    } else if(sp4State == SpikeState.READY){
         sp4 = new Spike();
       addObject(sp4, 620, 217);
       sp4State = SpikeState.APPEAR;
    } else if(sp4State == SpikeState.APPEAR && playerY == 246 &&
    playerX <= 575){
        removeObject(sp4);
    }
    
       if(playerX >= 670 && playerY <= 130 && sp5State == SpikeState.CREATED) {
        sp5State = SpikeState.READY;
    } else if(sp5State == SpikeState.READY){
        sp5 = new Spike();
       addObject(sp5, 705, 167);
       sp5State = SpikeState.APPEAR;
    } else if(sp5State == SpikeState.APPEAR && playerY == 206 &&
    playerX <= 650){
        removeObject(sp5);
    }
    
    
    }
}