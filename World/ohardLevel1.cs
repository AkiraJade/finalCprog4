import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class hardLevel1 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class ohardLevel1 extends World
{

    /**
     * Constructor for objects of class hardLevel1.
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
    Platform p2;
    boolean didJump = false;
    PlatformState p2UpState = PlatformState.WAITING;
    SpikeState sp1State = SpikeState.CREATED;
    Spike sp1;
    Spike sp2;
    SpikeState sp2State = SpikeState.CREATED;
    Platform p4;
    Platform p6;
    Spike sp3;
    Spike sp4;
    PlatformState p6UpState = PlatformState.WAITING;  
    PlatformState p4UpState = PlatformState.WAITING;
    SpikeState sp4State = SpikeState.READY;
    SpikeState sp3State = SpikeState.CREATED;
    public ohardLevel1()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
       super(750, 400, 1); 
       prepare();
    }
    
    public void prepare(){
        Person player1 = new Person();
        addObject(player1, 10, 280);
        Platform p1 = new Platform(100,100);
        addObject(p1, 30, 350);
        p2 = new Platform(25,150);
        addObject(p2, 140, 375);
        Platform p3 = new Platform(100, 100);
        addObject(p3, 178, 350);
        p4 = new Platform(25,150);
        addObject(p4, 300, 375);
        Platform p5 = new Platform(100, 100);
        addObject(p5, 338, 350);
        p6 = new Platform(25,100);
        addObject(p6, 462, 375);
        Platform p7 = new Platform(100,100);
        addObject(p7, 500, 350);
        Platform p8 = new Platform(180, 100);
        addObject(p8,700, 350);
        hardPortal hPor = new hardPortal(1);
        addObject(hPor, 705, 265);
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
    
    public void trapsV1(){
      if(playerX >= 80 && playerY <= 190 
      && p2UpState != PlatformState.STOPPED && !didJump){
          p2UpState = PlatformState.MOVING;
      }
      
      if(p2UpState == PlatformState.MOVING && !didJump){
          p2.setLocation(p2.getX(), p2.getY() - 20);
          
          if(p2.getY() <= 240){
              p2UpState = PlatformState.STOPPED;
              didJump = true;
          }
      }
      
      if(didJump && p2UpState == PlatformState.STOPPED
      && playerX <= 80 && playerY == 281){
          p2UpState = PlatformState.MOVING;
      }
      
      if(didJump && p2UpState == PlatformState.MOVING){
          p2.setLocation(p2.getX(), p2.getY() + 40);
          if(p2.getY() >= 399){
              p2UpState = PlatformState.STOPPED;
              didJump = false;
          }
      }
      
      if(playerX >= 150 && sp1State == SpikeState.CREATED){
        sp1State = SpikeState.READY;
      }
      
      if(sp1State == SpikeState.READY && sp1State != SpikeState.APPEAR){
        sp1 = new Spike();
        addObject(sp1, 190, 292);
        sp1State = SpikeState.APPEAR;
      }
      
      
    }
    boolean didJump2 = false;
    public void trapsV2(){
        if(playerX >= 250 && playerY <= 190
        && p4UpState != PlatformState.STOPPED && !didJump2){
            p4UpState = PlatformState.MOVING;
        }
         if(p4UpState == PlatformState.MOVING && !didJump2){
          p4.setLocation(p4.getX(), p4.getY() - 20);
          
          if(p4.getY() <= 240){
              p4UpState = PlatformState.STOPPED;
              didJump2 = true;
          }
      }
      
        if(didJump2 && p4UpState == PlatformState.STOPPED
      && playerX <= 230 && playerY == 281){
          p4UpState = PlatformState.MOVING;
      }
      
      if(didJump2 && p4UpState == PlatformState.MOVING){
          p4.setLocation(p4.getX(), p4.getY() + 40);
          if(p4.getY() >= 399){
              p4UpState = PlatformState.STOPPED;
              didJump2 = false;
          }
      }
      
      
         if(playerX >= 320 && sp2State == SpikeState.CREATED){
        sp2State = SpikeState.READY;
      }
      
      if(sp2State == SpikeState.READY && sp2State != SpikeState.APPEAR){
       sp2 = new Spike();
        addObject(sp2, 355, 292);
        sp2State = SpikeState.APPEAR;

      }
    }
    
    boolean readyHorizontal = false;
  
    public void trapsV3(){
        if (playerX >= 490 && sp3State == SpikeState.CREATED) {
            sp3State = SpikeState.READY;
        }
        
        if(sp3State == SpikeState.READY && sp3State != SpikeState.APPEAR){
        sp3= new Spike();
        addObject(sp3, 525, 292);
        sp3State = SpikeState.APPEAR;
        }
        
        if(sp3State == SpikeState.APPEAR 
        && p6UpState == PlatformState.WAITING){
            p6UpState = PlatformState.MOVING;
        }
        
        if(p6UpState == PlatformState.MOVING && !readyHorizontal){
             p6.setLocation(p6.getX(), p6.getY() - 20);
          
          if(p6.getY() <= 280){
              p6UpState = PlatformState.STOPPED;
              readyHorizontal = true;
          }
        }
        
        if(p6UpState == PlatformState.STOPPED && readyHorizontal ) {
            p6UpState = PlatformState.MOVING;
        } else if(p6UpState == PlatformState.MOVING && p6.getY() <= 275){
             p6.setLocation(p6.getX() + 10, p6.getY());
             if(p6.getX() >= 530){
                 p6UpState = PlatformState.STOPPED;
                 readyHorizontal = false;
             }
        } else if(p6UpState == PlatformState.STOPPED && !readyHorizontal){
            p6.setLocation(p6.getX(), p6.getY() + 20);
            removeObject(sp3);
        }
        
        if(playerX >= 570 && playerY <= 250 
        && sp4State == SpikeState.READY){
        sp4 = new Spike();
        addObject (sp4, 625, 292);
        sp4State = SpikeState.APPEAR;
        } else if (sp4State == SpikeState.APPEAR && playerX <= 550){
             removeObject(sp4);
        }
        
        
    }
    
}