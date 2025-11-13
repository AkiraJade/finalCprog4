import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)
/**
 * Write a description of class mediumLevel2 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class mediumLevel2 extends World
{
    /**
     * Constructor for objects of class mediumLevel2.
     * 
     */
    
   
    enum PlatformState {
        WAITING,      // Platform hasn't started moving yet
        MOVING,       // Platform is moving left
        STOPPED,      // Platform stopped at target position
        REMOVED,       // Platform has been removed from world
        DISAPPEAR,
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
    Platform p3Appearing;
    Spike sp1;
    Platform p4Moving;
    PlatformState p3State = PlatformState.WAITING;
    SpikeState spikeState = SpikeState.NOT_CREATED;
    PlatformState p4State = PlatformState.WAITING;
    PlatformState p5State = PlatformState.DISAPPEAR;
    SpikeState sp2State = SpikeState.DISAPPEAR;
    PlatformState p6State = PlatformState.APPEAR;
    SpikeState sp3State = SpikeState.APPEAR;
    Platform p5Disappearing;
    Spike sp2Disappearing;
    Platform p6Disappearing;
    Spike sp3Disappearing;
    Platform p7;
    Spike sp4;
    int transparencyHandler = 0;
    int transparencyHandler2 = 250;
    boolean isFading = false;
    
    public mediumLevel2()
    {    
        
        super(750, 400, 1); 
        prepare();
    }
    
    public void prepare(){
        Person player1 = new Person();
        addObject(player1, 30, 150);
        Platform p1 = new Platform(150, 230);
        addObject(p1, 0, 285);
        Platform p2 = new Platform(100, 100);
        addObject(p2, 200, 365);
        p3Appearing = new Platform(100, 100);
        addObject(p3Appearing, 200, 399);
        p4Moving = new Platform (50, 10);
        addObject(p4Moving, 225, 320);
        p5Disappearing = new Platform (50, 100);
        p5Disappearing.getImage().setTransparency(0);
        addObject(p5Disappearing, 400, 365);
        sp2Disappearing = new Spike();
        addObject (sp2Disappearing, 400, 307);
        sp2Disappearing.getImage().setTransparency(0);
        p6Disappearing = new Platform (50, 100);
        p6Disappearing.getImage().setTransparency(250);
        addObject(p6Disappearing, 550, 365);
        sp3Disappearing = new Spike();
        addObject (sp3Disappearing, 550, 307);
        sp3Disappearing.getImage().setTransparency(250);
         p7 = new Platform (80,100);
        addObject(p7, 715, 365);
        p7.getImage().setTransparency(0);
        sp4 = new Spike();
        addObject(sp4, 695, 307);
        sp4.getImage().setTransparency(0);
        mediumPortal mPor = new mediumPortal(2);
        addObject(mPor, 704, 270);
        pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
    }
    
    public void act(){
        updatePlayerPosition();
        handleSpike();
        handlePlatform();
        handlePlatformMovement();
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
    
    public void handleSpike(){
     
        if (playerX >= 135 && spikeState == SpikeState.NOT_CREATED){
            sp1 = new Spike();
            addObject(sp1, 165, 307);
            spikeState = SpikeState.CREATED;
        }
        
        
        if (spikeState == SpikeState.NOT_CREATED) return;
        
      
        if (spikeState == SpikeState.CREATED && playerY >= 300){
            p3State = PlatformState.MOVING;  
            spikeState = SpikeState.READY;
        }
        
       
        if (playerX >= 150 && playerY <= 260 && spikeState == SpikeState.READY){
            spikeState = SpikeState.MOVING;
        }
        
     
        if (spikeState == SpikeState.MOVING){
            sp1.setLocation(sp1.getX() + 10, sp1.getY());
            
           
            if (sp1.getX() >= 180){
                spikeState = SpikeState.STOPPED;
            }
        }
        
        if(playerY <= 210 && playerX >= 190 && sp1.getX() <= 215) {
            spikeState = SpikeState.READY;
         }
        
        if(playerX >= 190 && spikeState == SpikeState.READY) {
            spikeState = SpikeState.MOVING;
        }
        if(spikeState == SpikeState.MOVING){
            sp1.setLocation(sp1.getX() + 10, sp1.getY());
            
            if(sp1.getX() >=  215){
                spikeState = SpikeState.STOPPED;
            }
            
        }
        //250
        
    }
    
    public void handlePlatform(){
        
        if (p3State == PlatformState.MOVING){
            p3Appearing.setLocation(p3Appearing.getX() - 20, p3Appearing.getY());
            
        
            if (p3Appearing.getX() <= 120){
                p3State = PlatformState.STOPPED;
            }
        }
        
        if (spikeState == SpikeState.STOPPED && playerX <= 141 && playerY >= 330 && p3State != PlatformState.REMOVED){
            removeObject(p3Appearing);
            p3State = PlatformState.REMOVED;
        }
    }
    
    int transparencyHandler3 = 0;
    public void handlePlatformMovement(){
        if(playerX >= 248 && p4State == PlatformState.WAITING) {
            p4State = PlatformState.MOVING;
        }
        
        if (p4State == PlatformState.MOVING) {
            p4Moving.setLocation(p4Moving.getX() + 2, p4Moving.getY());
        }
        
       
        if(playerX >= 310 && sp2State == SpikeState.DISAPPEAR 
        && p5State == PlatformState.DISAPPEAR && !isFading) {
            p5State = PlatformState.APPEAR;
            sp2State = SpikeState.APPEAR;
            p6State = PlatformState.DISAPPEAR;
            sp3State = SpikeState.DISAPPEAR;
            transparencyHandler = 0;
            transparencyHandler2 = 250;
            isFading = true;
        }
        
        if(p6State == PlatformState.DISAPPEAR && sp3State == SpikeState.DISAPPEAR &&
        p5State == PlatformState.APPEAR && sp2State == SpikeState.APPEAR &&
        transparencyHandler < 250 ){
            p5Disappearing.getImage().setTransparency(transparencyHandler);
            sp2Disappearing.getImage().setTransparency(transparencyHandler);
            p6Disappearing.getImage().setTransparency(transparencyHandler2);
            sp3Disappearing.getImage().setTransparency(transparencyHandler2);
          
            transparencyHandler += 10;
            transparencyHandler2 -= 10;
        } else if(transparencyHandler >= 250 && p5State == PlatformState.APPEAR) {
            isFading = false;
        }
        
        
        if( playerX >= 460 && p6State == PlatformState.DISAPPEAR 
        && sp3State == SpikeState.DISAPPEAR && !isFading){
            p6State = PlatformState.APPEAR;
            sp3State = SpikeState.APPEAR;
            p5State = PlatformState.DISAPPEAR;
            sp2State = SpikeState.DISAPPEAR;
            transparencyHandler = 250;
            transparencyHandler2 = 0;
            isFading = true;
        }
        
        if(p5State == PlatformState.DISAPPEAR && sp2State == SpikeState.DISAPPEAR &&
        p6State == PlatformState.APPEAR && sp3State == SpikeState.APPEAR &&
        transparencyHandler > 0){
            p5Disappearing.getImage().setTransparency(transparencyHandler);
            sp2Disappearing.getImage().setTransparency(transparencyHandler);
            p6Disappearing.getImage().setTransparency(transparencyHandler2);
            sp3Disappearing.getImage().setTransparency(transparencyHandler2);
            transparencyHandler -= 10;
            transparencyHandler2 += 10;
        } else if(transparencyHandler <= 0 && p6State == PlatformState.APPEAR) {
            isFading = false;
        }
        
        if(playerX >= 650 && transparencyHandler3 < 250){
                p7.getImage().setTransparency(transparencyHandler3);
                sp4.getImage().setTransparency(transparencyHandler3);
                transparencyHandler3 += 10;                
                
        }
        
    }
}