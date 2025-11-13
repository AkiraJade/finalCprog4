import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class mediumLevel5 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class mediumLevel5 extends World
{

    /**
     * Constructor for objects of class mediumLevel5.
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
     Platform p1;
     Platform p2Moving;
     Platform p3;
     Platform p4;
     Platform p5;
     Platform p6;
     Platform p7;
     Platform p8;
     Platform p9;
     Platform p10;
    PlatformState p1State = PlatformState.WAITING;
    PlatformState p2State = PlatformState.WAITING;
    public mediumLevel5()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(750, 400, 1); 
        prepare();
    }
    
    public void prepare(){
        Person player1 = new Person();
        addObject(player1, 10, 180);
       p1 = new Platform(70, 135);
        addObject(p1, 35, 265);
        PlatformTrap pt1 = new PlatformTrap();
        addObject(pt1, 35, 380);
        p4 = new Platform(40, 10);
       addObject(p4, 280, 350);
       p5 = new Platform (30, 10);
       addObject(p5, 360, 370);
        p6 = new Platform(20, 100);
            addObject(p6, 430, 350);
        p7 = new Platform(30, 10);
        addObject(p7, 500, 370);
        p8 = new Platform(20, 100);
        addObject (p8,560, 340);
        p9 = new Platform(30, 10);
        addObject(p9, 650, 330);
         p10 = new Platform(50, 150);
        addObject (p10,740, 345);
        mediumPortal mPor = new mediumPortal(5);
        addObject(mPor, 747, 236);
        pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
    }
    
    public void act(){
       updatePlayerPosition(); 
       platformDown();
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
    
    
    boolean continuousMoving = false;
    boolean didJump = false;
    Spike sp2;
    boolean returned = false;
    int speed = 1;
    boolean addOne = false;
    public void platformDown(){
        if(playerX >= 45 && p1State == PlatformState.WAITING){
            p1State = PlatformState.MOVING;
        }
        
        if(p1State == PlatformState.MOVING){
            p1.setLocation(p1.getX(), p1.getY() +80);
            
            if(p1.getY() >= 399) {
                p2Moving = new Platform (25, 150);
                addObject(p2Moving, 0, 260);
                p1State = PlatformState.STOPPED;
                p2State = PlatformState.MOVING;

            }
        }
        
        if(playerY >= 312 && !continuousMoving){
            continuousMoving = true;
            p3 = new Platform(100, 100);
            addObject(p3, 170, 350);
        }
        if(p2State == PlatformState.MOVING && continuousMoving){
            if(p2Moving.getX() >= 400 && !addOne) {
            speed +=1;
            addOne = true;
        }
            p2Moving.setLocation(p2Moving.getX()+ speed, p2Moving.getY());
        }
        
        if(playerX >= 145){
            Spike sp1 = new Spike();
            addObject(sp1, 180, 292);
        }
        
        if(!didJump && !returned && playerY <= 180 && playerX >= 230){
            didJump = true;
        }
        
        if(didJump && !returned && sp2 == null){
             sp2 = new Spike();
            addObject(sp2, 280, 337);
        }
        
        
        if(didJump && playerX >= 200 && playerX < 230 && !returned){
            returned = true;
                 System.out.println(returned);
           
        }
    
        if (returned) {
            removeObject(sp2);
            sp2 = null;
        }
        
       
    }
}