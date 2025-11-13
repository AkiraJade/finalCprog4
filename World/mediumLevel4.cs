import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class mediumLevel4 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class mediumLevel4 extends World
{

    /**
     * Constructor for objects of class mediumLevel4.
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
    Platform p3;
    Platform p4;
    Platform p5Push;
    Platform p7Push;
    Spike sp1;
    SpikeState sp1State = SpikeState.CREATED;
    PlatformState p3State = PlatformState.WAITING;
    PlatformState p5PushState = PlatformState.WAITING;
    PlatformState p7PushState = PlatformState.WAITING;
    SpikeState sp2State =SpikeState.NOT_CREATED;
    Spike sp2;
    Platform p6;
    Platform p7;
 
    public mediumLevel4()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(750, 400, 1); 
        prepare();
    }
    
    public void prepare(){
        Person player1 = new Person();
        addObject(player1, 10, 150);
        Platform p1 = new Platform(150, 230);
        addObject(p1, 0, 285);
        PlatformTrap pt1 = new PlatformTrap();
        addObject(pt1, 110, 350);
        Platform p2 = new Platform(70, 150);
        addObject(p2, 150, 330);
        p3 = new Platform(150, 100);
        addObject(p3, 330, 350);
        p4 = new Platform(100, 100);
        addObject(p4, 550, 350);
        p5Push = new Platform (25, 150);
        addObject(p5Push, 290, 399);
      
        p6 = new Platform (100, 150);
        addObject(p6, 700, 330);
         p7Push = new Platform (25, 150);
        addObject(p7Push, 512, 399);
        mediumPortal mPor = new mediumPortal(4);
        addObject(mPor, 708, 223);
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
    boolean firstMove = false;
    public void trapsV1(){
        if(playerX >= 30){
            Spike sp1 = new Spike();
            addObject(sp1, 57, 162);
        }
        
        if(playerX >= 80 && playerY <= 180 && sp1State == SpikeState.CREATED){
        sp1 = new Spike();
        addObject(sp1, 130, 247);
        sp1State = SpikeState.READY;
        }
        
        if(sp1State == SpikeState.READY && playerY >= 200){
            sp1State = SpikeState.MOVING;
        }
        
        if(sp1State == SpikeState.MOVING && !firstMove){
            sp1.setLocation(sp1.getX() + 5, sp1.getY());
            
            if(sp1.getX() >= 150) {
                sp1State = SpikeState.STOPPED;
                
            }
        }
        
        if(playerX >= 195 && playerY <= 281 && p3State == PlatformState.WAITING){
            p3State = PlatformState.MOVING;
        }
        
        if (p3State == PlatformState.MOVING && !firstMove){
            p3.setLocation(p3.getX() + 5, p3.getY());
            if (p3.getX() >= 350){
                p3State= PlatformState.STOPPED;
                firstMove = true;
            }
        }
        
        
        if(playerX <= 185 && p3State == PlatformState.STOPPED
        && p3.getX() >= 330){
            p3State = PlatformState.MOVING;
            
        }
        
        if(firstMove && p3State == PlatformState.MOVING){
             p3.setLocation(p3.getX() - 5, p3.getY());
             
             if(p3.getX() <= 330){
                 p3State = PlatformState.STOPPED;
             }
        }
        
    
        
    }
    
    boolean verticalP5 = false;
    boolean horizontalP5 = false;
    boolean verticalP5v2 = false;
    
    public void trapsV2(){
        if(playerX >= 360 && p5PushState == PlatformState.WAITING){
            p5PushState = PlatformState.MOVING;
        }
        
        if(!verticalP5 && p5PushState == PlatformState.MOVING){
            p5Push.setLocation(p5Push.getX(), p5Push.getY() - 10);
            
            if(p5Push.getY() <= 230) {
                p5PushState = PlatformState.STOPPED;
                verticalP5 = true;
            }
        }
        
        if (!horizontalP5 && p5PushState == PlatformState.STOPPED){
            p5PushState = PlatformState.MOVING;
            horizontalP5 = true;
        }
        
        if(!verticalP5v2 && horizontalP5 && p5PushState == PlatformState.MOVING){
             p5Push.setLocation(p5Push.getX() + 10, p5Push.getY());
             if(p5Push.getX() >= 512){
                 p5PushState = PlatformState.STOPPED;
             }
        }
        
        if(p5PushState == PlatformState.STOPPED &&
        p5Push.getX() >= 512){
            p5PushState = PlatformState.MOVING;
            verticalP5v2 = true;
        }
        
        if(verticalP5v2 && p5PushState == PlatformState.MOVING){
            p5Push.setLocation(p5Push.getX(), p5Push.getY() + 5);
            if(p5Push.getY() >= 399){
                p5PushState = PlatformState.STOPPED;
                  sp2 = new Spike();
                  addObject(sp2, 570, 292);
                  sp2State = SpikeState.CREATED;
            }
        }
        
        
        
    }
    
    boolean verticalP7 = false;
    boolean horizontalP7 = false;
    public void trapsV3(){
        if(playerX <= 540 && p7PushState == PlatformState.WAITING
        && sp2State == SpikeState.CREATED){
            p7PushState = PlatformState.MOVING;
        }
        
        if(!verticalP7 && p7PushState == PlatformState.MOVING){
            p7Push.setLocation(p7Push.getX(), p7Push.getY() - 40);
            if(p7Push.getY() <= 240) {
                p7PushState = PlatformState.STOPPED;
                verticalP7 = true;
            }
        }
        
        if(verticalP7 &&  p7PushState == PlatformState.STOPPED){
            p7PushState = PlatformState.MOVING;
        }
        
        if(!horizontalP7 && verticalP7 
        && p7PushState == PlatformState.MOVING){
            p7Push.setLocation(p7Push.getX() + 2, p7Push.getY());
              if(p7Push.getX() >= 610) {
                p7PushState = PlatformState.STOPPED;
                horizontalP7 = true;
            }
        }
        
        
    }
    
}